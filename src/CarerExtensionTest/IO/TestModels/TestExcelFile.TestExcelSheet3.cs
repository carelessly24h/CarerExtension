using NPOI.SS.UserModel;

namespace CarerExtensionTest.IO.TestModels;

internal class TestExcelSheet3(IWorkbook workbook) : ExcelSheetIO(workbook)
{
    #region sheet values
    [ExcelArea(2, 0)]
    public TestExcelArea Area1 { get; set; } = null!;

    [ExcelArea(10, 0)]
    public TestExcelArea Area2 { get; set; } = null!;
    #endregion

    public override void Read()
    {
        SelectSheet("TestSheet3");
        ReadCells();
    }

    public override void Write()
    {
        CreateSheet("test_sheet3");
        SelectSheet("test_sheet3");
        SetValues();
        WriteCells();
    }

    private void SetValues()
    {
        Area1 = new()
        {
            CreateAt = new(2024, 1, 1),
            Creator = "TestUser",

            Items = [
                new(10, "Item11", 101, 10.1, new(2024, 1, 2), true),
                new(20, "Item12", 102, 10.2, new(2024, 1, 3), false),
                new(30, "Item13", 103, 10.3, new(2024, 1, 4), true),
            ]
        };

        Area2 = new()
        {
            CreateAt = new(2024, 2, 1),
            Creator = "TestUser2",

            Items = [
                new(11, "Item21", 111, 11.1, new(2024, 2, 2), false),
                new(21, "Item22", 112, 11.2, new(2024, 2, 3), true),
                new(31, "Item23", 113, 11.3, new(2024, 2, 4), false),
            ]
        };
    }

    public class TestExcelArea
    {
        [ExcelCell(0, 5, DataFormat = "YYYY/MM/DD")]
        public DateTime? CreateAt { get; set; }

        [ExcelCell(1, 5)]
        public string? Creator { get; set; }

        [ExcelAreaListCount(3)]
        [ExcelAreaList(4, 0, ListRowSize = 1)]
        public List<TestExcelListItem> Items { get; set; } = null!;
    }

    public class TestExcelListItem
    {
        [ExcelCell(0, 0)]
        public int? No { get; set; }

        [ExcelCell(0, 1)]
        public string? Name { get; set; }

        [ExcelCell(0, 2)]
        public int? Quantity { get; set; }

        [ExcelCell(0, 3)]
        public double? Average { get; set; }

        [ExcelCell(0, 4, DataFormat = "YYYY/MM/DD")]
        public DateTime? At { get; set; }

        [ExcelCell(0, 5)]
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
