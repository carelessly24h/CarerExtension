namespace CarerExtension.Extensions;

/// <summary>
/// DateTime struct extension.
/// </summary>
public static class DateTimeExtension
{
    /// <summary>
    /// 指定の日時、未来の日時を取得します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <param name="years">シフトする年数。</param>
    /// <param name="months">シフトする月数。</param>
    /// <param name="weeks">シフトする週数</param>
    /// <param name="days">シフトする日数</param>
    /// <param name="hours">シフトする時数</param>
    /// <param name="minutes">シフトする分数</param>
    /// <param name="seconds">シフトする秒数</param>
    /// <param name="milliseconds">シフトするミリ秒数</param>
    /// <returns>未来の日時</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime Advance(
        this DateTime source,
        int years = 0,
        int months = 0,
        int weeks = 0,
        int days = 0,
        int hours = 0,
        int minutes = 0,
        int seconds = 0,
        int milliseconds = 0)
    {
        // only when necessary is faster than add always.
        var d = source;
        if (years != 0)
        {
            d = d.AddYears(years);
        }
        if (months != 0)
        {
            d = d.AddMonths(months);
        }
        if (weeks != 0 || days != 0)
        {
            d = d.AddDays(weeks * 7 + days);
        }
        if (hours != 0)
        {
            d = d.AddHours(hours);
        }
        if (minutes != 0)
        {
            d = d.AddMinutes(minutes);
        }
        if (seconds != 0)
        {
            d = d.AddSeconds(seconds);
        }
        if (milliseconds != 0)
        {
            d = d.AddMilliseconds(milliseconds);
        }
        return d;
    }

    /// <summary>
    /// 指定の秒数、過去の日時を取得します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <param name="seconds">秒数。</param>
    /// <returns>過去の日時。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime Ago(this DateTime source, int seconds) =>
        source.AddSeconds(-seconds);

    /// <summary>
    /// その日の開始時点のタイムスタンプを返します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <returns>その日の開始時点のタイムスタンプ。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime BeginningOfDay(this DateTime source) =>
        source.Date;

    /// <summary>
    /// その時刻の開始時点のタイムスタンプを返します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <returns>その時刻の開始時点のタイムスタンプ。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime BeginningOfHour(this DateTime source) =>
        new(source.Year, source.Month, source.Day, source.Hour, 0, 0);

    /// <summary>
    /// その時刻の開始時点のタイムスタンプを返します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <returns>その時刻の開始時点のタイムスタンプ。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime BeginningOfMinute(this DateTime source) =>
        new(source.Year, source.Month, source.Day, source.Hour, source.Minute, 0);

    /// <summary>
    /// その月の開始時点のタイムスタンプを返します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <returns>その月の開始時点のタイムスタンプ。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime BeginningOfMonth(this DateTime source) =>
        new(source.Year, source.Month, 1);

    /// <summary>
    /// 四半期の開始時点のタイムスタンプを返します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <returns>四半期の開始時点のタイムスタンプ。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime BeginningOfQuarter(this DateTime source)
    {
        var newMonth = source.Month switch
        {
            1 or 2 or 3 => 1,
            4 or 5 or 6 => 4,
            7 or 8 or 9 => 7,
            10 or 11 or 12 => 10,
            // not happen.
            _ => 1,
        };
        return new(source.Year, newMonth, 1);
    }

    /// <summary>
    /// その週の開始時点のタイムスタンプを返します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <param name="beginning">週の先頭の曜日。</param>
    /// <returns>その週の開始時点のタイムスタンプ。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime BeginningOfWeek(this DateTime source, DayOfWeek beginning = DayOfWeek.Monday)
    {
        var beginOfWeek = (int)beginning;
        var week = (int)source.DayOfWeek;
        if (week < beginOfWeek)
        {
            week += 7;
        }
        return source.PreviousDay(week - beginOfWeek).Date;
    }

    /// <summary>
    /// その年の開始時点のタイムスタンプを返します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <returns>その年の開始時点のタイムスタンプ。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime BeginningOfYear(this DateTime source) =>
        new(source.Year, 1, 1);

    /// <summary>
    /// レシーバの日時の一部の要素だけを変更した新しい日時を返します。
    /// </summary>
    /// <param name="source">変更前の日時。</param>
    /// <param name="year">新しい年。</param>
    /// <param name="month">新しい月。</param>
    /// <param name="day">新しい日。</param>
    /// <param name="hour">新しい時。</param>
    /// <param name="minute">新しい分。</param>
    /// <param name="second">新しい秒。</param>
    /// <param name="millisecond">新しいミリ秒。</param>
    /// <returns>変更後の日時。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime Change(
        this DateTime source,
        int? year = null,
        int? month = null,
        int? day = null,
        int? hour = null,
        int? minute = null,
        int? second = null,
        int? millisecond = null)
    {
        var newYear = year ?? source.Year;
        var newMonth = month ?? source.Month;
        var newDay = day ?? source.Day;
        var newHour = hour ?? source.Hour;
        var newMinute = minute ?? source.Minute;
        var newSecond = second ?? source.Second;
        var newMillisecond = millisecond ?? source.Millisecond;
        return new(newYear, newMonth, newDay, newHour, newMinute, newSecond, newMillisecond);
    }

    /// <summary>
    /// 指定の年月に与えられた月の日数を返します。
    /// </summary>
    /// <param name="source">基準となる日時。</param>
    /// <returns>月の日数。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int DaysInMonth(this DateTime source) =>
        DateTime.DaysInMonth(source.Year, source.Month);

    /// <summary>
    /// 日時を分解して返します。
    /// </summary>
    /// <param name="source">分解する日時。</param>
    /// <param name="year">分解後の年。</param>
    /// <param name="month">分解後の月。</param>
    /// <param name="day">分解後の日。</param>
    /// <param name="hour">分解後の時。</param>
    /// <param name="minute">分解後の分。</param>
    /// <param name="second">分解後の秒。</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deconstruct(
        this DateTime source,
        out int year,
        out int month,
        out int day,
        out int hour,
        out int minute,
        out int second)
    {
        (year, month, day) = source;
        hour = source.Hour;
        minute = source.Minute;
        second = source.Second;
    }

    /// <summary>
    /// 日時を分解して返します。
    /// </summary>
    /// <param name="source">分解する日時。</param>
    /// <param name="year">分解後の年。</param>
    /// <param name="month">分解後の月。</param>
    /// <param name="day">分解後の日。</param>
    /// <param name="hour">分解後の時。</param>
    /// <param name="minute">分解後の分。</param>
    /// <param name="second">分解後の秒。</param>
    /// <param name="millisecond">分解後のミリ秒。</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Deconstruct(
        this DateTime source,
        out int year,
        out int month,
        out int day,
        out int hour,
        out int minute,
        out int second,
        out int millisecond)
    {
        (year, month, day, hour, minute, second) = source;
        millisecond = source.Millisecond;
    }

    /// <summary>
    /// その日の終了時点のタイムスタンプを返します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <returns>その日の終了時点のタイムスタンプ。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime EndOfDay(this DateTime source) =>
        new(source.Year, source.Month, source.Day, 23, 59, 59, 999);

    /// <summary>
    /// その時刻の終了時点のタイムスタンプを返します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <returns>その時刻の終了時点のタイムスタンプ</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime EndOfHour(this DateTime source) =>
        new(source.Year, source.Month, source.Day, source.Hour, 59, 59, 999);

    /// <summary>
    /// その時刻の終了時点のタイムスタンプを返します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <returns>その時刻の終了時点のタイムスタンプ。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime EndOfMinute(this DateTime source) =>
        new(source.Year, source.Month, source.Day, source.Hour, source.Minute, 59, 999);

    /// <summary>
    /// その月の終了時点のタイムスタンプを返します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <returns>その月の終了時点のタイムスタンプ。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime EndOfMonth(this DateTime source) =>
        new(source.Year, source.Month, source.DaysInMonth(), 23, 59, 59, 999);

    /// <summary>
    /// 四半期の終了時点のタイムスタンプを返します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <returns>四半期の終了時点のタイムスタンプ。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime EndOfQuarter(this DateTime source)
    {
        var newMonth = source.Month switch
        {
            1 or 2 or 3 => 3,
            4 or 5 or 6 => 6,
            7 or 8 or 9 => 9,
            10 or 11 or 12 => 12,
            // not happen.
            _ => 1,
        };
        var newDay = DateTime.DaysInMonth(source.Year, newMonth);
        return new(source.Year, newMonth, newDay, 23, 59, 59, 999);
    }

    /// <summary>
    /// その週の終了時点のタイムスタンプを返します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <param name="beginning">週の先頭の曜日。</param>
    /// <returns>その週の終了時点のタイムスタンプ。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime EndOfWeek(this DateTime source, DayOfWeek beginning = DayOfWeek.Monday)
    {
        var beginOfWeek = (int)beginning;
        var week = (int)source.DayOfWeek;
        if (week < beginOfWeek)
        {
            week += 7;
        }
        var days = 6 - (week - beginOfWeek);
        var (year, month, day) = source.NextDay(days);

        return new(year, month, day, 23, 59, 59, 999);
    }

    /// <summary>
    /// その年の終了時点のタイムスタンプを返します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <returns>その年の終了時点のタイムスタンプ。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime EndOfYear(this DateTime source) =>
        new(source.Year, 12, 31, 23, 59, 59, 999);

    /// <summary>
    /// 月数を受け取り、その月数だけ過去の日時を返します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <param name="months">月数。</param>
    /// <returns>過去の日時。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime MonthsAgo(this DateTime source, int months) =>
        source.AddMonths(-months);

    /// <summary>
    /// 月数を受け取り、その月数だけ未来の日時を返します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <param name="months">月数。</param>
    /// <returns>未来の日時。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime MonthsSince(this DateTime source, int months) =>
        source.AddMonths(months);

    /// <summary>
    /// 指定の日数の未来の日時を取得します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <param name="days">日数。</param>
    /// <returns>未来の日時。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime NextDay(this DateTime source, int days = 1) =>
        source.AddDays(days);

    /// <summary>
    /// 指定の日数の過去の日時を取得します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <param name="days">日数。</param>
    /// <returns>過去の日時。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime PreviousDay(this DateTime source, int days = 1) =>
        source.AddDays(-days);

    /// <summary>
    /// 指定の秒数、未来の日時を取得します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <param name="seconds">秒数。</param>
    /// <returns>未来の日時。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime Since(this DateTime source, int seconds) =>
        source.AddSeconds(seconds);

    /// <summary>
    /// 指定の週数、過去の日時を取得します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <param name="weeks">週数。</param>
    /// <returns>過去の日時。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime WeeksAgo(this DateTime source, int weeks) =>
        source.AddDays(-weeks * 7);

    /// <summary>
    /// 指定の週数、未来の日時を取得します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <param name="weeks">週数。</param>
    /// <returns>未来の日時。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime WeeksSince(this DateTime source, int weeks) =>
        source.AddDays(weeks * 7);

    /// <summary>
    /// 指定の年数、過去の日時を取得します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <param name="years">年数。</param>
    /// <returns>過去の日時。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime YearsAgo(this DateTime source, int years) =>
        source.AddYears(-years);

    /// <summary>
    /// 指定の年数、未来の日時を取得します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる日時。</param>
    /// <param name="years">年数。</param>
    /// <returns>未来の日時。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime YearsSince(this DateTime source, int years) =>
        source.AddYears(years);
}
