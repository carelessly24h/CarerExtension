namespace CarerExtension.IO.Excel.Attributes;

/// <summary>
/// Excelシートの特定のセルに対応するプロパティを表す属性クラス。
/// </summary>
/// <param name="rowIndex">行インデックス</param>
/// <param name="columnIndex">列インデックス</param>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class ExcelCellAttribute(int rowIndex, int columnIndex) : Attribute
{
    /// <summary>
    /// 行インデックス
    /// </summary>
    public int RowIndex => rowIndex;

    /// <summary>
    /// 列インデックス
    /// </summary>
    public int ColumnIndex => columnIndex;

    /// <summary>
    /// 組み込みのフォーマットに一致する文字列
    /// </summary>
    public string? DataFormat { get; set; }
}
