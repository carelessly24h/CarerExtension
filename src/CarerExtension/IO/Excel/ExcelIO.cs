using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace CarerExtension.IO.Excel;

public abstract class ExcelIO : IDisposable
{
    #region variable
    protected readonly IWorkbook workbook;
    #endregion

    #region constractor
    protected ExcelIO()
    {
        workbook = CreateXslx();
    }

    protected ExcelIO(string path)
    {
        workbook = Read(path);
    }
    #endregion

    #region method
    public void Dispose()
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
    protected virtual IWorkbook CreateXls() => new HSSFWorkbook();

    protected virtual IWorkbook CreateXslx() => new XSSFWorkbook();

    protected virtual IWorkbook Read(string path)
    {
        var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        return WorkbookFactory.Create(stream);
    }

    public virtual void Write(string path) => Write(path, workbook);

    public virtual void Write(string path, IWorkbook targetBook)
    {
        using var stream = new FileStream(path, FileMode.Create);
        targetBook.Write(stream);
    }
    #endregion
    #endregion
}
