using CarerExtension.Utilities.Threading.Tasks.Arguments;

namespace CarerExtension.Utilities.Threading.Tasks;

internal class TaskWorker(ParallelProcessor taskMaster, string? id) : IDisposable
{
    #region constant
    /// <summary>
    /// todo:ちゃんと説明を書く。メモ：単位はミリ秒。
    /// </summary>
    private const int threadWaitingDelay = 100;
    #endregion

    #region variable
    /// <summary>
    /// todo: ちゃんと説明を書く。メモ：ワーカー自体の停止を管理するためのキャンセレーショントークンソース。
    /// </summary>
    private CancellationTokenSource? workerCancellationTokenSource;

    private readonly IProgress<TaskReportArgs> taskProgress = new Progress<TaskReportArgs>(taskMaster.ReceiveTaskReport);
    #endregion

    #region property
    /// <summary>
    /// todo: ちゃんと説明を書く。メモ：ワーカーの識別子。どのワーカーがタスクを処理したかを識別するために使う。ログを実装したらログに出す予定。
    /// </summary>
    public string? Id => id;
    #endregion

    #region constructor
    public TaskWorker(ParallelProcessor taskMaster) : this(taskMaster, null)
    {
    }
    #endregion

    public void Dispose()
    {
        // workerCancellationTokenSource disposing is left to RunAsync method.
        Suspend();
    }

    public async Task RunAsync()
    {
        try
        {
            workerCancellationTokenSource ??= new();

            while (true)
            {
                var token = workerCancellationTokenSource.Token;

                token.ThrowIfCancellationRequested();
                ExecuteTaskWhenExists();
                token.ThrowIfCancellationRequested();

                await Task.Delay(threadWaitingDelay, token);
            }
        }
        catch (OperationCanceledException)
        {
            // worker suspend requested.
            // worker will stop.
        }
        catch (ObjectDisposedException)
        {
            // master or worker was disposed.
            // worker will stop.
        }
        finally
        {
            DisposeCancellationTokenSource();
        }
    }

    private void ExecuteTaskWhenExists()
    {
        var taskInfo = taskMaster.TakeTask();
        if (taskInfo is not null)
        {
            ExecuteTask(taskInfo);
        }
    }

    private void ExecuteTask(TaskInfo taskInfo)
    {
        try
        {
            taskInfo.ExecuteTask();
            taskProgress.Report(TaskReportArgs.CompleteArgs(taskInfo, Id));
        }
        catch (OperationCanceledException)
        {
            taskProgress.Report(TaskReportArgs.CanceledArgs(taskInfo, Id));
        }
        catch (Exception e)
        {
            taskProgress.Report(TaskReportArgs.FaultedArgs(taskInfo, Id, e));
        }
    }

    private void DisposeCancellationTokenSource()
    {
        try
        {
            workerCancellationTokenSource?.Dispose();
        }
        catch (ObjectDisposedException)
        {
            // already disposed.
        }
        finally
        {
            workerCancellationTokenSource = null;
        }
    }

    public void Suspend() => workerCancellationTokenSource?.Cancel();
}
