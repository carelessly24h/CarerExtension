using CarerExtension.Extensions.RegexGenerators;

namespace CarerExtension.Extensions;

public static class StringExtension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string At(this string source, Index position) => source[position].ToString();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EndsWith(this string source, params string[] suffixes) =>
        source.EndsWith(suffixes.AsEnumerable());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EndsWith(this string source, IEnumerable<string> suffixes) =>
        suffixes.Any(source.EndsWith);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool EndsWith(this string source, Span<string> suffixes) =>
        suffixes.Any(source.EndsWith);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string First(this string source, int limit) => source[..limit];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string From(this string source, Index position) => source[position..];

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Indent(this string source, int indentCount, char indentChar = ' ', bool indentEmptyLines = false)
    {
        var indentString = new string(indentChar, indentCount);
        if (indentEmptyLines)
        {
            return IndentAllLInes(source, indentString);
        }
        else
        {
            return IndentOnlyNonEmptyLines(source, indentString);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string IndentAllLInes(string source, string indentString)
    {
        var regex = RegexDefines.BeginningOfLine();
        return regex.Replace(source, indentString);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string IndentOnlyNonEmptyLines(string source, string indentString)
    {
        var regex = new Regex($"^([^\\S{Environment.NewLine}]*[^\\s])", RegexOptions.Multiline);
        return regex.Replace(source, $"{indentString}$1");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMatch(this string input, string pattern) =>
        Regex.IsMatch(input, pattern);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? value) =>
        string.IsNullOrEmpty(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? value) =>
        string.IsNullOrWhiteSpace(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPresent([NotNullWhen(true)] this string? value) => !value.IsNullOrEmpty();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsUnmatch(this string input, string pattern) => !input.IsMatch(pattern);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Last(this string source, int limit)
    {
        var startIndex = source.Length - limit;
        return source[startIndex..];
    }

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Remove(this string value, string pattern)
    {
        var regex = new Regex(pattern);
        return regex.Replace(value, string.Empty);
    }

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Squish(this string source)
    {
        var s = source.Trim();
        return RegexDefines.ConsecutiveSpace().Replace(s, " ");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool StartsWith(this string source, params string[] prefixes) =>
        source.StartsWith(prefixes.AsEnumerable());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool StartsWith(this string source, IEnumerable<string> prefixes) =>
        prefixes.Any(source.StartsWith);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool StartsWith(this string source, Span<string> prefixes) =>
        prefixes.Any(source.StartsWith);

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int CountMinimumIndentCount(string heredoc)
    {
        var regex = RegexDefines.IndentedDocument();
        return regex.Matches(heredoc).Where(r => r.Success).Min(m => m.Groups[1].Length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string TrimIndent(string heredoc, int trimCount)
    {
        var regex = new Regex($"^[^\\S{Environment.NewLine}]{{1,{trimCount}}}", RegexOptions.Multiline);
        return regex.Replace(heredoc, string.Empty);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string To(this string source, Index position)
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte[] ToBytes(this string source, Encoding encoding) =>
        encoding.GetBytes(source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime ToDateTime(this string source, string format) =>
        source.ToDateTime(format, CultureInfo.CurrentCulture);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime ToDateTime(this string source, string format, IFormatProvider provider) =>
        DateTime.ParseExact(source, format, provider);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime? ToDateTimeOrDefault(this string source, string format) =>
        source.ToDateTimeOrDefault(format, CultureInfo.CurrentCulture);

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal ToDecimal(this string source) =>
        Convert.ToDecimal(source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal ToDecimal(this string source, IFormatProvider provider) =>
        Convert.ToDecimal(source, provider);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal? ToDecimalOrDefault(this string source) =>
        ParseErrorHandler(source.ToDecimal);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static decimal? ToDecimalOrDefault(this string source, IFormatProvider provider) =>
        ParseErrorHandler(() => source.ToDecimal(provider));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ToDouble(this string source) =>
        Convert.ToDouble(source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ToDouble(this string source, IFormatProvider provider) =>
        Convert.ToDouble(source, provider);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double? ToDoubleOrDefault(this string source) =>
        ParseErrorHandler(source.ToDouble);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double? ToDoubleOrDefault(this string source, IFormatProvider provider) =>
        ParseErrorHandler(() => source.ToDouble(provider));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToInt32(this string source) =>
        Convert.ToInt32(source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToInt32(this string source, IFormatProvider provider) =>
        Convert.ToInt32(source, provider);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToInt32OrDefalt(this string source) =>
        ParseErrorHandler(source.ToInt32);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToInt32OrDefalt(this string source, IFormatProvider provider) =>
        ParseErrorHandler(() => source.ToInt32(provider));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long ToInt64(this string source) =>
        Convert.ToInt64(source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long ToInt64(this string source, IFormatProvider provider) =>
        Convert.ToInt64(source, provider);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long ToInt64OrDefault(this string source) =>
        ParseErrorHandler(source.ToInt64);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long ToInt64OrDefault(this string source, IFormatProvider provider) =>
        ParseErrorHandler(() => source.ToInt64(provider));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static T? ParseErrorHandler<T>(Func<T> converterFunc)
    {
        try
        {
            return converterFunc();
        }
        catch (Exception e) when (e is FormatException || e is InvalidCastException || e is OverflowException)
        {
            return default;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Trim(this string source, int count) =>
        source.TrimStart(count).TrimEnd(count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string TrimEnd(this string source, int count)
    {
        var regex = new Regex($"\\s{{1,{count}}}$");
        return regex.Replace(source, string.Empty);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string TrimStart(this string source, int count)
    {
        var regex = new Regex($"^\\s{{1,{count}}}");
        return regex.Replace(source, string.Empty);
    }
}
