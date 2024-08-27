namespace CarerExtension.Extensions;

public static class DecimalExtension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool MultipleOf(this decimal source, int value)
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
    public static bool MultipleOf(this decimal source, long value)
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
    public static bool MultipleOf(this decimal source, double value)
    {
        if (value == 0)
        {
            return source == 0;
        }
        else
        {
            return (double)source % value == 0;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool MultipleOf(this decimal source, decimal value)
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
