using CsvHelper.Configuration.Attributes;

namespace CarerExtensionTest.IO.TestModels;

internal class TestCsvRow
{
    [Index(0)]
    [Name("intItem")]
    public int? IntValue { get; set; }

    [Index(1)]
    [Name("doubleItem")]
    public double? DoubleValue { get; set; }

    [Index(2)]
    [Name("stringItem")]
    public string? StringValue { get; set; }

    [Index(3)]
    [Name("dateItem")]
    [Format("yyyyMMddHHmmss")]
    public DateTime? DateTimeValue { get; set; }

    [Index(4)]
    [Name("boolItem")]
    [BooleanTrueValues("t")]
    [BooleanFalseValues("f")]
    public bool? BoolValue { get; set; }

    public TestCsvRow()
    { }

    public TestCsvRow(int? intValue, double? doubleValue, string? stringValue, DateTime? datetimeValue, bool? boolValue)
    {
        IntValue = intValue;
        DoubleValue = doubleValue;
        StringValue = stringValue;
        DateTimeValue = datetimeValue;
        BoolValue = boolValue;
    }
}
