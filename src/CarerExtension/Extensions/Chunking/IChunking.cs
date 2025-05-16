namespace CarerExtension.Extensions.Chunking;

/// <summary>
/// オブジェクトのコレクションの一部のチャンクを表します。
/// </summary>
/// <typeparam name="T">チャンクの要素の型。この型パラメータは共変です。</typeparam>
public interface IChunking<out T> : IEnumerable<T>, IEnumerable
{
    /// <summary>
    /// 全体に対するチャンクの開始位置を表す0から始まるインデックスを取得します。
    /// </summary>
    Index Start { get; }

    /// <summary>
    /// チャンクの長さを取得します。
    /// </summary>
    int Length { get; }

    /// <summary>
    /// シーケンス内の要素の数を返します。
    /// </summary>
    /// <returns>シーケンス内の要素数。</returns>
    int Count() => Length;
}
