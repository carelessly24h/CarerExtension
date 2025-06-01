namespace CarerExtension.IO.Excel.Attributes;

/// <summary>
/// Excelシートの複数のセルに対応するエリアを表す属性クラス。
/// </summary>
/// <param name="topRowIndex">先頭の行インデックス</param>
/// <param name="topColumnIndex">先頭の列インデックス</param>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class ExcelAreaAttribute(int topRowIndex, int topColumnIndex) : Attribute
{
    /// <summary>
    /// エリアの先頭の行インデックス
    /// </summary>
    public int TopRowIndex => topRowIndex;

    /// <summary>
    /// エリアの先頭の列インデックス
    /// </summary>
    public int TopColumnIndex => topColumnIndex;

    /// <summary>
    /// Excelシートの複数のセルに対応するエリアを表す属性クラス。
    /// </summary>
    /// <remarks>
    /// エクセルシートの(0, 0)セルを基準位置に設定。
    /// </remarks>
    public ExcelAreaAttribute() : this(0, 0)
    {
    }
}
