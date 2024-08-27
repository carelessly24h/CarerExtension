using NPOI.SS.UserModel;

namespace CarerExtension.IO.Excel;

public static class IWorkbookExtension
{
    public static void SetCellFormat(this IWorkbook workbook, ICell cell, string dataFormat)
    {
        var cellStyle = workbook.CreateCellStyle();
        var formatter = workbook.CreateDataFormat();
        var formatIndex = formatter.GetFormat(dataFormat);
        cellStyle.DataFormat = formatIndex;
        cell.CellStyle = cellStyle;
    }
}
