namespace CarerExtension.Extensions.RegexGenerators;

internal static partial class RegexDefines
{
    [GeneratedRegex("^", RegexOptions.Multiline)]
    internal static partial Regex BeginningOfLine();

    [GeneratedRegex("([^\\s])")]
    internal static partial Regex AnyDocument();

    [GeneratedRegex("\\s+")]
    internal static partial Regex ConsecutiveSpace();

    [GeneratedRegex("^(\\s+)([^\\s])", RegexOptions.Multiline)]
    internal static partial Regex IndentedDocument();
}
