using CarerExtension.Extensions;

namespace CarerExtension.Utilities.LegacyDataFormatter;

/// <summary>
/// 連続するバイトのデータを、任意の長さでスライスするための構造体です。
/// スライスしたデータ長を保持することで、繰り返しスライスした際に、データを部分的に切り取ったようなデータを返します。
/// </summary>
/// <param name="buffer">管理するバイトの配列</param>
public ref struct ByteArraySlicer(ReadOnlySpan<byte> buffer)
{
    #region variable
    /// <summary>
    /// 現在の管理中のデータの先頭位置を示すオフセット値です。
    /// </summary>
    private int offset = 0;
    #endregion

    #region property
    /// <summary>
    /// 管理しているデータを取得します。
    /// </summary>
    public readonly ReadOnlySpan<byte> Buffer { get; } = buffer;

    /// <summary>
    /// 管理しているデータの長さを取得します。
    /// </summary>
    public readonly int BufferLength { get; } = buffer.Length;

    /// <summary>
    /// すべてのデータをスライス済みかどうかを示すプロパティです。
    /// </summary>
    public readonly bool IsEmpty => BufferLength <= offset;

    /// <summary>
    /// まだスライスしていないデータが存在するかどうかを示すプロパティです。
    /// </summary>
    public readonly bool IsPresent => !IsEmpty;
    #endregion

    #region constructor
    /// <summary>
    /// 連続するバイトのデータを、任意の長さでスライスするための構造体を、コピーするためのコンストラクタです。
    /// </summary>
    /// <param name="slicer">コピー元の<see cref="ByteArraySlicer"/></param>
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
    /// <summary>
    /// 現在のオブジェクトを新しい<see cref="ByteArraySlicer"/>として複製します。
    /// </summary>
    /// <returns>複製した<see cref="ByteArraySlicer"/></returns>
    public readonly ByteArraySlicer Clone() => new(this);

    /// <summary>
    /// 管理中のデータの先頭から、スライスしたデータを削除せずに返します。
    /// </summary>
    /// <param name="count">切り出すサイズ</param>
    /// <returns>スライスしたデータ。</returns>
    public readonly ReadOnlySpan<byte> Peek(int count = 1) =>
        Buffer.SafetySlice(offset, count);

    /// <summary>
    /// 管理中のデータの先頭から、すべてのデータを削除せずに返します。
    /// </summary>
    /// <returns>管理中のすべてのデータ</returns>
    public readonly ReadOnlySpan<byte> PeekAll() => Buffer[offset..];

    /// <summary>
    /// 管理中のデータの状態をリセットします。
    /// </summary>
    /// <returns>現在のインスタンス。</returns>
    public ByteArraySlicer Refresh()
    {
        offset = 0;
        return this;
    }

    /// <summary>
    /// 管理中のデータの先頭から、指定した数だけデータをシフトします。
    /// </summary>
    /// <param name="count">シフトするデータ数。</param>
    /// <returns>現在のインスタンス。</returns>
    public ByteArraySlicer Shift(int count)
    {
        offset = Math.Clamp(offset + count, 0, BufferLength);
        return this;
    }

    /// <summary>
    /// 管理中のデータの先頭から、スライスしたデータを返します。
    /// </summary>
    /// <param name="count">スライスするデータ数。</param>
    /// <returns>スライスしたデータ。</returns>
    public ReadOnlySpan<byte> Slice(int count = 1)
    {
        var s = Peek(count);
        Shift(count);
        return s;
    }

    /// <summary>
    /// 管理中のデータの先頭から、すべてのデータを返します。
    /// </summary>
    /// <returns>管理中のすべてのデータ</returns>
    public ReadOnlySpan<byte> SliceAll()
    {
        var s = PeekAll();
        Shift(s.Length);
        return s;
    }
    #endregion
}
