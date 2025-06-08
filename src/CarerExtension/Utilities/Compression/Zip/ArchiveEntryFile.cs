using CarerExtension.Extensions;

namespace CarerExtension.Utilities.Compression.Zip;

public record struct ArchiveEntryFile(string FilePath, string EntryPath, bool IsDirectory)
{
    private byte[]? entryFileContentsCache;

    public static ArchiveEntryFile Create(string filePath)
    {
        var entryPath = GetEntryPath(filePath);
        var isDirectory = IsDirecotry(filePath);
        return new(filePath, entryPath, isDirectory);
    }

    public static ArchiveEntryFile Create(string filePath, string entryPath)
    {
        var isDirectory = IsDirecotry(filePath);
        return new(filePath, entryPath, isDirectory);
    }

    public static ArchiveEntryFile Create(ZipArchiveEntry entry) =>
        new(entry.FullName, entry.FullName, IsDirectory: entry.Name == "")
        {
            entryFileContentsCache = entry.Read(),
        };

    private static bool IsDirecotry(string filePath)
    {
        var info = new DirectoryInfo(filePath);
        return info.Exists || Path.GetFileName(filePath) == "";
    }

    private static string GetEntryPath(string filePath)
    {
        if (IsDirecotry(filePath))
        {
            var info = new DirectoryInfo(filePath);
            return $"{info.Name}/";
        }
        else
        {
            return Path.GetFileName(filePath);
        }
    }

    public byte[] ReadFromCache()
    {
        entryFileContentsCache ??= Read();
        return entryFileContentsCache;
    }

    private readonly byte[] Read()
    {
        using var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
        using var memory = new MemoryStream();
        stream.CopyTo(memory);
        return memory.ToArray();
    }

    public readonly void Write(string destinationDirectoryPath)
    {
        if (entryFileContentsCache == null)
        {
            return;
        }

        var filePath = Path.Combine(destinationDirectoryPath, EntryPath);
        CreateDirectory(filePath);

        using var stream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write);
        stream.Write(entryFileContentsCache, 0, entryFileContentsCache.Length);
    }

    private static void CreateDirectory(string entryFilePath)
    {
        var directoryPath = Path.GetDirectoryName(entryFilePath);
        if (directoryPath != null)
        {
            // match archive directory structure.
            Directory.CreateDirectory(directoryPath);
        }
        else
        {
            throw new InvalidOperationException("Cannot create directory for entry file path: " + entryFilePath);
        }
    }
}
