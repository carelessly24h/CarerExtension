namespace CarerExtension.Extensions;

public static class DoubleExtension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool MultipleOf(this double source, int value)
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool MultipleOf(this double source, long value)
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool MultipleOf(this double source, double value)
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool MultipleOf(this double source, decimal value)
    {
        if (value == 0)
        {
            return source == 0;
        }
        else
        {
            return source % (double)value == 0;
        }
    }
}
