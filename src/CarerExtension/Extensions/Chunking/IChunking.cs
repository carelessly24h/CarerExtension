namespace CarerExtension.Extensions.Chunking;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IChunking<out T> : IEnumerable<T>, IEnumerable
{
    /// <summary>
    /// 
    /// </summary>
    Index Start { get; }

    /// <summary>
    /// 
    /// </summary>
    int Length { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    int Count() => Length;
}
