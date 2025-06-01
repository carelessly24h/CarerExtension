namespace CarerExtension.Extensions;

/// <summary>
/// IList extension methods for adding multiple items at once.
/// </summary>
public static class IListExtension
{
    /// <summary>
    /// 指定したコレクションの要素をリストの末尾に追加します。
    /// </summary>
    /// <param name="list">要素を追加するリスト。</param>
    /// <param name="items">リストの末尾に要素が追加されるコレクション。</param>
    public static void AddRange(this IList list, IEnumerable<object> items)
    {
        foreach (var item in items)
        {
            list.Add(item);
        }
    }

    /// <summary>
    /// 指定したコレクションの要素をリストの末尾に追加します。
    /// </summary>
    /// <typeparam name="T">追加する要素の型。</typeparam>
    /// <param name="list">要素を追加するリスト。</param>
    /// <param name="items">リストの末尾に要素が追加されるコレクション。</param>
    public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            list.Add(item);
        }
    }
}
