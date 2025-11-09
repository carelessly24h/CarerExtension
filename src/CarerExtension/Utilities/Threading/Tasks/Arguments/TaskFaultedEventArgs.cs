namespace CarerExtension.Utilities.Threading.Tasks.Arguments;

public class TaskFaultedEventArgs(Guid taskId, string? workerId, Exception? exception) : TaskEventArgs(taskId, workerId)
{
    #region property
    public Exception? Exception => exception;
    #endregion

    #region constructor
    public TaskFaultedEventArgs(Guid taskId) : this(taskId, null, null)
    {
    }

    public TaskFaultedEventArgs(Guid taskId, string workerId) : this(taskId, workerId, null)
    {
    }

    public TaskFaultedEventArgs(Guid taskId, Exception exception) : this(taskId, null, exception)
    {
    }

    internal TaskFaultedEventArgs(TaskReportArgs args) : this(args.TaskId, args.WorkerId, args.Exception)
    {
    }
    #endregion
}
