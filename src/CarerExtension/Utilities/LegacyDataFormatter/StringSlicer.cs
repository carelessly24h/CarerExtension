using CarerExtension.Extensions;

namespace CarerExtension.Utilities.LegacyDataFormatter;

public ref struct StringSlicer(ReadOnlySpan<char> buffer)
{
    #region variable
    private int offset = 0;
    #endregion

    #region property
    private readonly ReadOnlySpan<char> Buffer { get; } = buffer;

    public readonly int BufferLength => Buffer.Length;

    public readonly bool IsEmpty => BufferLength <= offset;

    public readonly bool IsPresent => !IsEmpty;
    #endregion

    #region constructor
    public StringSlicer(StringSlicer slicer) : this(slicer.Buffer)
    {
        offset = slicer.offset;
    }
    #endregion

    #region operator
    public static StringSlicer operator +(StringSlicer slicer, int count) =>
        slicer.Clone().Shift(count);

    public static StringSlicer operator -(StringSlicer slicer, int count) =>
        slicer.Clone().Shift(-count);
    #endregion

    #region method
    public readonly StringSlicer Clone() => new(this);

    public readonly string Peek(int count = 1)
    {
        var s = Buffer.SafetySlice(offset, count);
        return new string(s);
    }

    public readonly string PeekAll()
    {
        var s = Buffer.SafetySlice(offset);
        return new string(s);
    }

    public StringSlicer Refresh()
    {
        offset = 0;
        return this;
    }

    public StringSlicer Shift(int count)
    {
        offset = Math.Clamp(offset + count, 0, BufferLength);
        return this;
    }

    public string Slice(int count = 1)
    {
        var s = Peek(count);
        Shift(count);
        return s;
    }

    public string SliceAll()
    {
        var s = PeekAll();
        Shift(s.Length);
        return s;
    }
    #endregion
}
