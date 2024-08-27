namespace CarerExtension.IO.Excel;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class ExcelCellAttribute(int rowIndex, int columnIndex) : Attribute
{
    public int RowIndex => rowIndex;

    public int ColumnIndex => columnIndex;

    public string? DataFormat { get; set; }
}
