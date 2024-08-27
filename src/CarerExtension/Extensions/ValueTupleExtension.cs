namespace CarerExtension.Extensions;

public static class ValueTupleExtension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> Range(this (int Start, int Count) source) =>
        Enumerable.Range(source.Start, source.Count);
}
