namespace CarerExtension.Extensions;

/// <summary>
/// Int32 extension methods.
/// </summary>
public static class Int32Extension
{
    /// <summary>
    /// レシーバの数値が指定した数値の倍数かどうかを判定します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる数値。</param>
    /// <param name="value">数値。</param>
    /// <returns>
    /// レシーバの数値が指定した数値の倍数の場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool MultipleOf(this int source, int value)
    {
        if (value == 0)
        {
            return source == 0;
        }
        else
        {
            return source % value == 0;
        }
    }

    /// <summary>
    /// レシーバの数値が指定した数値の倍数かどうかを判定します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる数値。</param>
    /// <param name="value">数値。</param>
    /// <returns>
    /// レシーバの数値が指定した数値の倍数の場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool MultipleOf(this int source, long value)
    {
        if (value == 0)
        {
            return source == 0;
        }
        else
        {
            return source % value == 0;
        }
    }

    /// <summary>
    /// レシーバの数値が指定した数値の倍数かどうかを判定します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる数値。</param>
    /// <param name="value">数値。</param>
    /// <returns>
    /// レシーバの数値が指定した数値の倍数の場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool MultipleOf(this int source, double value)
    {
        if (value == 0)
        {
            return source == 0;
        }
        else
        {
            return source % value == 0;
        }
    }

    /// <summary>
    /// レシーバの数値が指定した数値の倍数かどうかを判定します。
    /// </summary>
    /// <remarks>active support like.</remarks>
    /// <param name="source">基準となる数値。</param>
    /// <param name="value">数値。</param>
    /// <returns>
    /// レシーバの数値が指定した数値の倍数の場合は<see langword="true"/>。
    /// そうでない場合は<see langword="false"/>。
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool MultipleOf(this int source, decimal value)
    {
        if (value == 0)
        {
            return source == 0;
        }
        else
        {
            return source % value == 0;
        }
    }
}
