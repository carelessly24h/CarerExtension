namespace CarerExtension.Extensions;

/// <summary>
/// ZipArchiveEntryの拡張メソッドを提供します。
/// </summary>
public static class ZipArchiveEntryExtension
{
    /// <summary>
    /// Zipアーカイブにバイナリデータを書き込みます。
    /// </summary>
    /// <param name="entry">データを書き込むZipアーカイブ。</param>
    /// <param name="buffer">書き込むデータ。</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Write(this ZipArchiveEntry entry, in Span<byte> buffer)
    {
        using var stream = entry.Open();
        stream.Write(buffer);
    }

    /// <summary>
    /// Zipアーカイブからバイナリデータを読み込みます。
    /// </summary>
    /// <param name="entry">データを読み込むZIPアーカイブ。</param>
    /// <returns>読み込んだデータ。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte[] Read(this ZipArchiveEntry entry)
    {
        using var stream = entry.Open();
        using var memory = new MemoryStream();
        stream.CopyTo(memory);
        return memory.ToArray();
    }
}
