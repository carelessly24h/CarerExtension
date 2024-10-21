namespace CarerExtensionTest.IO.Excel;

[TestClass]
public class ExcelTest
{
    private const string TEST_FILE = @"IO\TestFiles\TestExcelFile.xlsx";
    private const string ROOT_DIR = @"test\excel_test";

    [ClassInitialize]
    public static void Initialize(TestContext _)
    {
        Directory.CreateDirectory(ROOT_DIR);
    }

    [TestMethod]
    public void Read01()
    {
        using var excel = TestExcelFile.Read(TEST_FILE);
        var tester = new PrivateMemberTester(excel);

        var sheets = tester.Invoke<IEnumerable<ExcelSheetIO>>("Sheets");
        Assert.AreEqual(2, sheets.Count());

        // sheet1 testing.
        {
            var sheet = (TestExcelSheet1)sheets.ElementAt(0);
            Assert.AreEqual(new(2024, 6, 10), sheet.CreateAt);
            Assert.AreEqual("TestUser1", sheet.Creator);

            {
                var item = sheet.Items.ElementAt(0);
                Assert.AreEqual(1, item.No);
                Assert.AreEqual("Item1", item.Name);
                Assert.AreEqual(5, item.Quantity);
                Assert.AreEqual(new(2024, 1, 1), item.At);
                Assert.IsTrue(item.Enable);
            }
            {
                var item = sheet.Items.ElementAt(4);
                Assert.AreEqual(5, item.No);
                Assert.AreEqual("Item5", item.Name);
                Assert.AreEqual(9, item.Quantity);
                Assert.AreEqual(new(2024, 5, 1), item.At);
                Assert.IsTrue(item.Enable);
            }
        }
    }

    [TestMethod]
    public void Read02()
    {
        using var excel = TestExcelFile.Read(TEST_FILE);
        var tester = new PrivateMemberTester(excel);

        var sheets = tester.Invoke<IEnumerable<ExcelSheetIO>>("Sheets");
        Assert.AreEqual(2, sheets.Count());

        // sheet2 testing.
        {
            var sheet = (TestExcelSheet2)sheets.ElementAt(1);
            Assert.AreEqual(new(2024, 6, 10), sheet.CreateAt);
            Assert.AreEqual("TestUser2", sheet.Creator);

            {
                var point = sheet.MeasurePoints.ElementAt(0);
                Assert.AreEqual("Point1", point.Name);
                Assert.AreEqual(1000, point.ValueA);
                Assert.AreEqual(1001, point.ValueB);
                Assert.AreEqual(1000.5, point.Average);
            }
            {
                var point = sheet.MeasurePoints.ElementAt(2);
                Assert.AreEqual("Point3", point.Name);
                Assert.AreEqual(4000, point.ValueA);
                Assert.AreEqual(4001, point.ValueB);
                Assert.AreEqual(4000.5, point.Average);
            }

            Assert.AreEqual("P03", sheet.PointId);
            Assert.AreEqual("Point3", sheet.PointName);
            Assert.AreEqual("Tower No.3 A Room", sheet.PointAddress);
            Assert.AreEqual(1005.1, sheet.MinValue);
            Assert.AreEqual(1050.5, sheet.MaxValue);
        }
    }

    [TestMethod]
    public void Update01()
    {
        var dir = $@"{ROOT_DIR}\update1";
        var writeFile = $@"{dir}\update.csv";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        using var excel = TestExcelFile.Read(TEST_FILE);
        excel.Update(writeFile);

        Assert.IsTrue(File.Exists(writeFile));
    }

    [TestMethod]
    public void Write01()
    {
        var dir = $@"{ROOT_DIR}\write1";
        var writeFile = $@"{dir}\write.csv";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        using var excel = new TestExcelFile();
        excel.Write(writeFile);

        Assert.IsTrue(File.Exists(writeFile));
    }
}
