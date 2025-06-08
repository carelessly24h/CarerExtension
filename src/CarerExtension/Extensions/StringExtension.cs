using CarerExtension.Extensions.RegexGenerators;

namespace CarerExtension.Extensions;

/// <summary>
/// <see cref="string"/>の拡張メソッドを提供します。
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// 指定された位置の文字を取得します。
    /// </summary>
    /// <param name="source">文字を取得する文字列。</param>
    /// <param name="position">取得する位置。</param>
    /// <returns>指定された位置の文字。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string At(this string source, in Index position) =>
        source[position].ToString();

    /// <summary>
    /// 文字列が指定された接尾辞のいずれかで終わるかどうかを判定します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">接尾辞を判定する文字列。</param>
    /// <param name="suffixes">接頭辞。</param>
    /// <returns>
    /// 指定された接頭辞でおわる場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EndsWith(this string source, params string[] suffixes) =>
        source.EndsWith(suffixes.AsEnumerable());

    /// <summary>
    /// 文字列が指定された接尾辞のいずれかで終わるかどうかを判定します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">接尾辞を判定する文字列。</param>
    /// <param name="suffixes">接頭辞。</param>
    /// <returns>
    /// 指定された接頭辞でおわる場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EndsWith(this string source, IEnumerable<string> suffixes) =>
        suffixes.Any(source.EndsWith);

    /// <summary>
    /// 文字列が指定された接尾辞のいずれかで終わるかどうかを判定します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">接尾辞を判定する文字列。</param>
    /// <param name="suffixes">接頭辞。</param>
    /// <returns>
    /// 指定された接頭辞でおわる場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EndsWith(this string source, in Span<string> suffixes) =>
        suffixes.Any(source.EndsWith);

    /// <summary>
    /// 文字列冒頭から<paramref name="limit"/>文字分の部分文字列を返します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">先頭を切り取る文字列。</param>
    /// <param name="limit">切り取る文字数。</param>
    /// <returns>切り取った文字列。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string First(this string source, int limit) => source[..limit];

    /// <summary>
    /// 指定された位置から文字列を取得します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">切り取り実行する文字列。</param>
    /// <param name="position">切り取りを開始する文字位置。</param>
    /// <returns>切り取った文字列。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string From(this string source, in Index position) => source[position..];

    /// <summary>
    /// 指定した文字列に含まれる文字をエンコードすることによって生成されるバイト数を計算します。
    /// </summary>
    /// <param name="source">エンコード対象の文字のセットを格納している文字列。</param>
    /// <param name="encoding">エンコード。</param>
    /// <returns>指定した文字をエンコードすることによって生成されるバイト数。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetByteCount(this string? source, Encoding encoding)
    {
        if (source is string s)
        {
            return encoding.GetByteCount(s);
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// レシーバの行にインデントを追加します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">インデントを追加する文字列。</param>
    /// <param name="indentCount">インデント数。</param>
    /// <param name="indentChar">インデントに使う文字列を指定します。</param>
    /// <param name="indentEmptyLines">空行もインデントするかどうかを指定するフラグです。デフォルトは<see langword="false"/>です。</param>
    /// <returns>インデントを追加した文字列。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Indent(this string source, int indentCount, char indentChar = ' ', bool indentEmptyLines = false)
    {
        var indentString = new string(indentChar, indentCount);
        if (indentEmptyLines)
        {
            return IndentAllLines(source, indentString);
        }
        else
        {
            return IndentOnlyNonEmptyLines(source, indentString);
        }
    }

    /// <summary>
    /// 全ての行にインデントを追加します。
    /// </summary>
    /// <param name="source">インデントを追加する文字列。</param>
    /// <param name="indentString">インデントとして追加する文字列。</param>
    /// <returns>インデントを追加した文字列。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string IndentAllLines(string source, string indentString)
    {
        var regex = RegexDefines.BeginningOfLine();
        return regex.Replace(source, indentString);
    }

    /// <summary>
    /// 空行以外の行にインデントを追加します。
    /// </summary>
    /// <param name="source">インデントを追加する文字列。</param>
    /// <param name="indentString">インデントとして追加する文字列。</param>
    /// <returns>インデントを追加した文字列。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string IndentOnlyNonEmptyLines(string source, string indentString)
    {
        var regex = new Regex($"^([^\\S{Environment.NewLine}]*[^\\s])", RegexOptions.Multiline);
        return regex.Replace(source, $"{indentString}$1");
    }

    /// <summary>
    /// 正規表現にマッチするかどうかを判定します。
    /// </summary>
    /// <param name="input">一致を検索する文字列。</param>
    /// <param name="pattern">照合する正規表現パターン。</param>
    /// <returns>
    /// 正規表現で一致するものが見つかる場合に<see langword="true"/>。
    /// それ以外の場合は、<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMatch(this string input, string pattern) =>
        Regex.IsMatch(input, pattern);

    /// <summary>
    /// 文字列が<see langword="null"/>または空であるかどうかを判定します。
    /// </summary>
    /// <param name="value">判定する文字列。</param>
    /// <returns>
    /// <paramref name="value"/>パラメーターが<see langword="null"/>。または空の文字列 ("") の場合は<see langword="true"/>。
    /// それ以外の場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? value) =>
        string.IsNullOrEmpty(value);

    /// <summary>
    /// 文字列が<see langword="null"/>または空または空白であるかどうかを判定します。
    /// </summary>
    /// <param name="value">判定する文字列。</param>
    /// <returns>
    /// <paramref name="value"/>パラメーターが<see langword="null"/>。または空の文字列 ("")か空白文字だけで構成されている場合は<see langword="true"/>。
    /// それ以外の場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? value) =>
        string.IsNullOrWhiteSpace(value);

    /// <summary>
    /// 文字列が<see langword="null"/>または空でないかどうかを判定します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="value">判定する文字列。</param>
    /// <returns>
    /// <paramref name="value"/>パラメーターが<see langword="null"/>。または空の文字列 ("") の場合は<see langword="true"/>。
    /// それ以外の場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPresent([NotNullWhen(true)] this string? value) => !value.IsNullOrEmpty();

    /// <summary>
    /// 文字列末尾から<paramref name="limit"/>文字分の部分文字列を返します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">末尾を切り取る文字列。</param>
    /// <param name="limit">切り取る文字数。</param>
    /// <returns>切り取った文字列。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Last(this string source, int limit)
    {
        var startIndex = source.Length - limit;
        return source[startIndex..];
    }

    /// <summary>
    /// 文字列が<see langword="null"/>でも空でもない場合はそのまま返します。
    /// そうでない場合は<see langword="null"/>を返します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="value">判定する文字列。</param>
    /// <returns>
    /// 文字列が<see langword="null"/>でも空でもない場合は<paramref name="value"/>。
    ///それ以外の場合は<see langword="null"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? Presence(this string? value)
    {
        if (value.IsPresent())
        {
            return value;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// 正規表現パターンにマッチする部分を削除します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="value">削除する文字列。</param>
    /// <param name="pattern">削除する正規表現のパターン。</param>
    /// <returns>指定されたパターンを削除した文字列。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Remove(this string value, string pattern)
    {
        var regex = new Regex(pattern);
        return regex.Replace(value, string.Empty);
    }

    /// <summary>
    /// インスタンスから部分文字列を取得します。
    /// 部分文字列は、文字列中の指定した文字の位置で開始し、文字列の末尾まで続きます。
    /// </summary>
    /// <param name="source">部分文字列を取得する文字列。</param>
    /// <param name="startIndex">このインスタンス内の部分文字列の 0 から始まる開始文字位置。</param>
    /// <returns>
    /// このインスタンスの<paramref name="startIndex"/>で始まる部分文字列と等価な文字列。
    /// または、<paramref name="startIndex"/>がこのインスタンスの長さと等しい場合は<see langword="Empty"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? SafetySubstring(this string source, int startIndex)
    {
        try
        {
            return source[startIndex..];
        }
        catch (ArgumentOutOfRangeException)
        {
            return default;
        }
    }

    /// <summary>
    /// インスタンスから部分文字列を取得します。
    /// この部分文字列は、指定した文字位置から開始し、指定した文字数の文字列です。
    /// </summary>
    /// <param name="source">部分文字列を取得する文字列。</param>
    /// <param name="startIndex">このインスタンス内の部分文字列の 0 から始まる開始文字位置。</param>
    /// <param name="length">部分文字列の文字数。</param>
    /// <returns>
    /// このインスタンスの<paramref name="startIndex"/>から始まる長さ<paramref name="length"/>の部分文字列と等価な文字列。
    /// または、<paramref name="startIndex"/>がこのインスタンスの長さと等しく、<paramref name="length"/>がゼロの場合は<see langword="Empty"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string? SafetySubstring(this string source, int startIndex, int length)
    {
        try
        {
            var endIndex = startIndex + length;
            return source[startIndex..endIndex];
        }
        catch (ArgumentOutOfRangeException)
        {
            // It tries to slice as much as possible.
            return source.SafetySubstring(startIndex);
        }
    }

    /// <summary>
    /// 文字列の前後の空白を削除し、連続する空白を1つの空白に置き換えます。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">空白を削除する文字列。</param>
    /// <returns>前後の空白を削除した文字列。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Squish(this string source)
    {
        var s = source.Trim();
        return RegexDefines.ConsecutiveSpace().Replace(s, " ");
    }

    /// <summary>
    /// 文字列が指定された接頭辞のいずれかで始まるかどうかを判定します。
    /// </summary>
    /// <param name="source">接頭辞を判定する文字列。</param>
    /// <param name="prefixes">接頭辞。</param>
    /// <returns>
    /// 指定された接頭辞で始まる場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool StartsWith(this string source, params string[] prefixes) =>
        source.StartsWith(prefixes.AsEnumerable());

    /// <summary>
    /// 文字列が指定された接頭辞のいずれかで始まるかどうかを判定します。
    /// </summary>
    /// <param name="source">接頭辞を判定する文字列。</param>
    /// <param name="prefixes">接頭辞。</param>
    /// <returns>
    /// 指定された接頭辞で始まる場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool StartsWith(this string source, IEnumerable<string> prefixes) =>
        prefixes.Any(source.StartsWith);

    /// <summary>
    /// 文字列が指定された接頭辞のいずれかで始まるかどうかを判定します。
    /// </summary>
    /// <param name="source">接頭辞を判定する文字列。</param>
    /// <param name="prefixes">接頭辞。</param>
    /// <returns>
    /// 指定された接頭辞で始まる場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool StartsWith(this string source, in Span<string> prefixes) =>
        prefixes.Any(source.StartsWith);

    /// <summary>
    /// ヒアドキュメントのインデントを削除します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="heredoc">ヒアドキュメント。</param>
    /// <returns>インデントを削除したヒアドキュメント。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string StripHeredoc(this string heredoc)
    {
        var minIndentCount = CountMinimumIndentCount(heredoc);
        if (minIndentCount > 0)
        {
            return TrimIndent(heredoc, minIndentCount);
        }
        else
        {
            return heredoc;
        }
    }

    /// <summary>
    /// ヒアドキュメントの最小インデント数を取得します。
    /// </summary>
    /// <param name="heredoc">インデント数を取得するヒアドキュメント。</param>
    /// <returns>最小インデント数。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int CountMinimumIndentCount(string heredoc)
    {
        var regex = RegexDefines.IndentedDocument();
        return regex.Matches(heredoc).Where(r => r.Success).Min(m => m.Groups[1].Length);
    }

    /// <summary>
    /// ヒアドキュメントのインデントを削除します。
    /// </summary>
    /// <param name="heredoc">インデントを削除するヒアドキュメント。</param>
    /// <param name="trimCount">削除するインデント数。</param>
    /// <returns>インデントを削除したヒアドキュメント。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string TrimIndent(string heredoc, int trimCount)
    {
        var regex = new Regex($"^[^\\S{Environment.NewLine}]{{1,{trimCount}}}", RegexOptions.Multiline);
        return regex.Replace(heredoc, string.Empty);
    }

    /// <summary>
    /// 指定した位置までの部分文字列を取得します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">部分文字列を切り出す文字列。</param>
    /// <param name="position">切り出す位置。</param>
    /// <returns>指定した位置までの部分文字列。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string To(this string source, in Index position)
    {
        if (position.IsFromEnd)
        {
            var i = position.Value - 1;
            return source[..^i];
        }
        else
        {
            var i = position.Value + 1;
            return source[..i];
        }
    }

    /// <summary>
    /// 指定した文字列に含まれるすべての文字をバイト シーケンスにエンコードします。
    /// </summary>
    /// <param name="source">エンコードする文字を含む文字列。</param>
    /// <param name="encoding">エンコードする文字エンコーディング。</param>
    /// <returns>文字列をエンコードしたバイトの配列。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte[] ToBytes(this string source, Encoding encoding) =>
        encoding.GetBytes(source);

    /// <summary>
    /// 文字列を指定した形式で日付に変換します。
    /// </summary>
    /// <param name="source">日付に変換する文字列。</param>
    /// <param name="format"><paramref name="source"/>の必要な形式を定義する形式指定子。</param>
    /// <returns>変換後の日付。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime ToDateTime(this string source, string format) =>
        source.ToDateTime(format, CultureInfo.CurrentCulture);

    /// <summary>
    /// 文字列を指定した形式で日付に変換します。
    /// </summary>
    /// <param name="source">日付に変換する文字列。</param>
    /// <param name="format"><paramref name="source"/>の必要な形式を定義する形式指定子。</param>
    /// <param name="provider"><paramref name="source"/>に関するカルチャ固有の書式情報を提供するオブジェクト。</param>
    /// <returns>変換後の日付。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime ToDateTime(this string source, string format, IFormatProvider provider) =>
        DateTime.ParseExact(source, format, provider);

    /// <summary>
    /// 文字列を指定した形式で日付に変換します。
    /// 変換に失敗した場合は<see langword="null"/>を返します。
    /// </summary>
    /// <param name="source">日付に変換する文字列。</param>
    /// <param name="format"><paramref name="source"/>の必要な形式を定義する形式指定子。</param>
    /// <returns>変換後の日付。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime? ToDateTimeOrDefault(this string source, string format) =>
        source.ToDateTimeOrDefault(format, CultureInfo.CurrentCulture);

    /// <summary>
    /// 文字列を指定した形式で日付に変換します。
    /// 変換に失敗した場合は<see langword="null"/>を返します。
    /// </summary>
    /// <param name="source">日付に変換する文字列。</param>
    /// <param name="format"><paramref name="source"/>の必要な形式を定義する形式指定子。</param>
    /// <param name="provider"><paramref name="source"/>に関するカルチャ固有の書式情報を提供するオブジェクト。</param>
    /// <returns>変換後の日付。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime? ToDateTimeOrDefault(this string source, string format, IFormatProvider provider)
    {
        try
        {
            return source.ToDateTime(format, provider);
        }
        catch (Exception e) when (e is ArgumentNullException || e is FormatException)
        {
            return default;
        }
    }

    /// <summary>
    /// 指定した数値の文字列形式を等価の 10 進数に変換します。
    /// </summary>
    /// <param name="value">変換する数値を含む文字列。</param>
    /// <returns>
    /// <paramref name="value"/>の数値と等しい 10 進数。
    /// <paramref name="value"/>が<see langword="null"/>の場合は 0 (ゼロ)。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal ToDecimal(this string value) =>
        Convert.ToDecimal(value);

    /// <summary>
    /// 指定したカルチャ固有の書式情報を使用して、指定した数値の文字列形式を等価の 10 進数に変換します。
    /// </summary>
    /// <param name="value">変換する数値を含む文字列。</param>
    /// <param name="provider">カルチャ固有の書式設定情報を提供するオブジェクト。</param>
    /// <returns>
    /// <paramref name="value"/>の数値と等しい 10 進数。
    /// <paramref name="value"/>が<see langword="null"/>の場合は 0 (ゼロ)。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal ToDecimal(this string value, IFormatProvider provider) =>
        Convert.ToDecimal(value, provider);

    /// <summary>
    /// 指定した数値の文字列形式を等価の 10 進数に変換します。
    /// 変換に失敗した場合は<see langword="null"/>を返します。
    /// </summary>
    /// <param name="value">変換する数値を含む文字列。</param>
    /// <returns>
    /// <paramref name="value"/>の数値と等しい 10 進数。
    /// <paramref name="value"/>が<see langword="null"/>の場合は 0 (ゼロ)。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal? ToDecimalOrDefault(this string value) =>
        ParseErrorHandler(value.ToDecimal);

    /// <summary>
    /// 指定したカルチャ固有の書式情報を使用して、指定した数値の文字列形式を等価の 10 進数に変換します。
    /// 変換に失敗した場合は<see langword="null"/>を返します。
    /// </summary>
    /// <param name="value">変換する数値を含む文字列。</param>
    /// <param name="provider">カルチャ固有の書式設定情報を提供するオブジェクト。</param>
    /// <returns>
    /// <paramref name="value"/>の数値と等しい 10 進数。
    /// <paramref name="value"/>が<see langword="null"/>の場合は 0 (ゼロ)。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal? ToDecimalOrDefault(this string value, IFormatProvider provider) =>
        ParseErrorHandler(() => value.ToDecimal(provider));

    /// <summary>
    /// 指定した単精度浮動小数点数値を等価の倍精度浮動小数点数に変換します。
    /// </summary>
    /// <param name="value">変換する数値を含んだ文字列。</param>
    /// <returns>
    /// <paramref name="value"/>の数値と等価の倍精度浮動小数点数。
    /// <paramref name="value"/>が<see langword="null"/>の場合は 0 (ゼロ)。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ToDouble(this string value) =>
        Convert.ToDouble(value);

    /// <summary>
    /// 指定したカルチャに固有の書式情報を使用して、指定した数値の文字列形式を等価の倍精度浮動小数点数に変換します。
    /// </summary>
    /// <param name="value">変換する数値を含んだ文字列。</param>
    /// <param name="provider">カルチャ固有の書式情報を提供するオブジェクト。</param>
    /// <returns>
    /// <paramref name="value"/>の数値と等価の倍精度浮動小数点数。
    /// <paramref name="value"/>が<see langword="null"/>の場合は 0 (ゼロ)。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ToDouble(this string value, IFormatProvider provider) =>
        Convert.ToDouble(value, provider);

    /// <summary>
    /// 指定した単精度浮動小数点数値を等価の倍精度浮動小数点数に変換します。
    /// 変換に失敗した場合は<see langword="null"/>を返します。
    /// </summary>
    /// <param name="value">変換する数値を含んだ文字列。</param>
    /// <returns>
    /// <paramref name="value"/>の数値と等価の倍精度浮動小数点数。
    /// <paramref name="value"/>が<see langword="null"/>の場合は 0 (ゼロ)。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double? ToDoubleOrDefault(this string value) =>
        ParseErrorHandler(value.ToDouble);

    /// <summary>
    /// 指定したカルチャに固有の書式情報を使用して、指定した数値の文字列形式を等価の倍精度浮動小数点数に変換します。
    /// 変換に失敗した場合は<see langword="null"/>を返します。
    /// </summary>
    /// <param name="value">変換する数値を含んだ文字列。</param>
    /// <param name="provider">カルチャ固有の書式情報を提供するオブジェクト。</param>
    /// <returns>
    /// <paramref name="value"/>の数値と等価の倍精度浮動小数点数。
    /// <paramref name="value"/>が<see langword="null"/>の場合は 0 (ゼロ)。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double? ToDoubleOrDefault(this string value, IFormatProvider provider) =>
        ParseErrorHandler(() => value.ToDouble(provider));

    /// <summary>
    /// 指定した数値の文字列形式を等価の 32 ビット符号付き整数に変換します。
    /// </summary>
    /// <param name="value">変換する数値を含んだ文字列。</param>
    /// <returns>
    /// <paramref name="value"/>の数値と等価の 32 ビット符号付き整数。
    /// <paramref name="value"/>が<see langword="null"/>の場合は 0 (ゼロ)。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToInt32(this string value) =>
        Convert.ToInt32(value);

    /// <summary>
    /// 指定したカルチャに固有の書式情報を使用して、指定した数値の文字列形式を等価の 32 ビット符号付き整数に変換します。
    /// </summary>
    /// <param name="value">変換する数値を含んだ文字列。</param>
    /// <param name="provider">カルチャ固有の書式情報を提供するオブジェクト。</param>
    /// <returns>
    /// <paramref name="value"/>の数値と等価の 32 ビット符号付き整数。
    /// <paramref name="value"/>が<see langword="null"/>の場合は 0 (ゼロ)。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToInt32(this string value, IFormatProvider provider) =>
        Convert.ToInt32(value, provider);

    /// <summary>
    /// 指定した数値の文字列形式を等価の 32 ビット符号付き整数に変換します。
    /// 変換に失敗した場合は<see langword="null"/>を返します。
    /// </summary>
    /// <param name="value">変換する数値を含んだ文字列。</param>
    /// <returns>
    /// <paramref name="value"/>の数値と等価の 32 ビット符号付き整数。
    /// <paramref name="value"/>が<see langword="null"/>の場合は 0 (ゼロ)。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToInt32OrDefalt(this string value) =>
        ParseErrorHandler(value.ToInt32);

    /// <summary>
    /// 指定したカルチャに固有の書式情報を使用して、指定した数値の文字列形式を等価の 32 ビット符号付き整数に変換します。
    /// 変換に失敗した場合は<see langword="null"/>を返します。
    /// </summary>
    /// <param name="value">変換する数値を含んだ文字列。</param>
    /// <param name="provider">カルチャ固有の書式情報を提供するオブジェクト。</param>
    /// <returns>
    /// <paramref name="value"/>の数値と等価の 32 ビット符号付き整数。
    /// <paramref name="value"/>が<see langword="null"/>の場合は 0 (ゼロ)。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToInt32OrDefalt(this string value, IFormatProvider provider) =>
        ParseErrorHandler(() => value.ToInt32(provider));

    /// <summary>
    /// 指定した数値の文字列形式を等価の 64 ビット符号付き整数に変換します。
    /// </summary>
    /// <param name="value">変換する数値を含んだ文字列。</param>
    /// <returns>
    /// <paramref name="value"/>の数値と等価の 64 ビット符号付き整数。
    /// <paramref name="value"/>が<see langword="null"/>の場合は 0 (ゼロ)。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long ToInt64(this string value) =>
        Convert.ToInt64(value);

    /// <summary>
    /// 指定したカルチャに固有の書式情報を使用して、指定した数値の文字列形式を等価の 64 ビット符号付き整数に変換します。
    /// </summary>
    /// <param name="value">変換する数値を含んだ文字列。</param>
    /// <param name="provider">カルチャ固有の書式情報を提供するオブジェクト。</param>
    /// <returns>
    /// <paramref name="value"/>の数値と等価の 64 ビット符号付き整数。
    /// <paramref name="value"/>が<see langword="null"/>の場合は 0 (ゼロ)。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long ToInt64(this string value, IFormatProvider provider) =>
        Convert.ToInt64(value, provider);

    /// <summary>
    /// 指定した数値の文字列形式を等価の 64 ビット符号付き整数に変換します。
    /// 変換に失敗した場合は<see langword="null"/>を返します。
    /// </summary>
    /// <param name="value">変換する数値を含んだ文字列。</param>
    /// <returns>
    /// <paramref name="value"/>の数値と等価の 64 ビット符号付き整数。
    /// <paramref name="value"/>が<see langword="null"/>の場合は 0 (ゼロ)。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long ToInt64OrDefault(this string value) =>
        ParseErrorHandler(value.ToInt64);

    /// <summary>
    /// 指定したカルチャに固有の書式情報を使用して、指定した数値の文字列形式を等価の 64 ビット符号付き整数に変換します。
    /// 変換に失敗した場合は<see langword="null"/>を返します。
    /// </summary>
    /// <param name="value">変換する数値を含んだ文字列。</param>
    /// <param name="provider">カルチャ固有の書式情報を提供するオブジェクト。</param>
    /// <returns>
    /// <paramref name="value"/>の数値と等価の 64 ビット符号付き整数。
    /// <paramref name="value"/>が<see langword="null"/>の場合は 0 (ゼロ)。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long ToInt64OrDefault(this string value, IFormatProvider provider) =>
        ParseErrorHandler(() => value.ToInt64(provider));

    /// <summary>
    /// 例外のハンドリングを行います。
    /// </summary>
    /// <typeparam name="T">変換処理の結果の型。</typeparam>
    /// <param name="convertFunc">変換処理。</param>
    /// <returns>変換処理の結果。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static T? ParseErrorHandler<T>(Func<T> convertFunc)
    {
        try
        {
            return convertFunc();
        }
        catch (Exception e) when (e is FormatException || e is InvalidCastException || e is OverflowException)
        {
            return default;
        }
    }

    /// <summary>
    /// 現在の文字列から指定の数だけ、先頭と末尾の空白文字を削除します。
    /// </summary>
    /// <param name="source">空白を削除する文字列。</param>
    /// <param name="count">空白を削除する上限数。</param>
    /// <returns>
    /// 現在の文字列の先頭と末尾から、空白を指定の数削除した後に残る文字列。
    /// 現在のインスタンスから文字をトリミングできない場合、メソッドは現在のインスタンスを変更せずに返します。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Trim(this string source, int count) =>
        source.TrimStart(count).TrimEnd(count);

    /// <summary>
    /// 現在の文字列から指定の数だけ、末尾の空白文字を削除します。
    /// </summary>
    /// <param name="source">空白を削除する文字列。</param>
    /// <param name="count">空白を削除する上限数。</param>
    /// <returns>
    /// 現在の文字列の末尾から、空白を指定の数削除した後に残る文字列。
    /// 現在のインスタンスから文字をトリミングできない場合、メソッドは現在のインスタンスを変更せずに返します。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string TrimEnd(this string source, int count)
    {
        var regex = new Regex($"\\s{{1,{count}}}$");
        return regex.Replace(source, string.Empty);
    }

    /// <summary>
    /// 現在の文字列から指定の数だけ、先頭の空白文字を削除します。
    /// </summary>
    /// <param name="source">空白を削除する文字列。</param>
    /// <param name="count">空白を削除する上限数。</param>
    /// <returns>
    /// 現在の文字列の先頭から、空白を指定の数削除した後に残る文字列。
    /// 現在のインスタンスから文字をトリミングできない場合、メソッドは現在のインスタンスを変更せずに返します。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string TrimStart(this string source, int count)
    {
        var regex = new Regex($"^\\s{{1,{count}}}");
        return regex.Replace(source, string.Empty);
    }
}
