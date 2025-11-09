namespace CarerExtension.Utilities.Threading.Tasks.Exceptions;

public class ParallelProcessorException : Exception
{
    private const string DefaultMessage = "An error occurred during threading operations.";

    #region constructor
    public ParallelProcessorException()
    {
    }

    public ParallelProcessorException(string? message) : base(message)
    {
    }

    public ParallelProcessorException(Exception? innerException) : base(DefaultMessage, innerException)
    {
    }

    public ParallelProcessorException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
    #endregion
}
