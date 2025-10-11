namespace CarerExtension.Extensions;

/// <summary>
/// Extension methods for <see cref="IComparable"/>.
/// </summary>
public static class IComparableExtension
{
    /// <summary>。
    /// レシーバの値を範囲内に収めます
    /// </summary>
    /// <typeparam name="T">レシーバの値の型</typeparam>
    /// <param name="value">値。</param>
    /// <param name="limit1">上限または下限を示す値。</param>
    /// <param name="limit2">下限または上限を示す値。</param>
    /// <returns>
    /// レシーバの値が<paramref name="limit1"/>と<paramref name="limit2"/>の範囲内にある場合はレシーバの値。
    /// そうでない場合は上限または下限の値を返します。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Clamp<T>(this T value, T limit1, T limit2) where T : IComparable
    {
        var minLimit = limit1.CompareTo(limit2) < 0 ? limit1 : limit2;
        var maxLimit = limit1.CompareTo(limit2) < 0 ? limit2 : limit1;

        return value switch
        {
            var v when v.CompareTo(minLimit) < 0 => minLimit,
            var v when v.CompareTo(maxLimit) > 0 => maxLimit,
            _ => value,
        };
    }

    /// <summary>
    /// レシーバの値が指定された範囲内にあるかどうかを示します。
    /// </summary>
    /// <typeparam name="T">レシーバの値の型</typeparam>
    /// <param name="value">チェックする値。</param>
    /// <param name="limit1">上限または下限を示す値。</param>
    /// <param name="limit2">下限または上限を示す値。</param>
    /// <returns>
    /// レシーバの数値が指定した範囲内の場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBetween<T>(this T value, T limit1, T limit2) where T : IComparable =>
        (0 <= value.CompareTo(limit1) && value.CompareTo(limit2) <= 0) ||
        (0 <= value.CompareTo(limit2) && value.CompareTo(limit1) <= 0);

    /// <summary>
    /// レシーバの値がコレクション内に含まれないかどうかを示します。
    /// </summary>
    /// <typeparam name="T">レシーバの値の型</typeparam>
    /// <param name="value">チェックする値。</param>
    /// <param name="values">レシーバの値が含まれないコレクション。</param>
    /// <returns>
    /// レシーバの値がコレクションに含まれない場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsExclude<T>(this T? value, params T?[] values) where T : IComparable =>
        !value.IsInclude(values);

    /// <summary>
    /// レシーバの値がコレクション内に含まれないかどうかを示します。
    /// </summary>
    /// <typeparam name="T">レシーバの値の型</typeparam>
    /// <param name="value">チェックする値。</param>
    /// <param name="values">レシーバの値が含まれないコレクション。</param>
    /// <returns>
    /// レシーバの値がコレクションに含まれない場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsExclude<T>(this T? value, IEnumerable<T?> values) where T : IComparable =>
        !value.IsInclude(values);

    /// <summary>
    /// レシーバの値がコレクション内に含まれないかどうかを示します。
    /// </summary>
    /// <typeparam name="T">レシーバの値の型</typeparam>
    /// <param name="value">チェックする値。</param>
    /// <param name="values">レシーバの値が含まれないコレクション。</param>
    /// <returns>
    /// レシーバの値がコレクションに含まれない場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsExclude<T>(this T? value, Span<T?> values) where T : IComparable =>
        !value.IsInclude(values);

    /// <summary>
    /// レシーバの値がコレクション内に含まれるかどうかを示します。
    /// </summary>
    /// <typeparam name="T">レシーバの値の型</typeparam>
    /// <param name="value">チェックする値。</param>
    /// <param name="values">レシーバの値が含まれるコレクション。</param>
    /// <returns>
    /// レシーバの値がコレクションに含まれる場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsInclude<T>(this T? value, params T?[] values) where T : IComparable =>
        value.IsInclude((IEnumerable<T?>)values);

    /// <summary>
    /// レシーバの値がコレクション内に含まれるかどうかを示します。
    /// </summary>
    /// <typeparam name="T">レシーバの値の型</typeparam>
    /// <param name="value">チェックする値。</param>
    /// <param name="values">レシーバの値が含まれるコレクション。</param>
    /// <returns>
    /// レシーバの値がコレクションに含まれる場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
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

    /// <summary>
    /// レシーバの値がコレクション内に含まれるかどうかを示します。
    /// </summary>
    /// <typeparam name="T">レシーバの値の型</typeparam>
    /// <param name="value">チェックする値。</param>
    /// <param name="values">レシーバの値が含まれるコレクション。</param>
    /// <returns>
    /// レシーバの値がコレクションに含まれる場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
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
