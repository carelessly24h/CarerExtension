namespace CarerExtension.Extensions.Chunking;

/// <summary>
/// オブジェクトのコレクションの一部のチャンクを表します。
/// </summary>
/// <typeparam name="T">チャンクの要素の型。この型パラメータは共変です。</typeparam>
public sealed class Chunking<T> : IChunking<T>
{
    #region properties
    /// <summary>
    /// チャンクを表すコレクションを取得します。
    /// </summary>
    /// <remarks>ruby like.</remarks>
    private readonly IEnumerable<T> values;

    /// <summary>
    /// コレクション全体に対するチャンクの開始位置を表す0から始まるインデックスを取得します。
    /// </summary>
    public Index Start { get; }

    /// <summary>
    /// チャンクの長さを取得します。
    /// </summary>
    public int Length { get; }
    #endregion

    #region constructors
    /// <summary>
    /// コレクションの一部のチャンクを表す新しいインスタンスを初期化します。
    /// </summary>
    /// <param name="values">チャンクを表すコレクション。</param>
    /// <param name="start">コレクション全体に対するチャンクの開始位置を表す0から始まるインデックス。</param>
    internal Chunking(IEnumerable<T> values, Index start)
    {
        this.values = values;
        this.Start = start;
        this.Length = values.Count();
    }
    #endregion

    #region methods
    /// <summary>
    /// コレクションの一部のチャンクを表す新しいインスタンスを初期化します。
    /// </summary>
    /// <param name="values">チャンクを表すコレクション。</param>
    /// <param name="startIndex">コレクション全体に対するチャンクの開始位置を表す0から始まるインデックス。</param>
    /// <returns>切り取られたコレクションを表すチャンク。</returns>
    internal static IChunking<T> Create(IEnumerable<T> values, int startIndex = 0) =>
        new Chunking<T>(values, startIndex);

    /// <summary>
    /// コレクションの一部のチャンクを表す新しいインスタンスを初期化します。
    /// </summary>
    /// <param name="values">チャンクを表すコレクション。</param>
    /// <param name="startIndex">コレクション全体に対するチャンクの開始位置を表す0から始まるインデックス。</param>
    /// <param name="endIndex">コレクション全体に対するチャンクの終了位置を表す0から始まるインデックス。</param>
    /// <returns>切り取られたコレクションの一部を表すチャンク。</returns>
    internal static IChunking<T> Create(IEnumerable<T> values, int startIndex, int endIndex)
    {
        var count = endIndex - startIndex + 1;
        var v = values.Skip(startIndex).Take(count);
        return new Chunking<T>(v, startIndex);
    }

    /// <summary>
    /// コレクションを反復処理する列挙子を返します。
    /// </summary>
    /// <returns>コレクションを反復処理するために使用できる IEnumerator オブジェクト。</returns>
    public IEnumerator<T> GetEnumerator() => values.GetEnumerator();

    /// <summary>
    /// コレクションを反復処理する列挙子を返します。
    /// </summary>
    /// <returns>コレクションを反復処理するために使用できる IEnumerator オブジェクト。</returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    #endregion
}
