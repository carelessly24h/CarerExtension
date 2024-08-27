namespace CarerExtensionTest.IO.TestModels;

internal class TestCsvFile(string path) : CsvIO<TestCsvRow>(path), IDisposable
{
    public List<TestCsvRow> Rows { get; } = [];

    public static TestCsvFile Read(string path)
    {
        var csv = new TestCsvFile(path);
        csv.Rows.AddRange(csv.Read());
        return csv;
    }

    public void Write() => Write(Rows);

    public void Write(string path) => Write(path, Rows);
}
