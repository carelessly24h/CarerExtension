namespace CarerExtension.Extensions;

public static class EnumExtension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? GetDescription(this Enum source) =>
        source.GetAttribute<DescriptionAttribute>()?.Description;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static T? GetAttribute<T>(this Enum source) where T : Attribute =>
        source.GetAttributes<T>().SingleOrDefault();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static IEnumerable<T> GetAttributes<T>(this Enum source) where T : Attribute
    {
        var member = source.GetType().GetMember(source.ToString());
        return member?.Single().GetCustomAttributes<T>(false) ?? [];
    }
}
