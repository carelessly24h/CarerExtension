namespace CarerExtension.Extensions;

/// <summary>
/// ProperyInfo extension methods for creating instances of property types.
/// </summary>
public static class PropertyInfoExtension
{
    /// <summary>
    /// 指定されたプロパティのインスタンスを作成します。
    /// </summary>
    /// <param name="property">インスタンスを作成するプロパティ。</param>
    /// <returns>
    /// 指定された引数を使用して作成されたインスタンス。
    /// 作成に失敗した場合、またはプロパティがインスタンス化をサポートしていない場合は<see langword="null"/>が返されます。
    /// </returns>
    public static object? CreateValue(this PropertyInfo property) =>
        Activator.CreateInstance(property.PropertyType);

    /// <summary>
    /// 指定した引数を使用して、指定されたプロパティのインスタンスを作成します。
    /// </summary>
    /// <param name="property">インスタンスを作成するプロパティ。</param>
    /// <param name="args">コンストラクタに渡す引数の配列。</param>
    /// <returns>
    /// 指定された引数を使用して作成されたインスタンス。
    /// 作成に失敗した場合、またはプロパティがインスタンス化をサポートしていない場合は<see langword="null"/>が返されます。
    /// </returns>
    public static object? CreateValue(this PropertyInfo property, params object?[] args) =>
        Activator.CreateInstance(property.PropertyType, args);

    /// <summary>
    /// 指定されたプロパティのインスタンスを作成します。
    /// </summary>
    /// <typeparam name="T">作成するインスタンスの型。</typeparam>
    /// <param name="property">インスタンスを作成するプロパティ。</param>
    /// <returns>
    /// 指定された引数を使用して作成された<typeparamref name="T"/>型のインスタンス。
    /// 作成に失敗した場合、またはプロパティがインスタンス化をサポートしていない場合は<see langword="null"/>が返されます。
    /// </returns>
    public static T? CreateValue<T>(this PropertyInfo property) =>
        (T?)property.CreateValue();

    /// <summary>
    /// 指定した引数を使用して、指定されたプロパティのインスタンスを作成します。
    /// </summary>
    /// <typeparam name="T">作成するインスタンスの型。</typeparam>
    /// <param name="property">インスタンスを作成するプロパティ。</param>
    /// <param name="args">コンストラクタに渡す引数の配列。</param>
    /// <returns>
    /// 指定された引数を使用して作成された<typeparamref name="T"/>型のインスタンス。
    /// 作成に失敗した場合、またはプロパティがインスタンス化をサポートしていない場合は<see langword="null"/>が返されます。
    /// </returns>
    public static T? CreateValue<T>(this PropertyInfo property, params object?[] args) =>
        (T?)property.CreateValue(args);

    /// <summary>
    /// 指定されたプロパティの値を取得します。
    /// プロパティが<see langword="null"/>の場合は、新しい値を作成します。
    /// </summary>
    /// <param name="property">インスタンスを取得、または作成するプロパティ。</param>
    /// <param name="instance">プロパティを検査するオブジェクト。</param>
    /// <returns>
    /// プロパティが<see langword="null"/>でない場合は現在の値。
    /// そうでない場合は、新しく作成された値が返されます。
    /// </returns>
    public static object? GetOrCreateValue(this PropertyInfo property, object instance) =>
        property.GetValue(instance) ?? property.CreateValue();

    /// <summary>
    /// 指定されたプロパティの値を取得します。
    /// プロパティが<see langword="null"/>の場合は、新しい値を作成します。
    /// </summary>
    /// <param name="property">インスタンスを取得、または作成するプロパティ。</param>
    /// <param name="instance">プロパティを検査するオブジェクト。</param>
    /// <param name="args">コンストラクタに渡す引数の配列。</param>
    /// <returns>
    /// プロパティが<see langword="null"/>でない場合は現在の値。
    /// そうでない場合は、新しく作成された値が返されます。
    /// </returns>
    public static object? GetOrCreateValue(this PropertyInfo property, object instance, params object?[] args) =>
        property.GetValue(instance) ?? property.CreateValue(args);

    /// <summary>
    /// 指定されたプロパティの値を取得します。
    /// プロパティが<see langword="null"/>の場合は、新しい値を作成します。
    /// </summary>
    /// <typeparam name="T">作成するインスタンスの型。</typeparam>
    /// <param name="property">インスタンスを取得、または作成するプロパティ。</param>
    /// <param name="instance">プロパティを検査するオブジェクト。</param>
    /// <returns>
    /// プロパティが<see langword="null"/>でない場合は現在の値。
    /// そうでない場合は、新しく作成された値が返されます。
    /// </returns>
    public static T? GetOrCreateValue<T>(this PropertyInfo property, object instance) =>
        (T?)property.GetOrCreateValue(instance);

    /// <summary>
    /// 指定されたプロパティの値を取得します。
    /// プロパティが<see langword="null"/>の場合は、新しい値を作成します。
    /// </summary>
    /// <typeparam name="T">作成するインスタンスの型。</typeparam>
    /// <param name="property">インスタンスを取得、または作成するプロパティ。</param>
    /// <param name="instance">プロパティを検査するオブジェクト。</param>
    /// <param name="args">コンストラクタに渡す引数の配列。</param>
    /// <returns>
    /// プロパティが<see langword="null"/>でない場合は現在の値。
    /// そうでない場合は、新しく作成された値が返されます。
    /// </returns>
    public static T? GetOrCreateValue<T>(this PropertyInfo property, object instance, params object?[] args) =>
        (T?)property.GetOrCreateValue(instance, args);
}
