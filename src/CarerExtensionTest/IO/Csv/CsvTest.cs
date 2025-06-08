namespace CarerExtensionTest.IO.Csv;

[TestClass]
public class CsvTest
{
    private const string RootDir = @"test\csv_test";

    [ClassInitialize]
    public static void Initialize(TestContext _)
    {
        Directory.CreateDirectory(RootDir);
    }

    [TestMethod]
    public void Read01()
    {
        var dir = $@"{RootDir}\read1";
        var readFile = $@"{dir}\read.csv";

        #region pre-process
        Directory.CreateDirectory(dir);
        File.WriteAllText(readFile, CsvContent);
        #endregion

        using var csv = TestCsvFile.Read(readFile);

        Assert.AreEqual(6, csv.Rows.Count);
        {
            var row = csv.Rows[0];
            Assert.AreEqual(1, row.IntValue);
            Assert.AreEqual(1.5, row.DoubleValue);
            Assert.AreEqual("string1", row.StringValue);
            Assert.AreEqual(new(2001, 1, 1), row.DateTimeValue);
            Assert.IsTrue(row.BoolValue);
        }
        {
            var row = csv.Rows[5];
            Assert.AreEqual(6, row.IntValue);
            Assert.AreEqual(6.5, row.DoubleValue);
            Assert.AreEqual("string6", row.StringValue);
            Assert.AreEqual(new(2006, 6, 6), row.DateTimeValue);
            Assert.IsNull(row.BoolValue);
        }
    }

    [TestMethod]
    public void Write01()
    {
        var dir = $@"{RootDir}\write1";
        var writeFile = $@"{dir}\write.csv";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        using var csv = new TestCsvFile(writeFile);
        csv.Rows.AddRange([
            new(1, 1.5, "string1", new(2001, 1, 1), false),
            new(2, 2.5, "string2", new(2002, 2, 2), true),
        ]);
        csv.Write();

        Assert.IsTrue(File.Exists(writeFile));
    }

    [TestMethod]
    public void Write02()
    {
        var dir = $@"{RootDir}\write2";
        var readFile = $@"{dir}\read.csv";
        var writeFile = $@"{dir}\write.csv";

        #region pre-process
        Directory.CreateDirectory(dir);
        File.WriteAllText(readFile, CsvContent);
        #endregion

        using var csv = TestCsvFile.Read(readFile);
        csv.Rows.AddRange([new(1, 1.5, "string1", new(2001, 1, 1), false)]);
        csv.Write(writeFile);

        Assert.IsTrue(File.Exists(readFile));
        Assert.IsTrue(File.Exists(writeFile));
    }

    private const string CsvContent = @"intItem,doubleItem,stringItem,dateItem,boolItem
1,1.5,string1,20010101000000,t
,2.5,string2,20020202000000,f
3,,string3,20030303000000,t
4,4.5,,20040404000000,f
5,5.5,string5,,t
6,6.5,string6,20060606000000,
";
}
