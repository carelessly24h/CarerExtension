using NPOI.SS.UserModel;

namespace CarerExtension.IO.Excel;

public static class ICellExtension
{
    public static void SetCellValue(this ICell cell, object? value)
    {
        if (value != null)
        {
            cell.SetCellValue((dynamic)value);
        }
        else
        {
            cell.SetBlank();
        }
    }
}
