using CarerExtension.Extensions;

namespace CarerExtension.Utilities.LegacyDataFormatter;

public ref struct ByteArraySlicer(ReadOnlySpan<byte> buffer)
{
    #region variable
    private int offset = 0;
    #endregion

    #region property
    public readonly ReadOnlySpan<byte> Buffer { get; } = buffer;

    public readonly int BufferLength { get; } = buffer.Length;

    public readonly bool IsEmpty => BufferLength <= offset;

    public readonly bool IsPresent => !IsEmpty;
    #endregion

    #region constructor
    public ByteArraySlicer(ByteArraySlicer slicer) : this(slicer.Buffer)
    {
        offset = slicer.offset;
    }
    #endregion

    #region operator
    public static ByteArraySlicer operator +(ByteArraySlicer slicer, int count) =>
        slicer.Clone().Shift(count);

    public static ByteArraySlicer operator -(ByteArraySlicer slicer, int count) =>
        slicer.Clone().Shift(-count);
    #endregion

    #region method
    public readonly ByteArraySlicer Clone() => new(this);

    public readonly ReadOnlySpan<byte> Peek(int count = 1) =>
        Buffer.SafetySlice(offset, count);

    public readonly ReadOnlySpan<byte> PeekAll() => Buffer[offset..];

    public ByteArraySlicer Refresh()
    {
        offset = 0;
        return this;
    }

    public ByteArraySlicer Shift(int count)
    {
        offset = Math.Clamp(offset + count, 0, BufferLength);
        return this;
    }

    public ReadOnlySpan<byte> Slice(int count = 1)
    {
        var s = Peek(count);
        Shift(count);
        return s;
    }

    public ReadOnlySpan<byte> SliceAll()
    {
        var s = PeekAll();
        Shift(s.Length);
        return s;
    }
    #endregion
}
