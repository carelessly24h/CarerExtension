using CarerExtension.Extensions;

namespace CarerExtension.Utilities.Compression.Zip;

/// <summary>
/// ディスク上のファイルをメモリ上で圧縮、展開する機能を提供します。
/// ファイルの圧縮、展開にはZip圧縮方式を使用します。
/// </summary>
public class MemoryArchiver : IDisposable
{
    /// <summary>
    /// Zip展開後のファイルデータを格納する構造体です。
    /// </summary>
    /// <param name="Comment">Zipファイルのコメント。</param>
    /// <param name="Entries">展開したファイルの一覧。</param>
    private record struct UnzippedArchive(string Comment, ArchiveEntryFile[] Entries);

    #region constants
    /// <summary>
    /// 圧縮操作で速度を重視するか、または圧縮サイズを重視するかを示す値。
    /// 現在はバランス重視で設定。
    /// </summary>
    private const CompressionLevel ArchiveCompressionLevel = CompressionLevel.Optimal;
    #endregion

    #region variable
    /// <summary>
    /// アーカイブファイルのデータを管理するストリーム
    /// </summary>
    private readonly MemoryStream memory = new();
    #endregion

    #region property
    /// <summary>
    /// Zipファイルパス
    /// </summary>
    public string ZipFilePath { get; }
    #endregion

    #region constructor
    /// <summary>
    /// ディスク上のファイルをメモリ上で圧縮、展開する機能を提供します。
    /// ファイルの圧縮、展開にはZip圧縮方式を使用します。
    /// </summary>
    /// <param name="zipFilePath">Zipファイルパス。</param>
    public MemoryArchiver(string zipFilePath)
    {
        ZipFilePath = zipFilePath;

        // load into memory.
        var contents = File.ReadAllBytes(ZipFilePath);
        memory.Write(contents);
    }
    #endregion

    #region methods
    /// <summary>
    /// 解放処理
    /// </summary>
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

    /// <summary>
    /// アーカイブのデータを取得します。
    /// </summary>
    /// <returns>アーカイブのデータ。</returns>
    public byte[] GetBytes() => memory.ToArray();

    /// <summary>
    /// アーカイブのコメントを取得します。
    /// </summary>
    /// <returns>読み取ったコメント。</returns>
    public string GetComment() => GetComment(memory);

    #region Compress
    /// <summary>
    /// ファイルを指定してZipファイルに圧縮し、圧縮結果のバイナリデータを返します。
    /// </summary>
    /// <param name="filePath">圧縮するファイルのパス。</param>
    /// <param name="comment">Zipファイルに設定するコメント。</param>
    /// <returns>ファイルを圧縮した結果のバイナリデータ。</returns>
    public static byte[] Compress(string filePath, string? comment = null)
    {
        var entry = ArchiveEntryFile.Create(filePath);
        return Compress(entry, comment);
    }

    /// <summary>
    /// ファイルを指定してZipファイルに圧縮し、圧縮結果のバイナリデータを返します。
    /// </summary>
    /// <param name="entryFile">圧縮するファイルの情報。</param>
    /// <param name="comment">Zipファイルに設定するコメント。</param>
    /// <returns>ファイルを圧縮した結果のバイナリデータ。</returns>
    public static byte[] Compress(in ArchiveEntryFile entryFile, string? comment = null) =>
        Compress([entryFile], comment);

    /// <summary>
    /// ファイルを指定してZipファイルに圧縮し、圧縮結果のバイナリデータを返します。
    /// </summary>
    /// <param name="filePaths">圧縮するファイルのパス。</param>
    /// <param name="comment">Zipファイルに設定するコメント。</param>
    /// <returns>ファイルを圧縮した結果のバイナリデータ。</returns>
    public static byte[] Compress(IEnumerable<string> filePaths, string? comment = null)
    {
        var entries = filePaths.Select(ArchiveEntryFile.Create);
        return Compress(entries, comment);
    }

