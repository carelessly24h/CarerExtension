namespace CarerExtension.Utilities.Mutex;

public class FileLocker(string lockFilePath) : IDisposable
{
    #region variable
    private bool locked = false;

    private FileStream? stream = null;
    #endregion

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

    public static FileLocker Lock(string lockFilePath)
    {
        var mutex = new FileLocker(lockFilePath);
        if (!mutex.Lock())
        {
            throw new FileLockException();
        }
        return mutex;
    }

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
}
