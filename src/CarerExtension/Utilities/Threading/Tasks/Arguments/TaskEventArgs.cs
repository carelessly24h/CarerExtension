namespace CarerExtension.Utilities.Threading.Tasks.Arguments;

public class TaskEventArgs(Guid taskId, string? workerId) : EventArgs
{
    #region property
    public Guid TaskId => taskId;

    public string? WorkerId => workerId;
    #endregion

    #region constructor
    public TaskEventArgs(Guid taskId) : this(taskId, null)
    {
    }

    internal TaskEventArgs(TaskReportArgs args) : this(args.TaskId, args.WorkerId)
    {
    }
    #endregion
}
