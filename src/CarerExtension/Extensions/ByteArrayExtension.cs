namespace CarerExtension.Extensions;

/// <summary>
/// Extension methods for byte array.
/// </summary>
public static class ByteArrayExtension
{
    /// <summary>
    /// バイト配列の指定された位置で2バイトから変換されたブール値を返します。
    /// </summary>
    /// <param name="value">バイト配列。</param>
    /// <param name="startIndex"><paramref name="value"/>内の開始位置。</param>
    /// <returns>
    /// <paramref name="startIndex"/>のバイトが0でない場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    public static bool ToBoolean(this byte[] value, int startIndex = 0) =>
        BitConverter.ToBoolean(value, startIndex);

    /// <summary>
    /// バイト配列の指定された位置で4バイトから変換された32ビットの署名された整数を返します。
    /// </summary>
    /// <param name="value">バイト配列。</param>
    /// <param name="startIndex"><paramref name="value"/>内の開始位置。</param>
    /// <returns><paramref name = "startIndex"/>で始まる4バイトで形成された32ビットの署名整数。</returns>
    public static int ToInt32(this byte[] value, int startIndex = 0) =>
        BitConverter.ToInt32(value, startIndex);

    /// <summary>
    /// バイト配列の指定された位置で8バイトから変換された64ビットの署名された整数を返します。
    /// </summary>
    /// <param name="value">バイト配列。</param>
    /// <param name="startIndex"><paramref name="value"/>内の開始位置。</param>
    /// <returns><paramRef name = "startIndex"/>で始まる8バイトで形成された64ビットの署名された整数。</returns>
    public static long ToInt64(this byte[] value, int startIndex = 0) =>
        BitConverter.ToInt64(value, startIndex);
}