    /// <summary>
    /// ファイルを指定してZipファイルに圧縮し、圧縮結果のバイナリデータを返します。
    /// </summary>
    /// <param name="entryFiles">圧縮するファイルの情報。</param>
    /// <param name="comment">Zipファイルに設定するコメント。</param>
    /// <returns>ファイルを圧縮した結果のバイナリデータ。</returns>
    public static byte[] Compress(IEnumerable<ArchiveEntryFile> entryFiles, string? comment = null)
    {
        using var memory = new MemoryStream();
        CreateZip(memory, entryFiles, comment);
        return memory.ToArray();
    }
    #endregion

    #region Decompress
    /// <summary>
    /// 指定されたZipファイルを展開し、アーカイブ内のファイルの一覧を返します。
    /// </summary>
    /// <param name="filePath">圧縮ファイルパス。</param>
    /// <returns>Zipファイルの展開結果のコレクション。</returns>
    public static IEnumerable<ArchiveEntryFile> Decompress(string filePath)
    {
        using var archiver = new MemoryArchiver(filePath);
        return archiver.Decompress();
    }

    /// <summary>
    /// Zipファイルを展開し、アーカイブ内のファイルの一覧を返します。
    /// </summary>
    /// <returns>Zipファイルの展開結果のコレクション。</returns>
    public IEnumerable<ArchiveEntryFile> Decompress()
    {
        var (_, entries) = Unzip(memory);
        return entries;
    }
    #endregion

    #region add-file
    /// <summary>
    /// アーカイブにファイルを追加します。
    /// </summary>
    /// <param name="filePath">追加するファイルのディスク上のファイルパス。</param>
    /// <param name="entryPath">アーカイブ内のファイルパス。</param>
    /// <param name="newComment">あらたに設定するコメント。未設定の場合はコメントを変更しません。</param>
    public void AddEntry(string filePath, string entryPath, string? newComment = null)
    {
        var entry = ArchiveEntryFile.Create(filePath, entryPath);
        AddEntry(entry, newComment);
    }

    /// <summary>
    /// アーカイブにファイルを追加します。
    /// </summary>
    /// <param name="entryFile">追加するファイルの情報。</param>
    /// <param name="newComment">あらたに設定するコメント。未設定の場合はコメントを変更しません。</param>
    public void AddEntry(in ArchiveEntryFile entryFile, string? newComment = null) =>
        AddEntries([entryFile], newComment);

    /// <summary>
    /// アーカイブにファイルを追加します。
    /// </summary>
    /// <param name="entryFiles">追加するファイル情報の一覧。</param>
    /// <param name="newComment">あらたに設定するコメント。未設定の場合はコメントを変更しません。</param>
    public void AddEntries(IEnumerable<ArchiveEntryFile> entryFiles, string? newComment = null) =>
        UpdateZip(memory, entryFiles, newComment);
    #endregion

    #region remove-file
    /// <summary>
    /// アーカイブからファイルを削除します。
    /// </summary>
    /// <param name="entryFile">削除するアーカイブ内のファイルの情報。</param>
    /// <param name="newComment">あらたに設定するコメント。未設定の場合はコメントを変更しません。</param>
    public void RemoveEntry(in ArchiveEntryFile entryFile, string? newComment = null) =>
        RemoveEntries([entryFile.EntryPath], newComment);

    /// <summary>
    /// アーカイブからファイルを削除します。
    /// </summary>
    /// <param name="entryFilePath">削除するアーカイブ内のファイルパス。</param>
    /// <param name="newComment">あらたに設定するコメント。未設定の場合はコメントを変更しません。</param>
    public void RemoveEntry(string entryFilePath, string? newComment = null) =>
        RemoveEntries([entryFilePath], newComment);

    /// <summary>
    /// アーカイブからファイルを削除します。
    /// </summary>
    /// <param name="entryFiles">削除するアーカイブ内のファイル情報の一覧。</param>
    /// <param name="newComment">あらたに設定するコメント。未設定の場合はコメントを変更しません。</param>
    public void RemoveEntries(IEnumerable<ArchiveEntryFile> entryFiles, string? newComment = null) =>
        RemoveEntries(entryFiles.Select(entry => entry.EntryPath), newComment);

