namespace CarerExtension.Extensions;

/// <summary>
/// Generic Array class extension.
/// </summary>
public static class GenericArrayExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="destinationArray"></param>
    /// <param name="sourceArray"></param>
    /// <param name="length"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CopyFrom<T>(this T[] destinationArray, T[] sourceArray, int length) =>
        Array.Copy(sourceArray, destinationArray, length);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="destinationArray"></param>
    /// <param name="sourceArray"></param>
    /// <param name="length"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CopyFrom<T>(this T[] destinationArray, T[] sourceArray, long length) =>
        Array.Copy(sourceArray, destinationArray, length);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="destinationArray"></param>
    /// <param name="sourceArray"></param>
    /// <param name="destinationIndex"></param>
    /// <param name="sourceIndex"></param>
    /// <param name="length"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CopyFrom<T>(this T[] destinationArray, T[] sourceArray, int destinationIndex, int sourceIndex, int length) =>
        Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="destinationArray"></param>
    /// <param name="sourceArray"></param>
    /// <param name="destinationIndex"></param>
    /// <param name="sourceIndex"></param>
    /// <param name="length"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CopyFrom<T>(this T[] destinationArray, T[] sourceArray, long destinationIndex, long sourceIndex, long length) =>
        Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
}
