namespace CarerExtension.Utilities.Mutex;

public class FileLockException : Exception
{
    public FileLockException() : base("Lock failed. Another process already has exclusive control.") { }
}
