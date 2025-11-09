namespace CarerExtension.Utilities.Threading.Tasks;

public class TaskInfo(Action<CancellationTokenSource> task) : IDisposable
{
    #region variable
    private readonly CancellationTokenSource cancellationTokenSource = new();
    #endregion

    #region property
    public Guid Id { get; } = Guid.NewGuid();
    #endregion

    #region method
    public void Dispose()
    {
        try
        {
            cancellationTokenSource.Dispose();
        }
        finally
        {
            GC.SuppressFinalize(this);
        }
    }

    public void Cancel() =>
        cancellationTokenSource.Cancel();

    public void ExecuteTask() =>
        task(cancellationTokenSource);
    #endregion
}
