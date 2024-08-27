namespace CarerExtension.Utilities.LegacyDataFormatter;

public struct ByteArraySlicer(IEnumerable<byte> buffer)
{
    #region variable
    private int offset = 0;

    private readonly int length = buffer.Count();
    #endregion

    #region property
    private readonly IEnumerable<byte> Buffer => buffer;

    public readonly int BufferLength => length;

    public readonly bool IsEmpty => buffer.Count() <= offset;

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

    public readonly IEnumerable<byte> Peek(int count = 1) =>
        buffer.Skip(offset).Take(count) ?? Array.Empty<byte>();

    public readonly IEnumerable<byte> PeekAll() =>
        buffer.Skip(offset) ?? Array.Empty<byte>();

    public ByteArraySlicer Refresh()
    {
        offset = 0;
        return this;
    }

    public ByteArraySlicer Shift(int count)
    {
        var newOffset = offset + count;
        offset = Math.Clamp(newOffset, 0, BufferLength);
        return this;
    }

    public IEnumerable<byte> Slice(int count = 1)
    {
        var s = Peek(count);
        Shift(count);
        return s;
    }

    public IEnumerable<byte> SliceAll()
    {
        var s = PeekAll();
        Shift(s.Count());
        return s;
    }
    #endregion
}
