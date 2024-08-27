namespace CarerExtension.Extensions;

public static class Int64Extension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool MultipleOf(this long source, int value)
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
    public static bool MultipleOf(this long source, long value)
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
    public static bool MultipleOf(this long source, double value)
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
    public static bool MultipleOf(this long source, decimal value)
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
