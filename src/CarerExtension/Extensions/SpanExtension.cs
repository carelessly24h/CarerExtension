namespace CarerExtension.Extensions;

/// <summary>
/// Span struct extension.
/// </summary>
public static class SpanExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All<T>(this Span<T> source, Func<T, bool> predicate)
    {
        var s = (ReadOnlySpan<T>)source;
        return s.All(predicate);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
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
    public static bool Any<T>(this Span<T> source, Func<T, bool> predicate)
    {
        var s = (ReadOnlySpan<T>)source;
        return s.Any(predicate);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
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
    public static ReadOnlySpan<T> SafetySlice<T>(this Span<T> source, int start)
    {
        var s = (ReadOnlySpan<T>)source;
        return s.SafetySlice(start);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="start"></param>
    /// <returns></returns>
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
    public static ReadOnlySpan<T> SafetySlice<T>(this Span<T> source, int start, int length)
    {
        var s = (ReadOnlySpan<T>)source;
        return s.SafetySlice(start, length);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="start"></param>
    /// <param name="length"></param>
    /// <returns></returns>
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
