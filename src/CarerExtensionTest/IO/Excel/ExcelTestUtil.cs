using NPOI.SS.UserModel;

namespace CarerExtensionTest.IO.Excel;

internal class ExcelTestUtil
{
    public static IWorkbook OpenWorkbook(string path)
    {
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        return WorkbookFactory.Create(stream);
    }
}
