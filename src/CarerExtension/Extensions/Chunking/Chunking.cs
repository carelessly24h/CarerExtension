namespace CarerExtension.Extensions.Chunking;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="values"></param>
/// <param name="start"></param>
public sealed class Chunking<T>(IEnumerable<T> values, Index start) : IChunking<T>
{
    /// <summary>
    /// 
    /// </summary>
    public Index Start { get; } = start;

    /// <summary>
    /// 
    /// </summary>
    public int Length { get; } = values.Count();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="values"></param>
    /// <param name="startIndex"></param>
    /// <returns></returns>
    public static IChunking<T> Create(IEnumerable<T> values, int startIndex = 0) =>
        new Chunking<T>(values, startIndex);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="values"></param>
    /// <param name="startIndex"></param>
    /// <param name="endIndex"></param>
    /// <returns></returns>
    public static IChunking<T> Create(IEnumerable<T> values, int startIndex, int endIndex)
    {
        var count = endIndex - startIndex + 1;
        var v = values.Skip(startIndex).Take(count);
        return new Chunking<T>(v, startIndex);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerator<T> GetEnumerator() => values.GetEnumerator();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
