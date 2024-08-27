namespace CarerExtension.Extensions;

public static class CancellationTokenSourceExtension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryCancel(this CancellationTokenSource source)
    {
        try
        {
            source.Cancel();
            return true;
        }
        catch (Exception e) when (e is ObjectDisposedException || e is AggregateException)
        {
            return false;
        }
    }
}
