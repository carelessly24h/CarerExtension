namespace CarerExtension.Utilities.DateTimeCalculator;

public static class Int32Extension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Duration Years(this int source) => new(years: source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Duration Months(this int source) => new(months: source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Duration Fortnights(this int source) => new(days: source * 14);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Duration Weeks(this int source) => new(days: source * 7);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Duration Days(this int source) => new(days: source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Duration HalfDays(this int source) => new(hours: source * 12);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Duration Hours(this int source) => new(hours: source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Duration Minutes(this int source) => new(minutes: source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Duration Seconds(this int source) => new(seconds: source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Duration Milliseconds(this int source) => new(milliseconds: source);
}
