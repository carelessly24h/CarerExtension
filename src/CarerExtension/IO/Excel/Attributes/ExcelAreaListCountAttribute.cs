namespace CarerExtension.IO.Excel.Attributes;

/// <summary>
/// 明細の行数、または列数を指定する属性クラス。
/// </summary>
/// <param name="count">明細データ数</param>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class ExcelAreaListCountAttribute(int count) : Attribute
{
    /// <summary>
    /// 明細数
    /// </summary>
    public int? Count => count >= 0 ? count : null;

    /// <summary>
    /// この属性で明細数を指定しているかどうか
    /// </summary>
    public bool HasCount => count >= 0;

    /// <summary>
    /// 明細の行数、または列数を指定する属性クラス。
    /// </summary>
    public ExcelAreaListCountAttribute() : this(-1)
    {
    }
}
