﻿using NPOI.SS.UserModel;

namespace CarerExtension.IO.Excel.Extensions;

/// <summary>
/// <see cref="IWorkbook"/>の拡張メソッドを提供します。
/// </summary>
public static class IWorkbookExtension
{
    /// <summary>
    /// セルの書式を設定する
    /// </summary>
    /// <param name="workbook">対象のExcelファイル</param>
    /// <param name="cell">設定するセル</param>
    /// <param name="dataFormat">組み込みのフォーマットに一致する文字列</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetCellFormat(this IWorkbook workbook, ICell cell, string dataFormat)
    {
        var cellStyle = workbook.CreateCellStyle();
        var formatter = workbook.CreateDataFormat();
        var formatIndex = formatter.GetFormat(dataFormat);
        cellStyle.DataFormat = formatIndex;
        cell.CellStyle = cellStyle;
        cell.CellStyle.GetDataFormatString();
    }
}
