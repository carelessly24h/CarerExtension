using CarerExtension.Utilities.Threading.Tasks.Arguments;
using CarerExtension.Utilities.Threading.Tasks.Exceptions;

namespace CarerExtension.Utilities.Threading.Tasks;

public class ParallelProcessor : IDisposable
{
    #region event
    public event EventHandler<TaskEventArgs>? TaskCanceled;

    public event EventHandler<TaskEventArgs>? TaskCompleted;

    public event EventHandler<TaskFaultedEventArgs>? TaskFaulted;
    #endregion

    #region variable
    private readonly ConcurrentQueue<TaskInfo> waitingTasks = [];

    private readonly ConcurrentDictionary<Guid, TaskInfo> waitingTasksMap = [];

    private readonly ConcurrentDictionary<Guid, TaskInfo> runningTasks = [];

    private readonly List<TaskWorker> workers = [];
    #endregion

    #region property
    public bool HasWaitingTasks => !waitingTasksMap.IsEmpty;

    public bool HasRunningTasks => !runningTasks.IsEmpty;

    public int WorkersCount => workers.Count;
    #endregion

    #region method
    public void Dispose()
    {
        try
        {
            DisposeAllWorkers();
            workers.Clear();
        }
        finally
        {
            GC.SuppressFinalize(this);
        }
    }

    private void DisposeAllWorkers()
    {
        foreach (var worker in workers)
        {
            // disposing a worker will cancel the task.
            worker.Dispose();
        }
    }

    public void Start() => StartWarkers(1);

    public void Start(int workerCount)
    {
        DisposeWorkers();
        StartWarkers(workerCount);
    }

    private void DisposeWorkers()
    {
        foreach (var worker in workers)
        {
            worker.Dispose();
        }
        workers.Clear();
    }

    private void StartWarkers(int workerCount)
    {
        for (var i = 1; i <= workerCount; i++)
        {
            TaskWorker worker = new(this, $"Worker{i}");
            workers.Add(worker);
            _ = worker.RunAsync();
        }
    }

    // todo:戻り値はキャンセルするために使う予定。あとでテストで試してみる
    public Guid AddTask(Action<CancellationTokenSource> task)
    {
        TaskInfo taskInfo = new(task);

        // Task taking is parallel running.
        // So add it in the map first.
        waitingTasksMap.TryAdd(taskInfo.Id, taskInfo);
        waitingTasks.Enqueue(taskInfo);
        return taskInfo.Id;
    }

    internal TaskInfo? TakeTask()
    {
        // status update: wait -> running
        waitingTasks.TryDequeue(out var taskInfo);
        if (taskInfo is not null)
        {
            // TaskIds will not be duplicated.
            waitingTasksMap.TryRemove(taskInfo.Id, out _);
            runningTasks.TryAdd(taskInfo.Id, taskInfo);
        }
        return taskInfo;
    }

    public void CancelTask(Guid taskId) => CancelErrorHandle(() =>
    {
        // try cancel waiting task.
        waitingTasksMap.TryRemove(taskId, out var waitingTask);
        waitingTask?.Cancel();

        // try cancel running task.
        runningTasks.TryRemove(taskId, out var runningTask);
        runningTask?.Cancel();
    });

    public void CancelAllTasks() => CancelErrorHandle(() =>
    {
        // cancel waiting tasks.
        foreach (var task in waitingTasks)
        {
            task.Cancel();
        }
        // cancel running tasks.
        foreach (var task in runningTasks.Values)
        {
            task.Cancel();
        }
    });

    private static void CancelErrorHandle(Action action)
    {
        try
        {
            action();
        }
        catch (AggregateException e)
        {
            throw new ParallelProcessorException(e);
        }
        catch (ObjectDisposedException e)
        {
            throw new ParallelProcessorException(e);
        }
    }
    #endregion

    #region progress report method
    internal void ReceiveTaskReport(TaskReportArgs args)
    {
        runningTasks.TryRemove(args.TaskId, out var task);

        if (task is not null)
        {
            switch (args.ReportType)
            {
                case ReportType.Canceled:
                    OnTaskCanceled(args);
                    break;
                case ReportType.Completed:
                    OnTaskCompleted(args);
                    break;
                case ReportType.Faulted:
                    OnTaskFaulted(args);
                    break;
                default:
                    // do nothing.
                    break;
            }
        }
    }
    #endregion

    #region event invoker
    private void OnTaskCanceled(TaskReportArgs args) =>
        TaskCanceled?.Invoke(this, new(args));

    private void OnTaskCompleted(TaskReportArgs args) =>
        TaskCompleted?.Invoke(this, new(args));

    private void OnTaskFaulted(TaskReportArgs args) =>
        TaskFaulted?.Invoke(this, new(args));
    #endregion
}
