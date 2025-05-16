namespace CarerExtension.Extensions;

/// <summary>
/// Byte extension methods.
/// </summary>
public static class ByteExtension
{
    /// <summary>
    /// すべてのビットが1であるかどうかを返します。
    /// </summary>
    /// <param name="source">チェックするバイト。</param>
    /// <returns>
    /// すべてのビットが1の場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All(this byte source) => source == byte.MaxValue;

    /// <summary>
    /// <paramref name="mask"/>で指定されたビットがすべて1であるかどうかを返します。
    /// </summary>
    /// <param name="source">チェックするバイト。</param>
    /// <param name="mask">ビット位置を指定するためのフィルター。</param>
    /// <returns>
    /// すべてのビットが1の場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All(this byte source, byte mask) => (source & mask) == mask;

    /// <summary>
    /// <paramref name="position"/>で指定された位置のビットが1であるかどうかを返します。
    /// </summary>
    /// <param name="source">チェックするバイト。</param>
    /// <param name="position">チェックするBit位置。</param>
    /// <returns>
    /// ビットが1の場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="position"/>がバイトの範囲外。</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Bit(this byte source, int position)
    {
        if (position < 0 || 7 < position)
        {
            throw new ArgumentOutOfRangeException(nameof(position));
        }

        var mask = 1 << position;
        return (source & mask) != 0;
    }
}
