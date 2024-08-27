namespace CarerExtension.Extensions;

/// <summary>
/// DateTime struct extension.
/// </summary>
public static class DateTimeExtension
{
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime Ago(this DateTime source, int seconds) => source.AddSeconds(-seconds);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime BeginningOfDay(this DateTime source) =>
        source.Date;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime BeginningOfHour(this DateTime source) =>
        new(source.Year, source.Month, source.Day, source.Hour, 0, 0);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime BeginningOfMonth(this DateTime source) =>
        new(source.Year, source.Month, 1);

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime BeginningOfYear(this DateTime source) => new(source.Year, 1, 1);

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int DaysInMonth(this DateTime source) =>
        DateTime.DaysInMonth(source.Year, source.Month);

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime EndOfDay(this DateTime source) =>
        new(source.Year, source.Month, source.Day, 23, 59, 59, 999);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime EndOfHour(this DateTime source) =>
        new(source.Year, source.Month, source.Day, source.Hour, 59, 59, 999);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime EndOfMinute(this DateTime source) =>
        new(source.Year, source.Month, source.Day, source.Hour, source.Minute, 59, 999);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime EndOfMonth(this DateTime source) =>
        new(source.Year, source.Month, source.DaysInMonth(), 23, 59, 59, 999);

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime EndOfYear(this DateTime source) =>
        new(source.Year, 12, 31, 23, 59, 59, 999);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime MonthsAgo(this DateTime source, int months) =>
        source.AddMonths(-months);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime MonthsSince(this DateTime source, int months) =>
        source.AddMonths(months);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime NextDay(this DateTime source, int days = 1) =>
        source.AddDays(days);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime PreviousDay(this DateTime source, int days = 1) =>
        source.AddDays(-days);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime Since(this DateTime source, int seconds) =>
        source.AddSeconds(seconds);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime WeeksAgo(this DateTime source, int weeks) =>
        source.AddDays(-weeks * 7);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime WeeksSince(this DateTime source, int weeks) =>
        source.AddDays(weeks * 7);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime YearsAgo(this DateTime source, int years) =>
        source.AddYears(-years);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static DateTime YearsSince(this DateTime source, int years) =>
        source.AddYears(years);
}
