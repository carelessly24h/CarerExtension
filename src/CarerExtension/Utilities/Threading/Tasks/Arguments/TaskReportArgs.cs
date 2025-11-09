namespace CarerExtension.Utilities.Threading.Tasks.Arguments;

internal enum ReportType
{
    Completed,
    Canceled,
    Faulted
}

internal record class TaskReportArgs(ReportType ReportType, Guid TaskId, string? WorkerId, Exception? Exception)
{
    public static TaskReportArgs CompleteArgs(TaskInfo info, string? workerId) =>
        new(ReportType.Completed, info.Id, workerId, null);

    public static TaskReportArgs CanceledArgs(TaskInfo info, string? workerId) =>
        new(ReportType.Canceled, info.Id, workerId, null);

    public static TaskReportArgs FaultedArgs(TaskInfo info, string? workerId, Exception exception) =>
        new(ReportType.Faulted, info.Id, workerId, exception);
}
