namespace CarerExtension.Extensions;

public static class IComparableExtension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Clamp<T>(this T value, T min, T max) where T : IComparable
    {
        var argCompareResult = min.CompareTo(max);
        if (argCompareResult > 0)
        {
            // this message is the same as Math.clamp exception message.
            throw new ArgumentException($"'{min}' cannot be greater than {max}.");
        }

        var minResult = value.CompareTo(min);
        if (minResult <= 0)
        {
            return min;
        }
        var maxResult = value.CompareTo(max);
        if (maxResult >= 0)
        {
            return max;
        }
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBetween<T>(this T value, T low, T high) where T : IComparable
    {
        var argCompareResult = low.CompareTo(high);
        if (argCompareResult > 0)
        {
            throw new ArgumentException($"'{nameof(low)}' cannot be greater than {nameof(high)}.");
        }

        return 0 <= value.CompareTo(low) && value.CompareTo(high) <= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsExclude<T>(this T? value, params T?[] values) where T : IComparable =>
        !value.IsInclude(values);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsExclude<T>(this T? value, IEnumerable<T?> values) where T : IComparable =>
        !value.IsInclude(values);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsExclude<T>(this T? value, Span<T?> values) where T : IComparable =>
        !value.IsInclude(values);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInclude<T>(this T? value, params T?[] values) where T : IComparable =>
        value.IsInclude((IEnumerable<T?>)values);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInclude<T>(this T? value, IEnumerable<T?> values) where T : IComparable
    {
        if (value != null)
        {
            return values.Any(v => value.Equals(v));
        }
        else
        {
            return values.Any(v => v == null);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInclude<T>(this T? value, Span<T?> values) where T : IComparable
    {
        if (value != null)
        {
            return values.Any(v => value.Equals(v));
        }
        else
        {
            return values.Any(v => v == null);
        }
    }
}
