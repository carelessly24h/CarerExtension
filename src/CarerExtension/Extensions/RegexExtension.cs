namespace CarerExtension.Extensions;

public static class RegexExtension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool MultiLine(this Regex source) =>
        (source.Options & RegexOptions.Multiline) > 0;
}
