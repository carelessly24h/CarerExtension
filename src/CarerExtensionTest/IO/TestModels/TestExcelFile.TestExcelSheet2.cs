using NPOI.SS.UserModel;

namespace CarerExtensionTest.IO.TestModels;

internal class TestExcelSheet2(IWorkbook workbook) : ExcelSheetIO(workbook)
{
    #region sheet paramter
    [ExcelCell(2, 4, DataFormat = "YYYY/MM/DD")]
    public DateTime? CreateAt { get; set; }

    [ExcelCell(3, 4)]
    public string? Creator { get; set; }

    public List<MeasurePoint> MeasurePoints { get; set; } = [];

    [ExcelCell(11, 1)]
    public string? PointId { get; set; }

    [ExcelCell(12, 1)]
    public string? PointName { get; set; }

    [ExcelCell(13, 1)]
    public string? PointAddress { get; set; }

    [ExcelCell(14, 1, DataFormat = "#.00")]
    public double? MinValue { get; set; }

    [ExcelCell(15, 1, DataFormat = "#.00")]
    public double? MaxValue { get; set; }
    #endregion

    public override void Read()
    {
        Select("TestSheet2");
        Store();
        ReflectionRead();
    }

    private void Store()
    {
        Span<MeasurePoint> measurePoints = [
            StoreMeasurePoint(6),
            StoreMeasurePoint(7),
            StoreMeasurePoint(8),
        ];
        MeasurePoints.AddRange(measurePoints);
    }

    private MeasurePoint StoreMeasurePoint(int rowIndex)
    {
        var name = GetStringValue(rowIndex, 0);
        var valueA = GetInt32Value(rowIndex, 1);
        var valueB = GetInt32Value(rowIndex, 2);
        var average = GetDoubleValue(rowIndex, 3);
        return new(name, valueA, valueB, average);
    }

    public override void Write()
    {
        Create("test_sheet2");
        Set();
        EditPoints();
        ReflectionEdit();
    }

    private void Set()
    {
        CreateAt = new(2024, 12, 1);
        Creator = "TestUser";

        Span<MeasurePoint> items = [
            new("Item1", 10, 100, 10.5),
            new("Item2", 20, 200, 20.5),
            new("Item3", 30, 300, 30.5),
        ];
        MeasurePoints.AddRange(items);

        PointId = "Po001";
        PointName = "TestPoint1";
        PointAddress = "Tower No.3 B room";
        MinValue = 5.0;
        MaxValue = 10.5;
    }

    private void EditPoints()
    {
        var rowIndex = 6;
        foreach (var point in MeasurePoints)
        {
            Edit(rowIndex, 0, point.Name);
            Edit(rowIndex, 1, point.ValueA);
            Edit(rowIndex, 2, point.ValueB);
            Edit(rowIndex, 3, point.Average);
            rowIndex++;
        }
    }

    public record class MeasurePoint(string? Name, int? ValueA, int? ValueB, double? Average);
}
