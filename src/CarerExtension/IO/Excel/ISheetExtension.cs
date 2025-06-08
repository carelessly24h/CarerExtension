using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace CarerExtension.IO.Excel;

/// <summary>
/// <see cref="ISheet"/>の拡張メソッドを提供します。
/// </summary>
public static class ISheetExtension
{
    /// <summary>
    /// 行インデックスと列インデックスを指定して、セルを取得します。
    /// </summary>
    /// <param name="targetSheet">セルを取得するシート。</param>
    /// <param name="rowIndex">取得するセルの行インデックス。</param>
    /// <param name="columnIndex">取得するセルの列インデックス。</param>
    /// <returns>インデックスに対応するセル。</returns>
    public static ICell GetCell(this ISheet targetSheet, int rowIndex, int columnIndex)
    {
        var row = CellUtil.GetRow(rowIndex, targetSheet);
        return CellUtil.GetCell(row, columnIndex);
    }
}
