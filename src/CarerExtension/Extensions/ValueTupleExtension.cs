namespace CarerExtension.Extensions;

/// <summary>
/// Extension methods for <see cref="ValueTuple{T1, T2}"/>.
/// </summary>
public static class ValueTupleExtension
{
    /// <summary>
    /// 指定した範囲内の整数のシーケンスを生成します。
    /// </summary>
    /// <param name="source">シーケンス内の最初の整数の値と生成する連続した整数の数。</param>
    /// <returns>整数の範囲を含む<see cref="IEnumerable{Int32}"/></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> Range(this (int Start, int Count) source) =>
        Enumerable.Range(source.Start, source.Count);
}
