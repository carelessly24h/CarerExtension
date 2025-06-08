using NPOI.SS.UserModel;

namespace CarerExtension.IO.Excel.Extensions;

/// <summary>
/// ICellの拡張メソッド
/// </summary>
public static class ICellExtension
{
    /// <summary>
    /// セルの値を設定する
    /// </summary>
    /// <param name="cell">設定先のセル</param>
    /// <param name="value">設定する値</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
