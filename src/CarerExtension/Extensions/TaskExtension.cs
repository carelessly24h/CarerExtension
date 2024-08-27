namespace CarerExtension.Extensions;

public static class TaskExtension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Stopped(this Task source)
    {
        Span<TaskStatus> taskStatuses =
        [
            TaskStatus.Canceled,
            TaskStatus.Created,
            TaskStatus.Faulted,
            TaskStatus.RanToCompletion,
            TaskStatus.WaitingForChildrenToComplete,
        ];
        return taskStatuses.Any(s => s == source.Status);
    }
}
