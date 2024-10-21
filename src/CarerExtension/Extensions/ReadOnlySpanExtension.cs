namespace CarerExtension.Extensions;

public static class ReadOnlySpanExtension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All<T>(this ReadOnlySpan<T> source, Func<T, bool> predicate)
    {
        foreach (var value in source)
        {
            if (!predicate(value))
            {
                return false;
            }
        }
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any<T>(this ReadOnlySpan<T> source, Func<T, bool> predicate)
    {
        foreach (var value in source)
        {
            if (predicate(value))
            {
                return true;
            }
        }
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<T> SafetySlice<T>(this ReadOnlySpan<T> source, int start)
    {
        try
        {
            return source[start..];
        }
        catch (Exception e) when (e is ArgumentOutOfRangeException || e is IndexOutOfRangeException)
        {
            return default;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<T> SafetySlice<T>(this ReadOnlySpan<T> source, int start, int length)
    {
        try
        {
            return source.Slice(start, length);
        }
        catch (Exception e) when (e is ArgumentOutOfRangeException || e is IndexOutOfRangeException)
        {
            // if failed that try slice as much as possible.
            return source.SafetySlice(start);
        }
    }
}
