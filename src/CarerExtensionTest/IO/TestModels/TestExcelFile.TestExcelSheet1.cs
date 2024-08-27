using NPOI.SS.UserModel;

namespace CarerExtensionTest.IO.TestModels;

internal class TestExcelSheet1(IWorkbook workbook) : ExcelSheetIO(workbook)
{
    #region sheet parameter
    [ExcelCell(2, 4, DataFormat = "YYYY/MM/DD")]
    public DateTime? CreateAt { get; set; }

    [ExcelCell(3, 4)]
    public string? Creator { get; set; }

    public List<TestExcelItem> Items { get; } = [];
    #endregion

    public override void Read()
    {
        Select(0);
        Store();
        ReflectionRead();
    }

    private void Store()
    {
        Span<TestExcelItem> items = [
            StoreItem(6),
            StoreItem(7),
            StoreItem(8),
            StoreItem(9),
            StoreItem(10),
        ];
        Items.AddRange(items);
    }

    private TestExcelItem StoreItem(int rowIndex)
    {
        var no = GetInt32Value(rowIndex, 0);
        var name = GetStringValue(rowIndex, 1);
        var quantity = GetInt32Value(rowIndex, 2);
        var at = GetDateTimeValue(rowIndex, 3);
        var enable = GetBooleanValue(rowIndex, 4);
        return new TestExcelItem(no, name, quantity, at, enable);
    }

    public override void Write()
    {
        Create("test_sheet1");
        Set();
        EditItems();
        ReflectionEdit();
    }

    private void Set()
    {
        CreateAt = new(2024, 6, 1);
        Creator = "TestUser";

        Span<TestExcelItem> items = [
            new(10, "Item1", 101, new(2024, 1, 1), true),
            new(20, "Item2", 102, new(2024, 1, 2), false),
            new(30, "Item3", 103, new(2024, 1, 3), true),
        ];
        Items.AddRange(items);
    }

    private void EditItems()
    {
        var rowIndex = 6;
        foreach (var item in Items)
        {
            Edit(rowIndex, 0, item.No);
            Edit(rowIndex, 1, item.Name);
            Edit(rowIndex, 2, item.Quantity);
            Edit(rowIndex, 3, item.At);
            Edit(rowIndex, 4, item.Enable);
            rowIndex++;
        }
    }

    public record class TestExcelItem(int? No, string? Name, int? Quantity, DateTime? At, bool? Enable);
}
