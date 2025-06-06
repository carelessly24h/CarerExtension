namespace CarerExtension.Utilities.Mutex;

/// <summary>
/// ファイルロックを行うクラスです。
/// 任意のディレクトリにロックファイルを作成し、他のプロセスが同じロックファイルを使用している場合はロックできません。
/// </summary>
/// <param name="lockFilePath">ロックファイルのファイルパス</param>
public class FileLocker(string lockFilePath) : IDisposable
{
    #region variable
    /// <summary>
    /// 現在のインスタンスが排他制御を保持しているかどうかを示します。
    /// </summary>
    private bool locked = false;

    /// <summary>
    /// ロックファイルのストリーム。
    /// </summary>
    private FileStream? stream = null;
    #endregion

    #region method
    /// <summary>
    /// 解放処理を行います。
    /// </summary>
    public void Dispose()
    {
        try
        {
            stream?.Dispose();

            if (locked)
            {
                File.Delete(lockFilePath);
            }
        }
        catch (IOException e) when (e is DirectoryNotFoundException || e is FileNotFoundException)
        {
            // no processing.
        }
        finally
        {
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// 排他制御を取得します。
    /// </summary>
    /// <param name="lockFilePath">ロックファイルのファイルパス。</param>
    /// <returns>排他制御を取得した<see cref="FileLocker"/>のインスタンス。</returns>
    /// <exception cref="FileLockException">排他制御の取得に失敗した場合にスローされます。</exception>
    public static FileLocker Lock(string lockFilePath)
    {
        var mutex = new FileLocker(lockFilePath);
        if (!mutex.Lock())
        {
            throw new FileLockException();
        }
        return mutex;
    }

    /// <summary>
    /// 排他制御を取得します。
    /// </summary>
    /// <returns>
    /// 排他制御を取得した場合は<see cref="true"/>。
    /// そうでない場合は<see cref="false"/>。
    /// </returns>
    public bool Lock()
    {
        try
        {
            stream ??= new FileStream(lockFilePath, FileMode.Create, FileAccess.Write, FileShare.None);
            locked = true;
        }
        catch (IOException)
        {
            locked = false;
            // just in case, it's dispose.
            Dispose();
        }

        return locked;
    }
    #endregion
}
