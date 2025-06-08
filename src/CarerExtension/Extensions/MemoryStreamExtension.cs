namespace CarerExtension.Extensions;

/// <summary>
/// <see cref="MemoryStream"/>の拡張メソッドを提供します。
/// </summary>
public static class MemoryStreamExtension
{
    /// <summary>
    /// <see cref="MemoryStream"/>をクリアします。
    /// </summary>
    /// <param name="stream">クリアする<see cref="MemoryStream"/></param>
    public static void Clear(this MemoryStream stream)
    {
        stream.SetLength(0);
        stream.Position = 0;
    }
}
