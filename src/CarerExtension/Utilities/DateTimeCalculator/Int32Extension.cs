namespace CarerExtension.Utilities.DateTimeCalculator;

/// <summary>
/// Int32 extension methods for creating <see cref="Duration"/> instances.
/// </summary>
public static class Int32Extension
{
    /// <summary>
    /// 数値を期間の年数として解釈し、<see cref="Duration"/>を返します。
    /// </summary>
    /// <param name="source">年数として扱う数値。</param>
    /// <returns>年数を表す<see cref="Duration"/>。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Duration Years(this int source) => new(years: source);

    /// <summary>
    /// 数値を期間の月数として解釈し、<see cref="Duration"/>を返します。
    /// </summary>
    /// <param name="source">月数として扱う数値。</param>
    /// <returns>月数を表す<see cref="Duration"/>。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Duration Months(this int source) => new(months: source);

    /// <summary>
    /// 数値を期間の2週間として解釈し、<see cref="Duration"/>を返します。
    /// </summary>
    /// <param name="source">2週間として扱う数値。</param>
    /// <returns>2週間を表す<see cref="Duration"/>。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Duration Fortnights(this int source) => new(days: source * 14);

    /// <summary>
    /// 数値を期間の週数として解釈し、<see cref="Duration"/>を返します。
    /// </summary>
    /// <param name="source">週数として扱う数値。</param>
    /// <returns>週数を表す<see cref="Duration"/>。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Duration Weeks(this int source) => new(days: source * 7);

    /// <summary>
    /// 数値を期間の日数として解釈し、<see cref="Duration"/>を返します。
    /// </summary>
    /// <param name="source">日数として扱う数値。</param>
    /// <returns>日数を表す<see cref="Duration"/>。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Duration Days(this int source) => new(days: source);

    /// <summary>
    /// 数値を期間の12時間として解釈し、<see cref="Duration"/>を返します。
    /// </summary>
    /// <param name="source">12時間として扱う数値。</param>
    /// <returns>12時間を表す<see cref="Duration"/>。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Duration HalfDays(this int source) => new(hours: source * 12);

    /// <summary>
    /// 数値を期間の時数として解釈し、<see cref="Duration"/>を返します。
    /// </summary>
    /// <param name="source">時数として扱う数値。</param>
    /// <returns>時数を表す<see cref="Duration"/>。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Duration Hours(this int source) => new(hours: source);

    /// <summary>
    /// 数値を期間の分数として解釈し、<see cref="Duration"/>を返します。
    /// </summary>
    /// <param name="source">分数として扱う数値。</param>
    /// <returns>分数を表す<see cref="Duration"/>。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Duration Minutes(this int source) => new(minutes: source);

    /// <summary>
    /// 数値を期間の秒数として解釈し、<see cref="Duration"/>を返します。
    /// </summary>
    /// <param name="source">秒数として扱う数値。</param>
    /// <returns>秒数を表す<see cref="Duration"/>。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Duration Seconds(this int source) => new(seconds: source);

    /// <summary>
    /// 数値を期間のミリ秒数として解釈し、<see cref="Duration"/>を返します。
    /// </summary>
    /// <param name="source">ミリ秒数として扱う数値。</param>
    /// <returns>ミリ秒数を表す<see cref="Duration"/>。</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Duration Milliseconds(this int source) => new(milliseconds: source);
}
