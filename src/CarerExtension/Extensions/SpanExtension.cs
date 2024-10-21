namespace CarerExtension.Extensions;

/// <summary>
/// Span struct extension.
/// </summary>
public static class SpanExtension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All<T>(this Span<T> source, Func<T, bool> predicate)
    {
        var s = (ReadOnlySpan<T>)source;
        return s.All(predicate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any<T>(this Span<T> source, Func<T, bool> predicate)
    {
        var s = (ReadOnlySpan<T>)source;
        return s.Any(predicate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<T> SafetySlice<T>(this Span<T> source, int start)
    {
        var s = (ReadOnlySpan<T>)source;
        return s.SafetySlice(start);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<T> SafetySlice<T>(this Span<T> source, int start, int length)
    {
        var s = (ReadOnlySpan<T>)source;
        return s.SafetySlice(start, length);
    }
}
