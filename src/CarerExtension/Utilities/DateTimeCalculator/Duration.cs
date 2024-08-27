using CarerExtension.Extensions;

namespace CarerExtension.Utilities.DateTimeCalculator;

public readonly record struct Duration(long Ticks)
{
    #region internal struct
    private record struct DurationParts(int Years, int Months, int Days, int Hours, int Minutes, int Seconds, int Milliseconds)
    {
        public DurationParts(Span<int> parts) :
            this(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6])
        { }
    }
    #endregion

    #region constant
    private const long TICKS_PER_MILLISECOND = 1;

    private const long TICKS_PER_SECOND = 1000L * TICKS_PER_MILLISECOND;

    private const long TICKS_PER_MINUTE = 60L * TICKS_PER_SECOND;

    private const long TICKS_PER_HOUR = 60L * TICKS_PER_MINUTE;

    private const long TICKS_PER_DAY = 24L * TICKS_PER_HOUR;

    private const long TICKS_PER_MONTH = 2629746L * TICKS_PER_SECOND;

    private const long TICKS_PER_YEAR = 31556952L * TICKS_PER_SECOND;

    private readonly long[] DURATION_PARTS_TICKS = [
        TICKS_PER_YEAR,
        TICKS_PER_MONTH,
        TICKS_PER_DAY,
        TICKS_PER_HOUR,
        TICKS_PER_MINUTE,
        TICKS_PER_SECOND,
        TICKS_PER_MILLISECOND,
    ];
    #endregion

    #region constructor
    public Duration(int years = 0, int months = 0, int days = 0, int hours = 0, int minutes = 0, int seconds = 0, int milliseconds = 0) :
        this(Calculate(years, months, days, hours, minutes, seconds, milliseconds))
    { }
    #endregion

    #region operator +
    public static DateTime operator +(in DateTime a, in Duration b) =>
        b.Since(a);

    public static DateTime operator +(in Duration a, in DateTime b) =>
        a.Since(b);

    public static Duration operator +(in Duration a, in Duration b) =>
        new(a.Ticks + b.Ticks);

    public static Duration operator +(in Duration a, int b) =>
        new(a.Ticks + b * TICKS_PER_SECOND);

    public static Duration operator +(int a, in Duration b) =>
        new(a * TICKS_PER_SECOND + b.Ticks);

    public static Duration operator +(in Duration a, long b) =>
        new(a.Ticks + b * TICKS_PER_SECOND);

    public static Duration operator +(long a, in Duration b) =>
        new(a * TICKS_PER_SECOND + b.Ticks);

    public static Duration operator +(in Duration a, double b) =>
        new((long)(a.Ticks + b * TICKS_PER_SECOND));

    public static Duration operator +(double a, in Duration b) =>
        new((long)(a * TICKS_PER_SECOND + b.Ticks));
    #endregion

    #region operator -
    public static DateTime operator -(in DateTime a, in Duration b) =>
        b.Ago(a);

    public static Duration operator -(in Duration a, in Duration b) =>
        new(a.Ticks - b.Ticks);

    public static Duration operator -(in Duration a, int b) =>
        new(a.Ticks - b * TICKS_PER_SECOND);

    public static Duration operator -(int a, in Duration b) =>
        new(a * TICKS_PER_SECOND - b.Ticks);

    public static Duration operator -(in Duration a, long b) =>
        new(a.Ticks - b * TICKS_PER_SECOND);

    public static Duration operator -(long a, in Duration b) =>
        new(a * TICKS_PER_SECOND - b.Ticks);

    public static Duration operator -(in Duration a, double b) =>
        new((long)(a.Ticks - b * TICKS_PER_SECOND));

    public static Duration operator -(double a, in Duration b) =>
        new((long)(a * TICKS_PER_SECOND - b.Ticks));
    #endregion

    #region operator *
    public static Duration operator *(in Duration a, int b) =>
        new(a.Ticks * b);

    public static Duration operator *(int a, in Duration b) =>
        new(a * b.Ticks);

    public static Duration operator *(in Duration a, long b) =>
        new(a.Ticks * b);

    public static Duration operator *(long a, in Duration b) =>
        new(a * b.Ticks);

    public static Duration operator *(in Duration a, double b) =>
        new((long)(a.Ticks * b));

    public static Duration operator *(double a, in Duration b) =>
        new((long)(a * b.Ticks));
    #endregion

    #region operator /
    public static Duration operator /(in Duration a, int b) =>
        new(a.Ticks / b);

    public static Duration operator /(in Duration a, long b) =>
        new(a.Ticks / b);

    public static Duration operator /(in Duration a, double b) =>
        new((long)(a.Ticks / b));
    #endregion

    #region method
    #region calcurator
    public readonly DateTime FromNow() => Since(DateTime.Now);

    public readonly DateTime FromToday() => Since(DateTime.Today);

    public readonly DateTime Ago() => Ago(DateTime.Now);

    public readonly DateTime Ago(in DateTime criterion)
    {
        var (years, months, days, hours, minutes, seconds, milliseconds) = Build(Ticks);
        return criterion.Advance(
            -years,
            -months,
            weeks: 0,
            -days,
            -hours,
            -minutes,
            -seconds,
            -milliseconds);
    }

    public readonly DateTime Since() => Since(DateTime.Now);

    public readonly DateTime Since(in DateTime criterion)
    {
        var (years, months, days, hours, minutes, seconds, milliseconds) = Build(Ticks);
        return criterion.Advance(
            years,
            months,
            weeks: 0,
            days,
            hours,
            minutes,
            seconds,
            milliseconds);
    }
    #endregion

    #region converter
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long Calculate(int years, int months, int days, int hours, int minutes, int seconds, int milliseconds) =>
        years * TICKS_PER_YEAR +
        months * TICKS_PER_MONTH +
        days * TICKS_PER_DAY +
        hours * TICKS_PER_HOUR +
        minutes * TICKS_PER_MINUTE +
        seconds * TICKS_PER_SECOND +
        milliseconds * TICKS_PER_MILLISECOND;

    private readonly DurationParts Build(long ticks)
    {
        Span<int> parts = stackalloc int[DURATION_PARTS_TICKS.Length];

        var current = ticks;
        for (var index = 0; index < DURATION_PARTS_TICKS.Length; index++)
        {
            parts[index] = (int)(current / DURATION_PARTS_TICKS[index]);
            current %= DURATION_PARTS_TICKS[index];
        }

        return new(parts);
    }
    #endregion

    #region deconstruct
    public readonly void Deconstruct(out int years, out int months, out int days) =>
        (years, months, days, _, _, _, _) = Build(Ticks);

    public readonly void Deconstruct(out int years, out int months, out int days, out int hours, out int minutes, out int seconds) =>
        (years, months, days, hours, minutes, seconds, _) = Build(Ticks);

    public readonly void Deconstruct(out int years, out int months, out int days, out int hours, out int minutes, out int seconds, out int milliseconds) =>
        (years, months, days, hours, minutes, seconds, milliseconds) = Build(Ticks);
    #endregion

    public readonly bool Equals(Duration d) => Ticks == d.Ticks;

    public readonly override int GetHashCode() =>
        EqualityComparer<long>.Default.GetHashCode(Ticks);
    #endregion
}
