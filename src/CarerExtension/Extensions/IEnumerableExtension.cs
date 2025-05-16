using CarerExtension.Extensions.Chunking;

namespace CarerExtension.Extensions;

/// <summary>
/// <see cref="IEnumerable{T}"/>の拡張メソッドを提供します。
/// </summary>
public static class IEnumerableExtension
{
    /// <summary>
    /// 述語に基づいて値のシーケンスをチャンクに分けた結果を返します。
    /// </summary>
    /// <remarks>ruby like.</remarks>
    /// <typeparam name="T"><paramref name="source"/>の要素の型。</typeparam>
    /// <param name="source">チャンクに分ける<see cref="IEnumerable{T}"/>。</param>
    /// <param name="predicate">各要素が条件を満たしているかどうかをテストする関数。</param>
    /// <returns>条件を満たす、入力シーケンスの要素を含む <see cref="IEnumerable{T}"/>。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IChunking<T>> Chunk<T>(
        this IEnumerable<T> source,
        Func<T, bool> predicate) =>
        source.Chunk(predicate, item => !predicate(item));

    /// <summary>
    /// 述語に基づいて値のシーケンスをチャンクに分けた結果を返します。
    /// </summary>
    /// <typeparam name="T"><paramref name="source"/>の要素の型。</typeparam>
    /// <param name="source">チャンクに分ける<see cref="IEnumerable{T}"/>。</param>
    /// <param name="chunkStartPredicate">各要素が条件を満たしているかどうかをテストする関数。</param>
    /// <param name="chunkStopPredicate">各要素が条件を満たしていないことをテストする関数。</param>
    /// <returns>条件を満たす、入力シーケンスの要素を含む <see cref="IEnumerable{T}"/>。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IChunking<T>> Chunk<T>(
        this IEnumerable<T> source,
        Func<T, bool> chunkStartPredicate,
        Func<T, bool> chunkStopPredicate)
    {
        var skipCount = 0;

        var items = source.Select((v, i) => (Value: v, Index: i));
        while (true)
        {
            var targets = items.Skip(skipCount);
            if (targets.None())
            {
                break;
            }

            var chunk = targets
                .SkipWhile(t => !chunkStartPredicate(t.Value) || chunkStopPredicate(t.Value))
                .TakeWhile(t => !chunkStopPredicate(t.Value));
            if (chunk.None())
            {
                break;
            }

            var startIndex = chunk.First().Index;
            var endIndex = chunk.Last().Index;
            yield return Chunking<T>.Create(source, startIndex, endIndex);

            skipCount = endIndex + 1;
        }
    }

    /// <summary>
    /// シーケンスからnullを除外した結果を返します。
    /// </summary>
    /// <remarks>ruby like.</remarks>
    /// <typeparam name="T"><paramref name="source"/>の要素の型。</typeparam>
    /// <param name="source">シーケンス。</param>
    /// <returns>nullを除外したシーケンス。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Compact<T>(this IEnumerable<T> source) =>
        source.Where(s => s != null);

    /// <summary>
    /// 2つのシーケンスを交差結合した結果を返します。
    /// </summary>
    /// <typeparam name="TOuter">最初のシーケンスの要素の型。</typeparam>
    /// <typeparam name="TInner">2番目のシーケンスの要素の型。</typeparam>
    /// <param name="outer">結合する最初のシーケンス。</param>
    /// <param name="inner">最初のシーケンスに結合するシーケンス。</param>
    /// <returns>2つのシーケンスに対して交差結合を実行して取得する、</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<(TOuter, TInner)> CrossJoin<TOuter, TInner>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner) =>
        outer.Join(inner, _ => true, _ => true, (o, i) => (o, i));

    /// <summary>
    /// シーケンスの要素を指定された回数繰り返した結果を返します。
    /// </summary>
    /// <remarks>ruby like.</remarks>
    /// <typeparam name="T"><paramref name="source"/>の要素の型。</typeparam>
    /// <param name="source">シーケンス。</param>
    /// <param name="count">回数。</param>
    /// <returns>シーケンスを指定された回数繰り返したシーケンス。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Cycle<T>(this IEnumerable<T> source, int count)
    {
        for (var i = 0; i < count; i++)
        {
            foreach (var s in source)
            {
                yield return s;
            }
        }
    }

    /// <summary>
    /// シーケンスの要素を重複ありで指定された要素数で区切り、シーケンスの終了まで繰り返した結果を返します。
    /// </summary>
    /// <remarks>ruby like.</remarks>
    /// <typeparam name="T"><paramref name="source"/>の要素の型。</typeparam>
    /// <param name="source">シーケンス。</param>
    /// <param name="count">要素数。</param>
    /// <returns>シーケンスの要素を重複ありで指定された要素数で区切ったシーケンス。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IEnumerable<T>> EachCons<T>(this IEnumerable<T> source, int count)
    {
        if (count <= 0)
        {
            throw new ArgumentException($"{nameof(count)} must be greater than 0.", nameof(count));
        }

        var finishIndex = source.Count() - count;
        for (var startIndex = 0; startIndex <= finishIndex; startIndex++)
        {
            var current = source.Slice(startIndex, count);
            if (current.Any())
            {
                yield return current;
            }
            else
            {
                yield break;
            }
        }
    }

    /// <summary>
    /// 渡された要素を除いたシーケンスを返します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <typeparam name="T"><paramref name="source"/>の要素の型。</typeparam>
    /// <param name="source">シーケンス。</param>
    /// <param name="elements">除外する要素。</param>
    /// <returns>渡された要素を除いたシーケンス。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Excluding<T>(this IEnumerable<T> source, T[] elements) =>
        source.Excluding(elements.AsEnumerable());

    /// <summary>
    /// 渡された要素を除いたシーケンスを返します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <typeparam name="T"><paramref name="source"/>の要素の型。</typeparam>
    /// <param name="source">シーケンス。</param>
    /// <param name="elements">除外する要素。</param>
    /// <returns>渡された要素を除いたシーケンス。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Excluding<T>(this IEnumerable<T> source, IEnumerable<T> elements) =>
        source.Where(s => !elements.Contains(s));

    /// <summary>
    /// シーケンスの末尾に渡された要素を追加した結果を返します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <typeparam name="T"><paramref name="source"/>の要素の型。</typeparam>
    /// <param name="source">シーケンス。</param>
    /// <param name="elements">追加する要素。</param>
    /// <returns>シーケンスに渡された要素を追加したシーケンス。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Including<T>(this IEnumerable<T> source, params T[] elements) =>
        Including(source, elements.AsEnumerable());

    /// <summary>
    /// シーケンスの末尾に渡された要素を追加した結果を返します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <typeparam name="T"><paramref name="source"/>の要素の型。</typeparam>
    /// <param name="source">シーケンス。</param>
    /// <param name="elements">追加する要素。</param>
    /// <returns>シーケンスに渡された要素を追加したシーケンス。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Including<T>(this IEnumerable<T> source, IEnumerable<T> elements) =>
        source.Concat(elements);

    /// <summary>
    /// シーケンスの要素数が指定された数よりも多いかどうかを判定します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <typeparam name="T"><paramref name="source"/>の要素の型。</typeparam>
    /// <param name="source">シーケンス。</param>
    /// <param name="count">判定する数。</param>
    /// <returns>
    /// シーケンスの要素数が指定された数よりも多い場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Many<T>(this IEnumerable<T> source, int count = 1) =>
        source.Count() > count;

    /// <summary>
    /// シーケンスに要素が含まれていないかどうかを判定します。
    /// </summary>
    /// <typeparam name="T"><paramref name="source"/>の要素の型。</typeparam>
    /// <param name="source">空かどうかを確認する<see cref="IEnumerable{T}"/>。</param>
    /// <returns>
    /// シーケンスに要素が含まれていない場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool None<T>(this IEnumerable<T> source) =>
        !source.Any();

    /// <summary>
    /// シーケンスの任意の要素が条件を満たさないかどうかを判定します。
    /// </summary>
    /// <typeparam name="T"><paramref name="source"/>の要素の型。</typeparam>
    /// <param name="source">述語を適用する要素を含む<see cref="IEnumerable{T}"/>。</param>
    /// <param name="predicate">各要素が条件を満たしているかどうかをテストする関数。</param>
    /// <returns>
    /// シーケンスが空か、指定された述語で少なくともその1つの要素がテストに合格する場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool None<T>(this IEnumerable<T> source, Func<T, bool> predicate) =>
        !source.Any(predicate);

    /// <summary>
    /// 述語に基づいて値のシーケンスを除外した結果を返します。
    /// </summary>
    /// <typeparam name="T"><paramref name="source"/>の要素の型。</typeparam>
    /// <param name="source">フィルター処理する<see cref="IEnumerable{T}"/>。</param>
    /// <param name="predicate">各要素が条件を満たしているかどうかをテストする関数。</param>
    /// <returns>条件を満たす、入力シーケンスの要素を含まない<see cref="IEnumerable{T}"/></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Reject<T>(this IEnumerable<T> source, Func<T, bool> predicate) =>
        source.Where(s => !predicate(s));

    /// <summary>
    /// 指定されたシーケンスの一部を返します。
    /// </summary>
    /// <remarks>ruby like.</remarks>
    /// <typeparam name="T"><paramref name="source"/>の要素の型。</typeparam>
    /// <param name="source">返される要素が含まれる<see cref="IEnumerable{T}"/>。</param>
    /// <param name="startIndex">残りの要素を返す前にスキップする要素のインデックス。</param>
    /// <param name="count">返す要素数。</param>
    /// <returns>指定されたシーケンスの一部。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Slice<T>(this IEnumerable<T> source, int startIndex, int count) =>
        source.Skip(startIndex).Take(count);

    /// <summary>
    /// インデックスにより指定されたシーケンスの一部を返します。
    /// </summary>
    /// <remarks>ruby like.</remarks>
    /// <typeparam name="T"><paramref name="source"/>の要素の型。</typeparam>
    /// <param name="source">返される要素が含まれる<see cref="IEnumerable{T}"/>。</param>
    /// <param name="startIndex">残りの要素を返す前にスキップする要素のインデックス。</param>
    /// <param name="endIndex">返す要素の最後のインデックス。</param>
    /// <returns>指定されたシーケンスの一部。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> SliceByIndex<T>(this IEnumerable<T> source, int startIndex, int endIndex)
    {
        var count = endIndex - startIndex + 1;
        return source.Slice(startIndex, count);
    }
}
