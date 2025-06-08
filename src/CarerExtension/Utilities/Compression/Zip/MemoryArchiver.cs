using CarerExtension.Extensions;

namespace CarerExtension.Utilities.Compression.Zip;

public class MemoryArchiver : IDisposable
{
    #region constants
    private const CompressionLevel ArchiveCompressionLevel = CompressionLevel.Optimal;
    #endregion

    #region variable
    private readonly MemoryStream memory = new();
    #endregion

    #region property
    public string ZipFilePath { get; }
    #endregion

    public MemoryArchiver(string zipFilePath)
    {
        ZipFilePath = zipFilePath;
        // take care of source zip.
        memory.Write(File.ReadAllBytes(ZipFilePath));
    }

    #region methods
    public void Dispose()
    {
        try
        {
            memory.Dispose();
        }
        finally
        {
            GC.SuppressFinalize(this);
        }
    }

    public byte[] GetBytes() => memory.ToArray();

    public string GetComment()
    {
        using var archive = new ZipArchive(memory, ZipArchiveMode.Read, true);
        return archive.Comment;
    }

    #region Compress
    public static byte[] Compress(string filePath, string? comment = null) =>
        Compress([ArchiveEntryFile.Create(filePath)], comment);

    public static byte[] Compress(in ArchiveEntryFile entryFile, string? comment = null) =>
        Compress([entryFile], comment);

    public static byte[] Compress(IEnumerable<string> filePaths, string? comment = null) =>
        Compress(filePaths.Select(ArchiveEntryFile.Create), comment);

    public static byte[] Compress(IEnumerable<ArchiveEntryFile> entryFiles, string? comment = null)
    {
        using var memory = new MemoryStream();
        Zip(memory, entryFiles, comment);
        return memory.ToArray();
    }
    #endregion

    #region Decompress
    public static IEnumerable<ArchiveEntryFile> Decompress(string filePath)
    {
        using var archiver = new MemoryArchiver(filePath);
        return archiver.Decompress();
    }

    public IEnumerable<ArchiveEntryFile> Decompress()
    {
        using var archive = new ZipArchive(memory, ZipArchiveMode.Read, true);
        // needs convert to array.
        return [.. archive.Entries.Select(ArchiveEntryFile.Create)];
    }
    #endregion

    #region Add
    public void AddEntry(string filePath, string entryPath, string? newComment = null) =>
        AddEntries([ArchiveEntryFile.Create(filePath, entryPath)], newComment);

    public void AddEntry(in ArchiveEntryFile entryFile, string? newComment = null) =>
        AddEntries([entryFile], newComment);

    public void AddEntries(IEnumerable<ArchiveEntryFile> entryFiles, string? newComment = null)
    {
        // Zip binary output must be after disposing ZipArchive.
        using var archive = new ZipArchive(memory, ZipArchiveMode.Update, true);

        foreach (var entryFile in entryFiles)
        {
            var entry = archive.CreateEntry(entryFile.EntryPath, ArchiveCompressionLevel);
            if (!entryFile.IsDirectory)
            {
                entry.Write(entryFile.ReadFromCache());
            }
        }

        if (newComment != null)
        {
            archive.Comment = newComment;
        }
    }
    #endregion

    #region Remove
    public void RemoveEntry(in ArchiveEntryFile entryFile, string? newComment = null) =>
        RemoveEntries([entryFile.EntryPath], newComment);

    public void RemoveEntry(string entryFilePath, string? newComment = null) =>
        RemoveEntries([entryFilePath], newComment);

    public void RemoveEntries(IEnumerable<ArchiveEntryFile> entryFiles, string? newComment = null) =>
        RemoveEntries(entryFiles.Select(entry => entry.EntryPath), newComment);

    public void RemoveEntries(IEnumerable<string> entryFilePaths, string? newComment = null)
    {
        var newEntries = P(entryFilePaths);
        Zip(memory, newEntries, newComment);
    }

    private ArchiveEntryFile[] P(IEnumerable<string> entryFilePaths)
    {
        // Zip binary output must be after disposing ZipArchive.
        using var oldArchive = new ZipArchive(memory, ZipArchiveMode.Read, true);
        var entries = oldArchive.Entries.Reject(entry => entry.FullName.IsInclude(entryFilePaths));
        var results = entries.Select(ArchiveEntryFile.Create).ToArray();
        // todo:memoryを使わずにfilestreamをnewすれば良いのでは？read専用だし
        memory.Clear();
        return results;
    }
    #endregion

    #region Change
    public byte[] ChangeComment(string newComment)
    {
        // todo:
        {
            using var archive = new ZipArchive(memory, ZipArchiveMode.Create, true);
            archive.Comment = newComment;
        }
        return memory.ToArray();
    }
    #endregion

    private static void Zip(Stream stream, IEnumerable<ArchiveEntryFile> entryFiles, string? comment)
    {
        // Zip binary output must be after disposing ZipArchive.
        using var archive = new ZipArchive(stream, ZipArchiveMode.Create, true);

        foreach (var entryFile in entryFiles)
        {
            var entry = archive.CreateEntry(entryFile.EntryPath, ArchiveCompressionLevel);
            if (!entryFile.IsDirectory)
            {
                entry.Write(entryFile.ReadFromCache());
            }
        }

        if (comment != null)
        {
            archive.Comment = comment;
        }
    }
    #endregion
}
