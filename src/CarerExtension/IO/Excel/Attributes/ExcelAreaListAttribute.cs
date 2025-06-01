namespace CarerExtension.IO.Excel.Attributes;

/// <summary>
/// Excelの明細行、または明細列を表す属性クラス。
/// </summary>
/// <param name="topRowIndex">先頭の行インデックス</param>
/// <param name="topColumnIndex">先頭の列インデックス</param>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class ExcelAreaListAttribute(int topRowIndex, int topColumnIndex) : Attribute
{
    /// <summary>
    /// 明細行の先頭の行インデックス
    /// </summary>
    public int TopRowIndex => topRowIndex;

    /// <summary>
    /// 明細行の先頭の列インデックス
    /// </summary>
    public int TopColumnIndex => topColumnIndex;

    /// <summary>
    /// 明細行の1行がExcelの何行分に対応するかを表します。
    /// </summary>
    public int ListRowSize { get; set; } = 0;

    /// <summary>
    /// 明細列の1列がExcelの何列分に対応するかを表します。
    /// </summary>
    public int ListColumnSize { get; set; } = 0;

    /// <summary>
    /// Excelの明細行、または明細列を表す属性クラス。
    /// </summary>
    /// <remarks>
    /// エクセルシートの(0, 0)セルを基準位置に設定。
    /// </remarks>
    public ExcelAreaListAttribute() : this(0, 0)
    {
    }
}
