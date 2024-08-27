namespace CarerExtension.Extensions;

public static class ByteExtension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All(this byte source) =>
        source == byte.MaxValue;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All(this byte source, byte mask) =>
        (source & mask) == mask;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Bit(this byte source, int position)
    {
        if (position < 0 || 7 < position)
        {
            throw new ArgumentOutOfRangeException(nameof(position));
        }

        var mask = 1 << position;
        return (source & mask) != 0;
    }
}
