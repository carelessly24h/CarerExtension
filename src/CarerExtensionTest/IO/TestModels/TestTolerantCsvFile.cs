namespace CarerExtensionTest.IO.TestModels;

internal class TestTolerantCsvFile(string path) : TolerantCsvIO<TestCsvRow>(path), IDisposable
{
    public List<TestCsvRow> Rows { get; } = [];

    public static TestTolerantCsvFile Create(string path) => new(path);

    public static TestTolerantCsvFile Read(string path)
    {
        var csv = new TestTolerantCsvFile(path);
        csv.Rows.AddRange(csv.Read());
        return csv;
    }

    public void Write() => Write(Rows);

    public void Write(string path) => Write(path, Rows);
}
