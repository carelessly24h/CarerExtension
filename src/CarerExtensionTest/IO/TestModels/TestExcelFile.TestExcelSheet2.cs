using NPOI.SS.UserModel;

namespace CarerExtensionTest.IO.TestModels;

internal class TestExcelSheet2(IWorkbook workbook) : ExcelSheetIO(workbook)
{
    #region sheet values
    [ExcelArea]
    public TestExcelCreatorArea Area { get; set; } = new();

    [ExcelAreaList(ListRowSize = 1)]
    public List<TestExcelListItem> Items { get; set; } = [];

    [ExcelAreaListCount]
    public int ItemCount => Worksheet.LastRowNum;
    #endregion

    public override void Read()
    {
        SelectSheet(1);
        ReadCells();
    }

    public override void Write()
    {
        CreateSheet("test_sheet2");
        SelectSheet("test_sheet2");
        SetValues();
        WriteCells();
    }

    private void SetValues()
    {
        Area.CreateAt = new(2024, 1, 1);
        Area.Creator = "TestUser";

        Items = [
            new(10, "Item1", 101, 10.1, new(2024, 1, 2), true),
            new(20, "Item2", 102, 10.2, new(2024, 1, 3), false),
            new(30, "Item3", 103, 10.3, new(2024, 1, 4), true),
        ];
    }

    public class TestExcelCreatorArea
    {
        [ExcelCell(2, 5, DataFormat = "YYYY/MM/DD")]
        public DateTime? CreateAt { get; set; }

        [ExcelCell(3, 5)]
        public string? Creator { get; set; }
    }

    public class TestExcelListItem
    {
        [ExcelCell(6, 0)]
        public int? No { get; set; }

        [ExcelCell(6, 1)]
        public string? Name { get; set; }

        [ExcelCell(6, 2)]
        public int? Quantity { get; set; }

        [ExcelCell(6, 3)]
        public double? Average { get; set; }

        [ExcelCell(6, 4, DataFormat = "YYYY/MM/DD")]
        public DateTime? At { get; set; }

        [ExcelCell(6, 5)]
        public bool? Enable { get; set; }

        public TestExcelListItem() { }

        public TestExcelListItem(int? no, string? name, int? quantity, double? average, DateTime? at, bool? enable)
        {
            No = no;
            Name = name;
            Quantity = quantity;
            Average = average;
            At = at;
            Enable = enable;
        }
    }
}
