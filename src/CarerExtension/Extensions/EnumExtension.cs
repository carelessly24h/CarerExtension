namespace CarerExtension.Extensions;

/// <summary>
/// Enum extension methods.
/// </summary>
public static class EnumExtension
{
    /// <summary>
    /// DescriptionAttributeの値を取得します。
    /// </summary>
    /// <param name="source">DescriptionAttributeを設定した列挙体。</param>
    /// <returns>列挙体に設定したDescriptionAttribute。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? GetDescription(this Enum source) =>
        source.GetAttribute<DescriptionAttribute>()?.Description;

    /// <summary>
    /// 列挙体に設定した属性を取得します。
    /// </summary>
    /// <typeparam name="T">列挙体に設定した属性の型。</typeparam>
    /// <param name="source">属性を設定した列挙体。</param>
    /// <returns>列挙体に設定した属性。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static T? GetAttribute<T>(this Enum source) where T : Attribute =>
        source.GetAttributes<T>().SingleOrDefault();

    /// <summary>
    /// 列挙体に設定した属性を取得します。
    /// </summary>
    /// <typeparam name="T">列挙体に設定した属性の型。</typeparam>
    /// <param name="source">属性を設定した列挙体。</param>
    /// <returns>列挙体に設定した属性のコレクション。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static IEnumerable<T> GetAttributes<T>(this Enum source) where T : Attribute
    {
        var member = source.GetType().GetMember(source.ToString());
        return member?.Single().GetCustomAttributes<T>(false) ?? [];
    }
}
