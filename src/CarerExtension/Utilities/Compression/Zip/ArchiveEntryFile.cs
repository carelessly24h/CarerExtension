using CarerExtension.Extensions;

namespace CarerExtension.Utilities.Compression.Zip;

/// <summary>
/// アーカイブ内のファイル情報を包括的に扱うためのクラス。
/// </summary>
/// <param name="FilePath">ディスク上のファイルパス。</param>
/// <param name="EntryPath">アーカイブ内のファイルパス。</param>
/// <param name="IsDirectory">指定されたパスがディレクトリかどうかを示す値。</param>
public record struct ArchiveEntryFile(string FilePath, string EntryPath, bool IsDirectory)
{
    #region variable
    /// <summary>
    /// 管理しているファイルのデータ。
    /// </summary>
    private byte[]? entryFileContentsCache;
    #endregion

    #region method
    /// <summary>
    /// ディスク上のファイルを指定して、アーカイブに追加するためのファイルを管理するクラスを作成します。
    /// </summary>
    /// <param name="filePath">ディスク上のファイルパス。</param>
    /// <returns>アーカイブに追加する</returns>
    public static ArchiveEntryFile Create(string filePath)
    {
        var entryPath = GetEntryPath(filePath);
        var isDirectory = IsDirecotry(filePath);
        return new(filePath, entryPath, isDirectory);
    }

    /// <summary>
    /// アーカイブ内のファイル情報を包括的に扱うためのクラスを作成します。
    /// </summary>
    /// <param name="filePath">ディスク上のファイルパス。</param>
    /// <param name="entryPath">アーカイブ内のパス。</param>
    /// <returns></returns>
    public static ArchiveEntryFile Create(string filePath, string entryPath)
    {
        var isDirectory = IsDirecotry(filePath);
        return new(filePath, entryPath, isDirectory);
    }

    /// <summary>
    /// Zipアーカイブ内の圧縮ファイルを、管理用のクラスに変換します。
    /// </summary>
    /// <param name="entry">Zip</param>
    /// <returns></returns>
    public static ArchiveEntryFile ConvertFrom(ZipArchiveEntry entry)
    {
        var entryPath = entry.FullName;
        var isDirectory = entry.Name == "";
        return new(entryPath, entryPath, isDirectory)
        {
            entryFileContentsCache = entry.Read(),
        };
    }

    /// <summary>
    /// 指定されたパスがディレクトリかどうかを判定します。
    /// </summary>
    /// <param name="path">ディレクトリかどうかを調べるパス。</param>
    /// <returns>
    /// <paramref name="path"/>がディレクトリの場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    private static bool IsDirecotry(string path)
    {
        var info = new DirectoryInfo(path);
        return info.Exists || Path.GetFileName(path) == "";
    }

    /// <summary>
    /// 指定されたパスを元に、アーカイブ内のファイルパスを作成します。
    /// </summary>
    /// <param name="path">ファイルパス。</param>
    /// <returns>アーカイブ内のファイルパス。</returns>
    private static string GetEntryPath(string path)
    {
        if (IsDirecotry(path))
        {
            // dir name is end with '/'.
            // this matches the ZipArchiveEntry.
            var info = new DirectoryInfo(path);
            return $"{info.Name}/";
        }
        else
        {
            return Path.GetFileName(path);
        }
    }

    /// <summary>
    /// ディスク上のファイルのデータを読み取ります。
    /// 読み取ったデータはキャッシュし、次回以降はキャッシュからデータを取得します。
    /// </summary>
    /// <returns>読み取ったファイルのデータ。</returns>
    public byte[] Read()
    {
        entryFileContentsCache ??= Read(FilePath);
        return entryFileContentsCache;
    }

    /// <summary>
    /// ディスク上のファイルのデータを読み取ります。
    /// </summary>
    /// <param name="filePath">読み取るファイルのパス。</param>
    /// <returns>読み取ったファイルのデータ。</returns>
    private static byte[] Read(string filePath) => File.ReadAllBytes(filePath);

    /// <summary>
    /// アーカイブ内のファイルのデータをディスクに書き込みます。
    /// </summary>
    /// <param name="destinationDirectoryPath">出力先のルートディレクトパス。</param>
    public readonly void Write(string destinationDirectoryPath)
    {
        ThrowIfNull(entryFileContentsCache);

        var entryFullPath = Path.Combine(destinationDirectoryPath, EntryPath);
        Write(entryFullPath, entryFileContentsCache);
    }

    /// <summary>
    /// 指定されたオブジェクトが<see cref="null"/>の場合は、例外を投げます。
    /// </summary>
    /// <param name="obj">検査するオブジェクト。</param>
    /// <exception cref="InvalidOperationException">オブジェクトが<see cref="null"/>の場合にスローされる例外。</exception>
    private static void ThrowIfNull([NotNull] object? obj) =>
        _ = obj ?? throw new InvalidOperationException("A required parameter is null.");

    /// <summary>
    /// アーカイブ内のファイルのデータをディスクに書き込みます。
    /// パスがディレクトリの場合、ディレクトリを作成します。
    /// </summary>
    /// <param name="destinationPath">出力パス。</param>
    /// <param name="buffer">書き込むデータ。</param>
    private readonly void Write(string destinationPath, byte[] buffer)
    {
        if (IsDirectory)
        {
            Directory.CreateDirectory(destinationPath);
        }
        else
        {
            // match archive directory structure.
            CreateParentDirectory(destinationPath);
            File.WriteAllBytes(destinationPath, buffer);
        }
    }

    /// <summary>
    /// 指定されたパスの親ディレクトリを作成します。
    /// </summary>
    /// <param name="entryFileFullPath">ディスク上の位置を示すパス。</param>
    /// <exception cref="InvalidOperationException">親ディレクトリの取得に失敗した場合にスローされる例外。</exception>
    private static void CreateParentDirectory(string entryFileFullPath)
    {
        var directoryPath = Path.GetDirectoryName(entryFileFullPath);
        if (directoryPath != null)
        {
            Directory.CreateDirectory(directoryPath);
        }
        else
        {
            throw new InvalidOperationException("Cannot create directory for entry file path: " + entryFileFullPath);
        }
    }
    #endregion
}
