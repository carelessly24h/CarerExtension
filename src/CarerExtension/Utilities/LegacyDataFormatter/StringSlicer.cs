using CarerExtension.Extensions;

namespace CarerExtension.Utilities.LegacyDataFormatter;

public struct StringSlicer(string buffer)
{
    #region variable
    private int offset = 0;
    #endregion

    #region property
    private readonly string Buffer => buffer;

    public readonly int BufferLength => buffer.Length;

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

    public readonly string Peek(int count = 1) =>
        buffer.SafetySubstring(offset, count) ?? string.Empty;

    public readonly string PeekAll() =>
        buffer.SafetySubstring(offset) ?? string.Empty;

    public StringSlicer Refresh()
    {
        offset = 0;
        return this;
    }

    public StringSlicer Shift(int count)
    {
        var newOffset = offset + count;
        offset = Math.Clamp(newOffset, 0, BufferLength);
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
