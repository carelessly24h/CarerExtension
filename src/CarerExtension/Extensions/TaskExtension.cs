namespace CarerExtension.Extensions;

/// <summary>
/// Extension methods for <see cref="Task"/>.
/// </summary>
public static class TaskExtension
{
    /// <summary>
    /// タスクが停止しているかどうかを判定します。
    /// 停止しているとは、タスクが完了したか、キャンセルされたか、または例外が発生したことを意味します。
    /// </summary>
    /// <param name="source">判定するタスク。</param>
    /// <returns>
    /// タスクが停止している場合は<see langword="true"/>。
    /// それ以外の場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsStopped(this Task source)
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
