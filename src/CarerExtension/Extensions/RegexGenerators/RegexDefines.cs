namespace CarerExtension.Extensions.RegexGenerators;

/// <summary>
/// 正規表現の定義を行うクラスです。
/// </summary>
internal static partial class RegexDefines
{
    /// <summary>
    /// 行頭を表す正規表現を取得します。
    /// </summary>
    /// <returns>行頭を表す正規表現。</returns>
    [GeneratedRegex("^", RegexOptions.Multiline)]
    internal static partial Regex BeginningOfLine();

    /// <summary>
    /// 連続するスペースを表す正規表現を取得します。
    /// </summary>
    /// <returns>連続するスペース。</returns>
    [GeneratedRegex("\\s+")]
    internal static partial Regex ConsecutiveSpace();

    /// <summary>
    /// インデントされた文書を表す正規表現を取得します。
    /// </summary>
    /// <returns>インデントされた文書。</returns>
    [GeneratedRegex("^(\\s+)([^\\s])", RegexOptions.Multiline)]
    internal static partial Regex IndentedDocument();
}
