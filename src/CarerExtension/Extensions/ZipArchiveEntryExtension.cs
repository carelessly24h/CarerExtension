namespace CarerExtension.Extensions;

public static class ZipArchiveEntryExtension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Write(this ZipArchiveEntry entry, byte[] buffer)
    {
        using var stream = entry.Open();
        stream.Write(buffer, 0, buffer.Length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte[] Read(this ZipArchiveEntry entry)
    {
        using var stream = entry.Open();
        using var memory = new MemoryStream();
        stream.CopyTo(memory);
        return memory.ToArray();
    }
}
