using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace CarerExtension.IO.Excel;

public abstract class ExcelSheetIO(IWorkbook workbook) : IExcelSheet
{
    public readonly record struct ExcelCell(int RowIndex, int ColumnIndex, object? Value, string? DataFormat = null);

    #region variable
    protected readonly IWorkbook workbook = workbook;

    protected ISheet? worksheet;
    #endregion

    #region property
    private ISheet Worksheet => worksheet ?? throw new InvalidOperationException("Worksheet is not selected.");

    public int SheetIndex => workbook.GetSheetIndex(worksheet);

    public string SheetName
    {
        get => Worksheet.SheetName;
        set => workbook.SetSheetName(SheetIndex, value);
    }
    #endregion

    #region method
    protected virtual void ReflectionEdit()
    {
        foreach (var (prop, attr) in GetExcelCellProperties())
        {
            var cell = new ExcelCell(attr.RowIndex, attr.ColumnIndex, prop.GetValue(this), attr.DataFormat);
            Edit(cell);
        }
    }

    protected virtual void ReflectionRead()
    {
        foreach (var (prop, attr) in GetExcelCellProperties())
        {
            var value = GetValue(prop.PropertyType, attr.RowIndex, attr.ColumnIndex);
            prop.SetValue(this, value);
        }
    }

    private IEnumerable<(PropertyInfo, ExcelCellAttribute)> GetExcelCellProperties() =>
        GetType().
        GetProperties().
        Select(p => (Prop: p, Attr: p.GetCustomAttribute<ExcelCellAttribute>())).
        Where(s => s.Attr != null).
        Cast<(PropertyInfo, ExcelCellAttribute)>();

    #region sheet-operation
    public virtual void Create(string sheetName)
    {
        worksheet = workbook.CreateSheet(sheetName);
    }

    public virtual void Select(int index)
    {
        worksheet = workbook.GetSheetAt(index);
    }

    public virtual void Select(string sheetName)
    {
        worksheet = workbook.GetSheet(sheetName) ?? throw new ArgumentException("Illegal sheet name.");
    }

    public virtual void Write() => throw new NotImplementedException();

    public virtual void Read() => throw new NotImplementedException();

    protected virtual void Copy(string destinationSheetName) =>
        Worksheet.CopyTo(workbook, destinationSheetName, true, true);
    #endregion

    #region cell-operation
    public virtual void CopyRow(int sourceIndex, int destinationIndex) =>
        SheetUtil.CopyRow(worksheet, sourceIndex, destinationIndex);

    private ICell GetCell(int rowIndex, int columnIndex)
    {
        var row = CellUtil.GetRow(rowIndex, worksheet);
        return CellUtil.GetCell(row, columnIndex);
    }
    #endregion

    #region cell-reading
    private object? GetValue(Type dataType, int rowIndex, int columnIndex) => dataType switch
    {
        Type t when t == typeof(bool?) => GetBooleanValue(rowIndex, columnIndex),
        Type t when t == typeof(DateTime?) => GetDateTimeValue(rowIndex, columnIndex),
        Type t when t == typeof(double?) => GetDoubleValue(rowIndex, columnIndex),
        Type t when t == typeof(int?) => GetInt32Value(rowIndex, columnIndex),
        Type t when t == typeof(long?) => GetInt64Value(rowIndex, columnIndex),
        Type t when t == typeof(string) => GetStringValue(rowIndex, columnIndex),
        _ => null,
    };

    protected virtual bool? GetBooleanValue(int rowIndex, int columnIndex) =>
        GetCellValue<bool?>(GetCell(rowIndex, columnIndex));

    protected virtual DateTime? GetDateTimeValue(int rowIndex, int columnIndex) =>
        GetDoubleValue(rowIndex, columnIndex) switch
        {
            double v => DateTime.FromOADate(v),
            _ => null,
        };

    protected virtual double? GetDoubleValue(int rowIndex, int columnIndex) =>
        GetCellValue<double?>(GetCell(rowIndex, columnIndex));

    protected virtual int? GetInt32Value(int rowIndex, int columnIndex) =>
        (int?)GetCellValue<double?>(GetCell(rowIndex, columnIndex));

    protected virtual long? GetInt64Value(int rowIndex, int columnIndex) =>
        (long?)GetCellValue<double?>(GetCell(rowIndex, columnIndex));

    protected virtual string? GetStringValue(int rowIndex, int columnIndex) =>
        GetCellValue<string?>(GetCell(rowIndex, columnIndex));

    private static T? GetCellValue<T>(ICell cell) => GetCellValue(cell.CellType, cell) switch
    {
        T v => v,
        "" => default,
        null => default,
        var v => throw new FormatException($"Invalid value type. T: ${typeof(T)} Cell-Type: ${v.GetType()} "),
    };

    private static object? GetCellValue(CellType type, ICell cell) => type switch
    {
        CellType.Boolean => cell.BooleanCellValue,
        CellType.Numeric => cell.NumericCellValue,
        CellType.String => cell.StringCellValue,
        CellType.Blank => "",
        CellType.Formula => GetCellValue(cell.CachedFormulaResultType, cell),
        _ => null,
    };
    #endregion

    #region cell-writing
    protected virtual void Edit(IEnumerable<ExcelCell> cells)
    {
        foreach (var cell in cells)
        {
            Edit(cell);
        }
    }

    protected virtual void Edit(ExcelCell cell) =>
        Edit(GetCell(cell.RowIndex, cell.ColumnIndex), cell.Value, cell.DataFormat);

    protected virtual void Edit(int rowIndex, int columnIndex, object? value, string? dataFormat = null) =>
        Edit(GetCell(rowIndex, columnIndex), value, dataFormat);

    protected virtual void Edit(ICell cell, object? value, string? dataFormat = null)
    {
        cell.SetCellValue(value);

        var format = dataFormat ?? GetDefaultFormat(value);
        if (format != null)
        {
            workbook.SetCellFormat(cell, format);
        }
    }

    protected virtual string? GetDefaultFormat(object? value) => value switch
    {
        DateTime => "YYYY/M/D",
        double => "#.0",
        _ => null,
    };
    #endregion
    #endregion
}