    /// <summary>
    /// アーカイブからファイルを削除します。
    /// </summary>
    /// <param name="entryFilePaths">削除するアーカイブ内のファイルパスの一覧。</param>
    /// <param name="newComment">あらたに設定するコメント。未設定の場合はコメントを変更しません。</param>
    public void RemoveEntries(IEnumerable<string> entryFilePaths, string? newComment = null)
    {
        var (oldComment, oldEntries) = Unzip(memory);

        var newEntries = oldEntries.Reject(entry => entry.EntryPath.IsInclude(entryFilePaths));
        var comment = newComment ?? oldComment;

        // remake zip in memory.
        memory.Clear();
        CreateZip(memory, newEntries, comment);
    }
    #endregion

    #region change-file
    /// <summary>
    /// アーカイブのコメントを変更します。
    /// </summary>
    /// <param name="newComment">新しいコメント</param>
    public void ChangeComment(string newComment) =>
        UpdateComment(memory, newComment);
    #endregion

    #region common-util
    /// <summary>
    /// Zipファイルを作成します。
    /// </summary>
    /// <param name="stream">圧縮するデータのストリーム。</param>
    /// <param name="entryFiles">アーカイブ内のファイルの一覧。</param>
    /// <param name="newComment">新しいコメント。</param>
    private static void CreateZip(Stream stream, IEnumerable<ArchiveEntryFile> entryFiles, string? newComment)
    {
        // Zip binary output must be after disposing ZipArchive.
        using var archive = new ZipArchive(stream, ZipArchiveMode.Create, true);
        WriteArchive(archive, entryFiles, newComment);
    }

    /// <summary>
    /// Zipファイルを更新します。
    /// </summary>
    /// <param name="stream">Zipデータのストリーム。</param>
    /// <param name="entryFiles">アーカイブ内のファイルの一覧。</param>
    /// <param name="newComment">新しいコメント。</param>
    private static void UpdateZip(Stream stream, IEnumerable<ArchiveEntryFile> entryFiles, string? newComment)
    {
        // Zip binary output must be after disposing ZipArchive.
        using var archive = new ZipArchive(stream, ZipArchiveMode.Update, true);
        WriteArchive(archive, entryFiles, newComment);
    }

    /// <summary>
    /// コメントを更新します。
    /// </summary>
    /// <param name="stream">Zipデータのストリーム。</param>
    /// <param name="newComment">新しいコメント。</param>
    private static void UpdateComment(Stream stream, string newComment) =>
        UpdateZip(stream, [], newComment);

    /// <summary>
    /// アーカイブにデータを書き込みます。
    /// </summary>
    /// <param name="archive">書き込む対象のアーカイブ</param>
    /// <param name="entryFiles">書き込むアーカイブ内のファイルの一覧。</param>
    /// <param name="newComment">新しいコメント。</param>
    private static void WriteArchive(ZipArchive archive, IEnumerable<ArchiveEntryFile> entryFiles, string? newComment)
    {
        foreach (var entryFile in entryFiles)
        {
            var entry = archive.CreateEntry(entryFile.EntryPath, ArchiveCompressionLevel);
            if (!entryFile.IsDirectory)
            {
                entry.Write(entryFile.Read());
            }
        }

        if (newComment != null)
        {
            archive.Comment = newComment;
        }
    }

    /// <summary>
    /// Zipファイルを展開します。
    /// </summary>
    /// <param name="stream">Zipデータのストリーム。</param>
    /// <returns>展開したZipファイルのデータ。</returns>
    private static UnzippedArchive Unzip(Stream stream)
    {
        // Zip binary output must be after disposing ZipArchive.
        using var archive = new ZipArchive(stream, ZipArchiveMode.Read, true);
        var comment = archive.Comment;
        var entries = archive.Entries.Select(ArchiveEntryFile.ConvertFrom);
        return new(comment, [.. entries]);
    }

    /// <summary>
    /// Zipファイルを展開し、コメントを取得します。
    /// </summary>
    /// <param name="stream">Zipデータのストリーム。</param>
    /// <returns>読み取ったコメント。</returns>
    private static string GetComment(Stream stream)
    {
        using var archive = new ZipArchive(stream, ZipArchiveMode.Read, true);
        return archive.Comment;
    }
    #endregion
    #endregion
}
