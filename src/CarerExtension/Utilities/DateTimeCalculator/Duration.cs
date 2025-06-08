using CarerExtension.Extensions;

namespace CarerExtension.Utilities.DateTimeCalculator;

/// <summary>
/// 時間の期間を操作できる単純なメカニズムを提供する構造体です。
/// </summary>
/// <remarks>
/// active support like.
/// </remarks>
/// <param name="Ticks">任意の期間の日付と時刻を表すタイマー刻み数</param>
public readonly record struct Duration(long Ticks)
{
    #region internal struct
    /// <summary>
    /// 指定された期間の年、月、日、時、分、秒、およびミリ秒を保持する構造体です。
    /// <param name="Years">期間の年</param>
    /// <param name="Months">期間の月</param>
    /// <param name="Days">期間の日</param>
    /// <param name="Hours">期間の時</param>
    /// <param name="Minutes">期間の分</param>
    /// <param name="Seconds">期間の秒</param>
    /// <param name="Milliseconds">期間のミリ秒</param>
    private record struct DurationParts(int Years, int Months, int Days, int Hours, int Minutes, int Seconds, int Milliseconds)
    {
        public DurationParts(in Span<int> parts) :
            this(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6])
        { }
    }
    #endregion

    #region constant
    /// <summary>
    /// 平均化した月間のTicks数を表す定数です。
    /// </summary>
    /// <remarks>
    /// この定数は、ユリウス年を12分割した値です。
    /// </remarks>
    private const long TicksPerMonth = 2629746L * TimeSpan.TicksPerSecond;

    /// <summary>
    /// 平均化した年間のTicks数を表す定数です。
    /// </summary>
    /// <remarks>
    /// この定数はユリウス年を表します。
    /// </remarks>
    private const long TicksPerYear = 31556952L * TimeSpan.TicksPerSecond;

    /// <summary>
    /// 年、月、日、時、分、秒、およびミリ秒の各部分のTicks数を表す配列です。
    /// </summary>
    private readonly long[] DulationPartsTicks = [
        TicksPerYear,
        TicksPerMonth,
        TimeSpan.TicksPerDay,
        TimeSpan.TicksPerHour,
        TimeSpan.TicksPerMinute,
        TimeSpan.TicksPerSecond,
        TimeSpan.TicksPerMillisecond,
    ];
    #endregion

    #region constructor
    /// <summary>
    /// 時間の期間を操作できる単純なメカニズムを提供する構造体です。
    /// </summary>
    /// <param name="years">期間の年</param>
    /// <param name="months">期間の月</param>
    /// <param name="days">期間の日</param>
    /// <param name="hours">期間の時</param>
    /// <param name="minutes">期間の分</param>
    /// <param name="seconds">期間の秒</param>
    /// <param name="milliseconds">期間のミリ秒</param>
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
        new(a.Ticks + b * TimeSpan.TicksPerSecond);

    public static Duration operator +(int a, in Duration b) =>
        new(a * TimeSpan.TicksPerSecond + b.Ticks);

    public static Duration operator +(in Duration a, long b) =>
        new(a.Ticks + b * TimeSpan.TicksPerSecond);

    public static Duration operator +(long a, in Duration b) =>
        new(a * TimeSpan.TicksPerSecond + b.Ticks);

    public static Duration operator +(in Duration a, double b) =>
        new((long)(a.Ticks + b * TimeSpan.TicksPerSecond));

    public static Duration operator +(double a, in Duration b) =>
        new((long)(a * TimeSpan.TicksPerSecond + b.Ticks));
    #endregion

    #region operator -
    public static DateTime operator -(in DateTime a, in Duration b) =>
        b.Ago(a);

    public static Duration operator -(in Duration a, in Duration b) =>
        new(a.Ticks - b.Ticks);

    public static Duration operator -(in Duration a, int b) =>
        new(a.Ticks - b * TimeSpan.TicksPerSecond);

    public static Duration operator -(int a, in Duration b) =>
        new(a * TimeSpan.TicksPerSecond - b.Ticks);

    public static Duration operator -(in Duration a, long b) =>
        new(a.Ticks - b * TimeSpan.TicksPerSecond);

    public static Duration operator -(long a, in Duration b) =>
        new(a * TimeSpan.TicksPerSecond - b.Ticks);

    public static Duration operator -(in Duration a, double b) =>
        new((long)(a.Ticks - b * TimeSpan.TicksPerSecond));

    public static Duration operator -(double a, in Duration b) =>
        new((long)(a * TimeSpan.TicksPerSecond - b.Ticks));
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
    /// <summary>
    /// システム日時から期間が経過した、未来の日時を取得します。
    /// </summary>
    /// <returns>システム日時から期間が経過した、未来の日時。</returns>
    public readonly DateTime FromNow() => Since(DateTime.Now);

    /// <summary>
    /// システム日付から期間が経過した、未来の日時を取得します。
    /// </summary>
    /// <returns>システム日付から期間が経過した、未来の日時。</returns>
    public readonly DateTime FromToday() => Since(DateTime.Today);

    /// <summary>
    /// システム日時から期間分、過去の日時を取得します。
    /// </summary>
    /// <returns>システム日時から期間の分、過去の日時。</returns>
    public readonly DateTime Ago() => Ago(DateTime.Now);

    /// <summary>
    /// 指定された基準日から期間分、過去の日時を取得します。
    /// </summary>
    /// <param name="criterion">基準となる基準日。</param>
    /// <returns>指定された基準日から期間分、過去の日時。</returns>
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

    /// <summary>
    /// システム日時から期間が経過した、未来の日時を取得します。
    /// </summary>
    /// <returns>システム日時から期間が経過した、未来の日時。</returns>
    public readonly DateTime Since() => Since(DateTime.Now);

    /// <summary>
    /// 指定された基準日から期間が経過した、未来の日時を取得します。
    /// </summary>
    /// <returns>指定された基準日から期間が経過した、未来の日時。</returns>
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
    /// <summary>
    /// 引数によって指定された年、月、日、時、分、秒、およびミリ秒をTicks数に変換します。
    /// </summary>
    /// <param name="years">Ticksに変換する年数。</param>
    /// <param name="months">Ticksに変換する月数。</param>
    /// <param name="days">Ticksに変換する日数。</param>
    /// <param name="hours">Ticksに変換する時数。</param>
    /// <param name="minutes">Ticksに変換する分数。</param>
    /// <param name="seconds">Ticksに変換する秒数。</param>
    /// <param name="milliseconds">Ticksに変換するミリ秒数。</param>
    /// <returns>指定された日時によって表されるTick数。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long Calculate(int years, int months, int days, int hours, int minutes, int seconds, int milliseconds) =>
        years * TicksPerYear +
        months * TicksPerMonth +
        days * TimeSpan.TicksPerDay +
        hours * TimeSpan.TicksPerHour +
        minutes * TimeSpan.TicksPerMinute +
        seconds * TimeSpan.TicksPerSecond +
        milliseconds * TimeSpan.TicksPerMillisecond;

    /// <summary>
    /// Ticksから年、月、日、時、分、秒、およびミリ秒の各パーツを計算します。
    /// </summary>
    /// <param name="ticks">計算元のTicks。</param>
    /// <returns>計算された期間を表すインスタンス。</returns>
    private readonly DurationParts Build(long ticks)
    {
        Span<int> parts = stackalloc int[DulationPartsTicks.Length];

        var current = ticks;
        for (var index = 0; index < DulationPartsTicks.Length; index++)
        {
            parts[index] = (int)(current / DulationPartsTicks[index]);
            current %= DulationPartsTicks[index];
        }

        return new(parts);
    }
    #endregion

    #region deconstruct
    /// <summary>
    /// 期間を年月日に分解します。
    /// </summary>
    /// <param name="years">分解後の年。</param>
    /// <param name="months">分解後の月。</param>
    /// <param name="days">分解後の日。</param>
    public readonly void Deconstruct(out int years, out int months, out int days) =>
        (years, months, days, _, _, _, _) = Build(Ticks);

    /// <summary>
    /// 期間を年月日時分秒に分解します。
    /// </summary>
    /// <param name="years">分解後の年。</param>
    /// <param name="months">分解後の月。</param>
    /// <param name="days">分解後の日。</param>
    /// <param name="hours">分解後の時。</param>
    /// <param name="minutes">分解後の分。</param>
    /// <param name="seconds">分解後の秒。</param>
    public readonly void Deconstruct(out int years, out int months, out int days, out int hours, out int minutes, out int seconds) =>
        (years, months, days, hours, minutes, seconds, _) = Build(Ticks);

    /// <summary>
    /// 期間を年月日時分秒ミリ秒に分解します。
    /// </summary>
    /// <param name="years">分解後の年。</param>
    /// <param name="months">分解後の月。</param>
    /// <param name="days">分解後の日。</param>
    /// <param name="hours">分解後の時。</param>
    /// <param name="minutes">分解後の分。</param>
    /// <param name="seconds">分解後の秒。</param>
    /// <param name="milliseconds">分解後のミリ秒。</param>
    public readonly void Deconstruct(out int years, out int months, out int days, out int hours, out int minutes, out int seconds, out int milliseconds) =>
        (years, months, days, hours, minutes, seconds, milliseconds) = Build(Ticks);
    #endregion

    /// <summary>
    /// 2 つのオブジェクト インスタンスが等しいかどうかを判断します。
    /// </summary>
    /// <param name="obj">現在のオブジェクトと比較するオブジェクト。</param>
    /// <returns>
    /// 指定したオブジェクトが現在のオブジェクトと等しい場合は<see langword="true"/>。
    /// それ以外の場合は、<see langword="false"/>です。
    /// </returns>
    public readonly bool Equals(Duration obj) => Ticks == obj.Ticks;

    /// <summary>
    /// 既定のハッシュ関数として機能します。
    /// </summary>
    /// <returns>現在のオブジェクトのハッシュコード。</returns>
    public readonly override int GetHashCode() =>
        EqualityComparer<long>.Default.GetHashCode(Ticks);
    #endregion
}
