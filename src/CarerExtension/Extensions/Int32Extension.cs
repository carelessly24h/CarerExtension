namespace CarerExtension.Extensions;

public static class Int32Extension
{
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
