namespace CarerExtension.Utilities.FileSystem;

public readonly struct Pathname(string path)
{
    #region property
    private readonly string FilePath => path;

    public bool Exists => IsDirectory || IsFile;

    public string Extension => Path.GetExtension(path);

    public string FileName => Path.GetFileName(path);

    public string FullPath => Path.GetFullPath(path);

    public bool IsDirectory => Directory.Exists(path);

    public bool IsFile => File.Exists(path);
    #endregion

    #region constructor
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
    public IEnumerable<Pathname> Children(SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        foreach (var f in EnumerateDirectories(searchOption))
        {
            yield return f;
        }

        foreach (var f in EnumerateFiles(searchOption))
        {
            yield return f;
        }
    }

    public IEnumerable<Pathname> Children(string pattern, SearchOption searchOption = SearchOption.TopDirectoryOnly) =>
        Children(new Regex(pattern), searchOption);

    public IEnumerable<Pathname> Children(Regex pattern, SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        foreach (var f in EnumerateDirectories(pattern, searchOption))
        {
            yield return f;
        }

        foreach (var f in EnumerateFiles(pattern, searchOption))
        {
            yield return f;
        }
    }
    #endregion

    #region Combine
    public Pathname Combine(Pathname childPath) => Combine(childPath.FilePath);

    public Pathname Combine(Span<string> paths) => new([path, .. paths]);

    public Pathname Combine(string childPath) => new(path, childPath);

    public Pathname Combine(params string[] paths) => new(path, Path.Combine(paths));
    #endregion

    #region CopyTo
    public void CopyTo(in Pathname destFileName) =>
        CopyTo(destFileName.FullPath);

    public void CopyTo(string destFileName) =>
        File.Copy(FullPath, destFileName);

    public void CopyTo(in Pathname destFileName, bool overwrite) =>
        CopyTo(destFileName.FullPath, overwrite);

    public void CopyTo(string destFileName, bool overwrite) =>
        File.Copy(FullPath, destFileName, overwrite);
    #endregion

    public Pathname CreateDirectory()
    {
        Directory.CreateDirectory(FullPath);
        return this;
    }

    #region Delete
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

    public void DeleteDirectory() => DeleteDirectory(false);

    public void DeleteDirectory(bool recursive) => Directory.Delete(FullPath, recursive);

    public void DeleteFile() => File.Delete(FullPath);
    #endregion

    public void Empty()
    {
        if (IsFile)
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
    public IEnumerable<Pathname> EnumerateDirectories(SearchOption searchOption = SearchOption.TopDirectoryOnly) =>
        Directory.EnumerateDirectories(FullPath, "*", searchOption).Select(p => new Pathname(p));

    public IEnumerable<Pathname> EnumerateDirectories(string pattern, SearchOption searchOption = SearchOption.TopDirectoryOnly) =>
        EnumerateDirectories(new Regex(pattern), searchOption);

    public IEnumerable<Pathname> EnumerateDirectories(Regex pattern, SearchOption searchOption = SearchOption.TopDirectoryOnly) =>
        EnumerateDirectories(searchOption).Where(d => pattern.IsMatch(d.FileName));
    #endregion

    #region EnumerateFiles
    public IEnumerable<Pathname> EnumerateFiles(SearchOption searchOption = SearchOption.TopDirectoryOnly) =>
        Directory.EnumerateFiles(FullPath, "*", searchOption).Select(p => new Pathname(p));

    public IEnumerable<Pathname> EnumerateFiles(string pattern, SearchOption searchOption = SearchOption.TopDirectoryOnly) =>
        EnumerateFiles(new Regex(pattern), searchOption);

    public IEnumerable<Pathname> EnumerateFiles(Regex pattern, SearchOption searchOption = SearchOption.TopDirectoryOnly) =>
        EnumerateFiles(searchOption).Where(f => pattern.IsMatch(f.FileName));
    #endregion

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
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

    public override int GetHashCode() => path.GetHashCode();

    #region MoveTo
    public void MoveTo(in Pathname destDirName) => MoveTo(destDirName.FullPath);

    public void MoveTo(string destDirName) => Directory.Move(FullPath, destDirName);
    #endregion

    public Pathname Parent()
    {
        var parent = Path.GetDirectoryName(FullPath) ??
            throw new NullReferenceException("The managed path is null.");
        return new(parent);
    }

    public override string ToString() => path;

    #region Write
    public void Write(string contents) => Write(contents, Encoding.UTF8);

    public void Write(string contents, Encoding encoding) => File.WriteAllText(FullPath, contents, encoding);

    public void Write(IEnumerable<byte> buffer) => File.WriteAllBytes(FullPath, buffer.ToArray());

    public void Write(ReadOnlySpan<byte> buffer)
    {
        using var stream = new FileStream(FullPath, FileMode.OpenOrCreate);
        stream.Write(buffer);
    }
    #endregion
    #endregion
}
