namespace CarerExtensionTest.IO.TestModels;

internal class TestExcelFile : ExcelIO, IDisposable
{
    private readonly string? filePath;
    public string FilePath => filePath ?? throw new InvalidOperationException("File destination is not specified.");

    private List<ExcelSheetIO> Sheets { get; } = [];

    public TestExcelFile() : base()
    {
        Initialize();
    }

    public TestExcelFile(string path) : base(path)
    {
        filePath = path;
        Initialize();
        ReadSheets();
    }

    private void Initialize()
    {
        Sheets.Add(new TestExcelSheet1(workbook));
        Sheets.Add(new TestExcelSheet2(workbook));
    }

    public static new TestExcelFile Read(string path) => new(path);

    private void ReadSheets()
    {
        foreach (var sheet in Sheets)
        {
            sheet.Read();
        }
    }

    public void Update(string path) => base.Write(path);

    public override void Write(string path)
    {
        WriteSheets();
        base.Write(path);
    }

    private void WriteSheets()
    {
        foreach (var sheet in Sheets)
        {
            sheet.Write();
        }
    }
}
