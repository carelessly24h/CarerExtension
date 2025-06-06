using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace CarerExtension.IO.Excel;

/// <summary>
/// Excelファイルの入出力を行うクラス
/// </summary>
public abstract class ExcelIO : IDisposable
{
    #region variable
    /// <summary>
    /// ワークブック
    /// </summary>
    protected readonly IWorkbook workbook;
    #endregion

    #region constractor
    /// <summary>
    /// XLSXファイルを作成する
    /// </summary>
    protected ExcelIO()
    {
        workbook = CreateXslx();
    }

    /// <summary>
    /// Excelファイルを読み込む
    /// </summary>
    /// <param name="path">ファイルパス</param>
    protected ExcelIO(string path)
    {
        workbook = Read(path);
    }
    #endregion

    #region method
    /// <summary>
    /// 解放処理
    /// </summary>
    public virtual void Dispose()
    {
        try
        {
            workbook.Close();
        }
        finally
        {
            GC.SuppressFinalize(this);
        }
    }

    #region book-operation
    /// <summary>
    /// XLSファイルを作成する
    /// </summary>
    /// <returns>Excelファイル</returns>
    protected virtual IWorkbook CreateXls() => new HSSFWorkbook();

    /// <summary>
    /// XLSXファイルを作成する
    /// </summary>
    /// <returns>Excelファイル</returns>
    protected virtual IWorkbook CreateXslx() => new XSSFWorkbook();

    /// <summary>
    /// ファイルを読み込む
    /// </summary>
    /// <param name="path">読み込みファイルパス</param>
    /// <returns>読み込んだExcelファイル</returns>
    protected virtual IWorkbook Read(string path)
    {
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        return WorkbookFactory.Create(stream);
    }

    /// <summary>
    /// ファイルを書き込む
    /// </summary>
    /// <param name="path">出力先ファイルパス</param>
    public virtual void Write(string path) => Write(path, workbook);

    /// <summary>
    /// ファイルを書き込む
    /// </summary>
    /// <param name="path">出力先ファイルパス</param>
    /// <param name="targetBook">出力先のブックファイル</param>
    protected virtual void Write(string path, IWorkbook targetBook)
    {
        using var stream = new FileStream(path, FileMode.Create);
        targetBook.Write(stream);
    }
    #endregion
    #endregion
}
