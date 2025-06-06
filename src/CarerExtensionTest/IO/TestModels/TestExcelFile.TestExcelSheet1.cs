using NPOI.SS.UserModel;

namespace CarerExtensionTest.IO.TestModels;

internal class TestExcelSheet1(IWorkbook workbook) : ExcelSheetIO(workbook)
{
    #region sheet values
    [ExcelCell(2, 5, DataFormat = "YYYY/MM/DD")]
    public DateTime? CreateAt { get; set; }

    [ExcelCell(3, 5)]
    public string? Creator { get; set; }
    #endregion

    public override void Read()
    {
        SelectSheet(0);
        ReadCells();
    }

    public override void Write()
    {
        CreateSheet("test_sheet1");
        SelectSheet("test_sheet1");
        SetValues();
        WriteCells();
    }

    private void SetValues()
    {
        CreateAt = new(2024, 1, 1);
        Creator = "TestUser";
    }
}
