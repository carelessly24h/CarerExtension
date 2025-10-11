namespace CarerExtension.Extensions;

/// <summary>
/// Extension methods for <see cref="ReadOnlySpan{T}"/>.
/// </summary>
public static class ReadOnlySpanExtension
{
    /// <summary>
    /// スパンのすべての要素が条件を満たしているかどうかを判断します。
    /// </summary>
    /// <typeparam name="T"><paramref name="source"/>の要素の型。</typeparam>
    /// <param name="source">述語を適用する要素を格納している<see cref="ReadOnlySpan{T}"/>。</param>
    /// <param name="predicate">各要素が条件を満たしているかどうかをテストする関数。</param>
    /// <returns>
    /// 指定された述語でソース シーケンスのすべての要素がテストに合格する場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All<T>(this ReadOnlySpan<T> source, Func<T, bool> predicate)
    {
        // use `for` for performance.
        for (var i = 0; i < source.Length; i++)
        {
            if (!predicate(source[i]))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// スパンに要素が含まれているかどうかを判断します。
    /// </summary>
    /// <typeparam name="T"><paramref name="source"/>の要素の型。</typeparam>
    /// <param name="source">空かどうかを確認する<see cref="ReadOnlySpan{T}"/>。</param>
    /// <returns>
    /// スパンに要素が含まれている場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any<T>(this ReadOnlySpan<T> source) =>
        source.Length > 0;

    /// <summary>
    /// スパンのいずれかの要素が条件を満たしているかどうかを判断します。
    /// </summary>
    /// <typeparam name="T"><paramref name="source"/>の要素の型。</typeparam>
    /// <param name="source">述語を適用する要素を含む<see cref="ReadOnlySpan{T}"/>。</param>
    /// <param name="predicate">各要素が条件を満たしているかどうかをテストする関数。</param>
    /// <returns>
    /// スパンが空ではないか、指定された述語で少なくともその 1 つの要素がテストに合格する場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any<T>(this ReadOnlySpan<T> source, Func<T, bool> predicate)
    {
        // use `for` for performance.
        for (var i = 0; i < source.Length; i++)
        {
            if (predicate(source[i]))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 指定のインデックスで始まる現在のスパンからスライスを形成します。
    /// </summary>
    /// <typeparam name="T"><paramref name="source"/>の要素の型。</typeparam>
    /// <param name="source">スライスする<see cref="ReadOnlySpan{T}"/>。</param>
    /// <param name="start">スライスを開始する位置の 0 から始まるインデックス。</param>
    /// <returns><paramref name="start"/>からスパンの終わりまで、現在のスパンの全要素で構成されるスパン。</returns>
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

    /// <summary>
    /// 指定インデックスから始まる現在のスパンからスライスを指定の長さで形成します。
    /// </summary>
    /// <typeparam name="T"><paramref name="source"/>の要素の型。</typeparam>
    /// <param name="source">スライスする<see cref="ReadOnlySpan{T}"/>。</param>
    /// <param name="start">このスライスを開始する位置の 0 から始まるインデックス。</param>
    /// <param name="length">スライスに求められる長さ。</param>
    /// <returns><paramref name="start"/>で始まる現在のスパンからの<paramref name="length"/>要素で構成されるスパン。</returns>
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
