namespace CarerExtension.Utilities.Mutex;

/// <summary>
/// 排他制御を取得できなかった場合にスローされる例外です。
/// </summary>
public class FileLockException : Exception
{
    /// <summary>
    /// 排他制御を取得したときにスローされる例外です。
    /// </summary>
    public FileLockException() : base("Lock failed. Another process already has exclusive control.") { }
}
