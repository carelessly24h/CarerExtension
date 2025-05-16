namespace CarerExtension.Extensions;

/// <summary>
/// Generic <see cref="Array"/> class extension.
/// </summary>
public static class GenericArrayExtension
{
    /// <summary>
    /// 最初の要素を開始位置として<see cref="Array"/>から要素の範囲をコピーし、最初の要素を開始位置として他の<see cref="Array"/>にそれらの要素を貼り付けます。
    /// </summary>
    /// <typeparam name="T">コピーする配列の型。</typeparam>
    /// <param name="destinationArray">データを受け取る<see cref="Array"/>。</param>
    /// <param name="sourceArray">コピーするデータを格納している<see cref="Array"/>。</param>
    /// <param name="length">コピーする要素の数を表す 32 ビット整数。</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CopyFrom<T>(this T[] destinationArray, T[] sourceArray, int length) =>
        Array.Copy(sourceArray, destinationArray, length);

    /// <summary>
    /// 最初の要素を開始位置として<see cref="Array"/>から要素の範囲をコピーし、最初の要素を開始位置として他の<see cref="Array"/>にそれらの要素を貼り付けます。
    /// </summary>
    /// <typeparam name="T">コピーする配列の型。</typeparam>
    /// <param name="destinationArray">データを受け取る<see cref="Array"/>。</param>
    /// <param name="sourceArray">コピーするデータを格納している<see cref="Array"/>。</param>
    /// <param name="length">コピーする要素の数を表す 32 ビット整数。</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CopyFrom<T>(this T[] destinationArray, T[] sourceArray, long length) =>
        Array.Copy(sourceArray, destinationArray, length);

    /// <summary>
    /// 指定したコピー元インデックスを開始位置として<see cref="Array"/>から要素の範囲をコピーし、指定したコピー先インデックスを開始位置として他の<see cref="Array"/>にそれらの要素を貼り付けます。 
    /// </summary>
    /// <typeparam name="T">コピーする配列の型。</typeparam>
    /// <param name="destinationArray">データを受け取る<see cref="Array"/>。</param>
    /// <param name="sourceArray">コピーするデータを格納している<see cref="Array"/>。</param>
    /// <param name="destinationIndex">格納を開始する<paramref name = "destinationArray"/>のインデックスを表す 64 ビット整数。</param>
    /// <param name="sourceIndex">コピー操作の開始位置となる<paramref name = "sourceArray"/>内のインデックスを表す整数。</param>
    /// <param name="length">コピーする要素の数を表す整数。 整数は、0 から<see langword="Int32.MaxValue"/>までの範囲で指定する必要があります。</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CopyFrom<T>(this T[] destinationArray, T[] sourceArray, int destinationIndex, int sourceIndex, int length) =>
        Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);

    /// <summary>
    /// 指定したコピー元インデックスを開始位置として Array から要素の範囲をコピーし、指定したコピー先インデックスを開始位置として他の Array にそれらの要素を貼り付けます。 
    /// </summary>
    /// <typeparam name="T">コピーする配列の型。</typeparam>
    /// <param name="destinationArray">データを受け取る<see cref="Array"/>。</param>
    /// <param name="sourceArray">コピーするデータを格納している<see cref="Array"/>。</param>
    /// <param name="destinationIndex">格納を開始する<paramref name = "destinationArray"/>のインデックスを表す 64 ビット整数。</param>
    /// <param name="sourceIndex">コピー操作の開始位置となる<paramref name = "sourceArray"/>内のインデックスを表す整数。</param>
    /// <param name="length">コピーする要素の数を表す整数。 整数は、0 からInt32.MaxValueまでの範囲で指定する必要があります。</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CopyFrom<T>(this T[] destinationArray, T[] sourceArray, long destinationIndex, long sourceIndex, long length) =>
        Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
}
