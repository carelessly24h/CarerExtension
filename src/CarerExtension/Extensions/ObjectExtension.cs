namespace CarerExtension.Extensions;

/// <summary>
/// object extension methods for retrieving properties with specific attributes.
/// </summary>
public static class ObjectExtension
{
    /// <summary>
    /// 指定された属性を設定したプロパティを取得します。
    /// </summary>
    /// <typeparam name="T">検索する属性の型。</typeparam>
    /// <param name="instance">プロパティを検査するオブジェクト。</param>
    /// <returns>
    /// 指定された属性を設定したプロパティを表す<see cref="PropertyInfo"/>のコレクション。
    /// プロパティが見つからない場合、コレクションは空になります。
    /// </returns>
    public static IEnumerable<PropertyInfo?> GetProperties<T>(this object instance) where T : Attribute =>
        instance.
        GetType().
        GetProperties().
        Where(p => p.GetCustomAttribute<T>() != null);

    /// <summary>
    /// 指定された属性を設定したプロパティを取得します。
    /// </summary>
    /// <remarks>
    /// 複数のプロパティに同じ属性を設定していた場合は、例外がスローされます。
    /// </remarks>
    /// <typeparam name="T">検索する属性の型。</typeparam>
    /// <param name="instance">プロパティを検査するオブジェクト。</param>
    /// <returns>
    /// 指定された属性を設定した最初のプロパティを表す<see cref="PropertyInfo"/>。
    /// プロパティが見つからない場合は<see langword="null"/>。
    /// </returns>
    public static PropertyInfo? GetProperty<T>(this object instance) where T : Attribute =>
        instance.GetProperties<T>().SingleOrDefault();

    /// <summary>
    /// 指定された属性を設定したプロパティと設定した属性のコレクションを取得します。
    /// </summary>
    /// <typeparam name="T">検索する属性の型。</typeparam>
    /// <param name="instance">プロパティを検査するオブジェクト。</param>
    /// 指定された属性を設定したプロパティを表す<see cref="PropertyInfo"/>と属性のコレクション。
    /// プロパティが見つからない場合、コレクションは空になります。
    public static IEnumerable<(PropertyInfo, T)> GetPropertySets<T>(this object instance) where T : Attribute =>
        instance.
        GetType().
        GetProperties().
        Select(p => (Property: p, Attribute: p.GetCustomAttribute<T>())).
        Where(s => s.Attribute != null).
        Cast<(PropertyInfo, T)>();
}
