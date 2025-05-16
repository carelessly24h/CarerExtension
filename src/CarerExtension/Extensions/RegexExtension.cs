namespace CarerExtension.Extensions;

/// <summary>
/// Extension methods for <see cref="Regex"/>.
/// </summary>
public static class RegexExtension
{
    /// <summary>
    /// 正規表現に複数行モードが設定されているかどうかをチェックします。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">チェックする<see cref="Regex"/></param>
    /// <returns>
    /// 複数行モードが設定されている場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMultiLine(this Regex source) =>
        (source.Options & RegexOptions.Multiline) > 0;
}
