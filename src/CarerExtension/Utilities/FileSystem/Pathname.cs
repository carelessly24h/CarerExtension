namespace CarerExtension.Utilities.FileSystem;

/// <summary>
/// パス名を扱うクラスです。
/// Pathname オブジェクトはパス名を表しており、ファイルやディレクトリそのものを表してはいません。
/// 存在しないファイルのパス名も扱えます。絶対パスも相対パスも扱えます。
/// </summary>
/// <remarks>ruby like.</remarks>
/// <param name="path">パス名を表す文字列。</param>
public readonly struct Pathname(string path)
{
    #region property
    /// <summary>
    /// 管理しているパス名を表す文字列です。
    /// </summary>
    private readonly string FilePath => path;

    /// <summary>
    /// ディスク上に、パス名の示すファイルやディレクトリが存在するかどうかを示すプロパティです。
    /// </summary>
    public bool Exists => IsDirectory || IsFile;

    /// <summary>
    /// パス名から拡張子を取得します。
    /// </summary>
    public string Extension => Path.GetExtension(path);

    /// <summary>
    /// パス名からファイル名を取得します。
    /// </summary>
    public string FileName => Path.GetFileName(path);

    /// <summary>
    /// フルパスを取得します。
    /// </summary>
    public string FullPath => Path.GetFullPath(path);

    /// <summary>
    /// パス名がディレクトリを指しているかどうかを示します。
    /// </summary>
    public bool IsDirectory => Directory.Exists(path);

    /// <summary>
    /// パス名がファイルを指しているかどうかを示します。
    /// </summary>
    public bool IsFile => File.Exists(path);
    #endregion

    #region constructor
    /// <summary>
    /// パス名を扱うクラスです。
    /// Pathname オブジェクトはパス名を表しており、ファイルやディレクトリそのものを表してはいません。
    /// 存在しないファイルのパス名も扱えます。絶対パスも相対パスも扱えます。
    /// </summary>
    /// <param name="paths">パスを構成する文字列のコレクション。</param>
    public Pathname(params string[] paths) : this(Path.Combine(paths)) { }
    #endregion

    #region operator
    public static Pathname operator +(in Pathname path1, in Pathname path2) =>
        path1.Combine(path2);

    public static Pathname operator +(in Pathname path1, string path2) =>
        path1.Combine(path2);

    public static Pathname operator /(in Pathname path1, in Pathname path2) =>
        path1 + path2;

    public static Pathname operator /(in Pathname path1, string path2) =>
        path1 + path2;

    public static bool operator ==(in Pathname path1, in Pathname path2) =>
        path1.FilePath.Equals(path2.FilePath);

    public static bool operator ==(in Pathname path1, string path2) =>
        path1.FilePath.Equals(path2);

    public static bool operator ==(string path1, in Pathname path2) =>
        path1.Equals(path2.FilePath);

    public static bool operator !=(in Pathname path1, in Pathname path2) =>
        !(path1 == path2);

    public static bool operator !=(in Pathname path1, string path2) =>
        !(path1 == path2);

    public static bool operator !=(string path1, in Pathname path2) =>
        !(path1 == path2);
    #endregion

    #region method
    #region Children
    /// <summary>
    /// ディレクトリの子要素（ファイルとディレクトリ）を列挙します。
    /// </summary>
    /// <param name="searchOption">検索操作に現在のディレクトリのみを含めるのか、またはすべてのサブディレクトリを含めるのかを指定する列挙値の1つ。</param>
    /// <returns>ディレクトリの子要素のコレクション。</returns>
    public IEnumerable<Pathname> Children(SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        foreach (var d in EnumerateDirectories(searchOption))
        {
            yield return d;
        }

        foreach (var f in EnumerateFiles(searchOption))
        {
            yield return f;
        }
    }

    /// <summary>
    /// パスの一部、または全部が正規表現に一致するディレクトリの子要素（ファイルとディレクトリ）を検索して列挙します。
    /// </summary>
    /// <param name="pattern">検索する正規表現のパターン。</param>
    /// <param name="searchOption">検索操作に現在のディレクトリのみを含めるのか、またはすべてのサブディレクトリを含めるのかを指定する列挙値の1つ。</param>
    /// <returns>検索結果に一致する子要素のコレクション。</returns>
    public IEnumerable<Pathname> Children(string pattern, SearchOption searchOption = SearchOption.TopDirectoryOnly) =>
        Children(new Regex(pattern), searchOption);

    /// <summary>
    /// パスの一部、または全部が正規表現に一致するディレクトリの子要素（ファイルとディレクトリ）を検索して列挙します。
    /// </summary>
    /// <param name="pattern">検索する正規表現。</param>
    /// <param name="searchOption">検索操作に現在のディレクトリのみを含めるのか、またはすべてのサブディレクトリを含めるのかを指定する列挙値の1つ。</param>
    /// <returns>検索結果に一致する子要素のコレクション。</returns>
    public IEnumerable<Pathname> Children(Regex pattern, SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        foreach (var d in EnumerateDirectories(pattern, searchOption))
        {
            yield return d;
        }

        foreach (var f in EnumerateFiles(pattern, searchOption))
        {
            yield return f;
        }
    }
    #endregion

    #region Combine
    /// <summary>
    /// 指定されたパスを現在のパスに結合します。
    /// </summary>
    /// <param name="childPath">子要素のパス</param>
    /// <returns>パスを結合した<see cref="Pathname"/>のインスタンス。</returns>
    public Pathname Combine(Pathname childPath) => Combine(childPath.FilePath);

    /// <summary>
    /// 文字列を現在のパスに結合します。
    /// </summary>
    /// <param name="paths">子要素のパス</param>
    /// <returns>パスを結合した<see cref="Pathname"/>のインスタンス。</returns>
    public Pathname Combine(in Span<string> paths) => new([path, .. paths]);

    /// <summary>
    /// 文字列を現在のパスに結合します。
    /// </summary>
    /// <param name="childPath">子要素のパス</param>
    /// <returns>パスを結合した<see cref="Pathname"/>のインスタンス。</returns>
    public Pathname Combine(string childPath) => new(path, childPath);

    /// <summary>
    /// 文字列を現在のパスに結合します。
    /// </summary>
    /// <param name="paths">子要素のパス</param>
    /// <returns>パスを結合した<see cref="Pathname"/>のインスタンス。</returns>
    public Pathname Combine(params string[] paths) => new(path, Path.Combine(paths));
    #endregion

    #region CopyTo
    /// <summary>
    /// ファイル、またはディレクトリを指定されたパスにコピーします。
    /// </summary>
    /// <param name="destFileName">コピー先のパス</param>
    public void CopyTo(in Pathname destFileName) =>
        CopyTo(destFileName.FullPath);

    /// <summary>
    /// ファイル、またはディレクトリを指定されたパスにコピーします。
    /// </summary>
    /// <param name="destFileName">コピー先のパス</param>
    public void CopyTo(string destFileName) =>
        File.Copy(FullPath, destFileName);

    /// <summary>
    /// ファイル、またはディレクトリを指定されたパスにコピーします。
    /// </summary>
    /// <param name="destFileName">コピー先のパス</param>
    /// <param name="overwrite">コピー先に同名のファイルが存在する場合、上書きするかどうかを示す値。</param>
    public void CopyTo(in Pathname destFileName, bool overwrite) =>
        CopyTo(destFileName.FullPath, overwrite);

    /// <summary>
    /// ファイル、またはディレクトリを指定されたパスにコピーします。
    /// </summary>
    /// <param name="destFileName">コピー先のパス</param>
    /// <param name="overwrite">コピー先に同名のファイルが存在する場合、上書きするかどうかを示す値。</param>
    public void CopyTo(string destFileName, bool overwrite) =>
        File.Copy(FullPath, destFileName, overwrite);
    #endregion

    /// <summary>
    /// 現在のパス名が指すディレクトリを作成します。
    /// </summary>
    /// <returns>現在のパスを表す<see cref="Pathname"/></returns>
    public Pathname CreateDirectory()
    {
        Directory.CreateDirectory(FullPath);
        return this;
    }

    #region Delete
    /// <summary>
    /// 現在のパス名が指すファイルまたはディレクトリを削除します。
    /// </summary>
    public void Delete()
    {
        if (IsDirectory)
        {
            DeleteDirectory();
        }
        if (IsFile)
        {
            DeleteFile();
        }
    }

    /// <summary>
    /// 現在のパス名が指すディレクトリを削除します。
    /// </summary>
    public void DeleteDirectory() => DeleteDirectory(false);

    /// <summary>
    /// 現在のパス名が指すディレクトリを削除します。
    /// </summary>
    /// <param name="recursive">子要素を含めてすべて削除するかどうかを示す値。</param>
    public void DeleteDirectory(bool recursive) => Directory.Delete(FullPath, recursive);

    /// <summary>
    /// 現在のパス名が指すファイルを削除します。
    /// </summary>
    public void DeleteFile() => File.Delete(FullPath);
    #endregion

    /// <summary>
    /// ディレクトリの中身をすべて削除します。
    /// </summary>
    /// <exception cref="DirectoryNotFoundException">パスがディレクトリでない、またはディレクトリが存在しない場合にスローされます。</exception>
    public void Empty()
    {
        if (!IsDirectory)
        {
            throw new DirectoryNotFoundException("directory not found. or file was specified.");
        }

        foreach (var file in EnumerateFiles())
        {
            file.Delete();
        }
        foreach (var directory in EnumerateDirectories())
        {
            directory.Delete();
        }
    }

    #region EnumerateDirectories
    /// <summary>
    /// 現在のパスに存在するディレクトリの完全名から成る、列挙可能なコレクションを返します。
    /// オプションでサブディレクトリを検索対象にできます。
    /// </summary>
    /// <param name="searchOption">検索操作に現在のディレクトリのみを含めるのか、またはすべてのサブディレクトリを含めるのかを指定する列挙値の1つ。</param>
    /// <returns>現在のパスで指定したディレクトリ内にあり、指定した検索オプションと一致する、ディレクトリの完全名 (パスを含む) の列挙可能なコレクション。</returns>
    public IEnumerable<Pathname> EnumerateDirectories(SearchOption searchOption = SearchOption.TopDirectoryOnly) =>
        Directory.EnumerateDirectories(FullPath, "*", searchOption).Select(p => new Pathname(p));

    /// <summary>
    /// 現在のパスから、検索パターンに一致するディレクトリの完全名から成る、列挙可能なコレクションを返します。
    /// オプションでサブディレクトリを検索対象にできます。
    /// </summary>
    /// <param name="pattern">
    /// 現在のパスのディレクトリの名前と照合する検索文字列。
    /// このパラメータは正規表現をサポートします。
    /// </param>
    /// <param name="searchOption">検索操作に現在のディレクトリのみを含めるのか、またはすべてのサブディレクトリを含めるのかを指定する列挙値の1つ。</param>
    /// <returns>現在のパスで指定したディレクトリ内にあり、指定した検索パターンおよび検索オプションと一致する、ディレクトリの完全名 (パスを含む) の列挙可能なコレクション。</returns>
    public IEnumerable<Pathname> EnumerateDirectories(string pattern, SearchOption searchOption = SearchOption.TopDirectoryOnly) =>
        EnumerateDirectories(new Regex(pattern), searchOption);

    /// <summary>
    /// 現在のパスから、検索パターンに一致するディレクトリの完全名から成る、列挙可能なコレクションを返します。
    /// オプションでサブディレクトリを検索対象にできます。
    /// </summary>
    /// <param name="pattern">現在のパスのディレクトリの名前と照合する正規表現。</param>
    /// <param name="searchOption">検索操作に現在のディレクトリのみを含めるのか、またはすべてのサブディレクトリを含めるのかを指定する列挙値の1つ。</param>
    /// <returns>現在のパスで指定したディレクトリ内にあり、指定した検索パターンおよび検索オプションと一致する、ディレクトリの完全名 (パスを含む) の列挙可能なコレクション。</returns>
    public IEnumerable<Pathname> EnumerateDirectories(Regex pattern, SearchOption searchOption = SearchOption.TopDirectoryOnly) =>
        EnumerateDirectories(searchOption).Where(d => pattern.IsMatch(d.FileName));
    #endregion

    #region EnumerateFiles
    /// <summary>
    /// 指定されたパスに存在するファイルの完全名から成る、列挙可能なコレクションを返します。
    /// オプションでサブディレクトリを検索対象にできます。
    /// </summary>
    /// <param name="searchOption">検索操作に現在のディレクトリのみを含めるのか、またはすべてのサブディレクトリを含めるのかを指定する列挙値の1つ。</param>
    /// <returns>path で指定したディレクトリ内にあり、指定した検索オプションと一致する、ファイルの完全名 (パスを含む) の列挙可能なコレクション。</returns>
    public IEnumerable<Pathname> EnumerateFiles(SearchOption searchOption = SearchOption.TopDirectoryOnly) =>
        Directory.EnumerateFiles(FullPath, "*", searchOption).Select(p => new Pathname(p));

    /// <summary>
    /// 指定されたパスから、検索パターンに一致するファイルの完全名から成る、列挙可能なコレクションを返します。
    /// オプションでサブディレクトリを検索対象にできます。
    /// </summary>
    /// <param name="pattern">
    /// 現在のパスのファイル名と対応させる検索文字列。
    /// このパラメータは正規表現をサポートします。
    /// </param>
    /// <param name="searchOption">検索操作に現在のディレクトリのみを含めるのか、またはすべてのサブディレクトリを含めるのかを指定する列挙値の1つ。</param>
    /// <returns>path で指定したディレクトリ内にあり、指定した検索パターンおよび検索オプションと一致する、ファイルの完全名 (パスを含む) の列挙可能なコレクション。</returns>
    public IEnumerable<Pathname> EnumerateFiles(string pattern, SearchOption searchOption = SearchOption.TopDirectoryOnly) =>
        EnumerateFiles(new Regex(pattern), searchOption);

    /// <summary>
    /// 指定されたパスから、検索パターンに一致するファイルの完全名から成る、列挙可能なコレクションを返します。
    /// オプションでサブディレクトリを検索対象にできます。
    /// </summary>
    /// <param name="pattern">現在のパスのファイル名と対応させる正規表現。</param>
    /// <param name="searchOption">検索操作に現在のディレクトリのみを含めるのか、またはすべてのサブディレクトリを含めるのかを指定する列挙値の1つ。</param>
    /// <returns>path で指定したディレクトリ内にあり、指定した検索パターンおよび検索オプションと一致する、ファイルの完全名 (パスを含む) の列挙可能なコレクション。</returns>
    public IEnumerable<Pathname> EnumerateFiles(Regex pattern, SearchOption searchOption = SearchOption.TopDirectoryOnly) =>
        EnumerateFiles(searchOption).Where(f => pattern.IsMatch(f.FileName));
    #endregion

    /// <summary>
    /// 2 つのオブジェクト インスタンスが等しいかどうかを判断します。
    /// </summary>
    /// <param name="obj">現在のオブジェクトと比較するオブジェクト。</param>
    /// <returns>
    /// 指定したオブジェクトが現在のオブジェクトと等しい場合は<see langword="true"/>。
    /// それ以外の場合は、<see langword="false"/>です。
    /// </returns>
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        // Either a or b is valid.
        int? hashCode = obj switch
        {
            Pathname p => p.GetHashCode(),
            string s => s.GetHashCode(),
            _ => null,
        };

        if (hashCode is int code)
        {
            return GetHashCode() == code;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 既定のハッシュ関数として機能します。
    /// </summary>
    /// <returns>現在のオブジェクトのハッシュコード。</returns>
    public override int GetHashCode() => path.GetHashCode();

    #region MoveTo
    /// <summary>
    /// ファイルまたはディレクトリ、およびその内容を新しい場所に移動します。
    /// </summary>
    /// <param name="destDirName">現在のファイルパス。または、その子要素の新しい場所へのパス。 現在のパスがファイルの場合は、<paramref name="destDirName"/>もファイル名にする必要があります。</param>
    public void MoveTo(in Pathname destDirName) => MoveTo(destDirName.FullPath);

    /// <summary>
    /// ファイルまたはディレクトリ、およびその内容を新しい場所に移動します。
    /// </summary>
    /// <param name="destDirName">現在のファイルパス。または、その子要素の新しい場所へのパス。 現在のパスがファイルの場合は、<paramref name="destDirName"/>もファイル名にする必要があります。</param>
    public void MoveTo(string destDirName) => Directory.Move(FullPath, destDirName);
    #endregion

    /// <summary>
    /// 現在のパスの親ディレクトリを取得します。
    /// </summary>
    /// <returns>親ディレクトリのパス。</returns>
    /// <exception cref="NullReferenceException">親ディレクトリが存在しない場合、スローされます。</exception>
    public Pathname Parent()
    {
        var parent = Path.GetDirectoryName(FullPath) ??
            throw new NullReferenceException("The managed path is null.");
        return new(parent);
    }

    #region Read
    /// <summary>
    /// バイナリファイルとして、すべてのバイトを読み取ります。
    /// </summary>
    /// <returns>読み取ったファイルの内容。</returns>
    public IEnumerable<byte> ReadAllBytes() => File.ReadAllBytes(FullPath);

    /// <summary>
    /// テキストファイルとして、すべてのテキストを読み取ります。
    /// エンコードはUTF-8を使用します。
    /// </summary>
    /// <returns>読み取ったファイルの内容。</returns>
    public string ReadAllText() => File.ReadAllText(FilePath, Encoding.UTF8);

    /// <summary>
    /// テキストファイルとして、すべてのテキストを読み取ります。
    /// </summary>
    /// <param name="encoding">ファイルのエンコード。</param>
    /// <returns>読み取ったファイルの内容。</returns>
    public string ReadAllText(Encoding encoding) => File.ReadAllText(FilePath, encoding);
    #endregion

    /// <summary>
    /// パス名を表す文字列を返します。
    /// </summary>
    /// <returns>パス名を表す文字列。</returns>
    public override string ToString() => path;

    #region Write
    /// <summary>
    /// テキストファイルとして、指定された内容を書き込みます。
    /// エンコードはUTF-8を使用します。
    /// </summary>
    /// <param name="contents">ファイルに書き込む内容。</param>
    public void Write(string contents) => Write(contents, Encoding.UTF8);

    /// <summary>
    /// テキストファイルとして、指定された内容を書き込みます。
    /// </summary>
    /// <param name="contents">ファイルに書き込む内容。</param>
    /// <param name="encoding">ファイルのエンコード。</param>
    public void Write(string contents, Encoding encoding) => File.WriteAllText(FullPath, contents, encoding);

    /// <summary>
    /// バイナリファイルとして、指定されたバイト列を書き込みます。
    /// </summary>
    /// <param name="buffer">ファイルに書き込む内容。</param>
    public void Write(IEnumerable<byte> buffer) => File.WriteAllBytes(FullPath, [.. buffer]);

    /// <summary>
    /// バイナリファイルとして、指定されたバイト列を書き込みます。
    /// </summary>
    /// <param name="buffer">ファイルに書き込む内容。</param>
    public void Write(ReadOnlySpan<byte> buffer)
    {
        using var stream = new FileStream(FullPath, FileMode.OpenOrCreate);
        stream.Write(buffer);
    }
    #endregion
    #endregion
}
