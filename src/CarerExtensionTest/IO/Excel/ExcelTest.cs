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
        #region pre-process
        using var excel = TestExcelFile.Read(TEST_FILE);
        var tester = new PrivateMemberTester(excel);
        #endregion

        var sheets = tester.Invoke<IEnumerable<ExcelSheetIO>>("Sheets");
        Assert.AreEqual(3, sheets.Count());

        // sheet1 testing.
        var sheet = (TestExcelSheet1)sheets.ElementAt(0);
        Assert.AreEqual(new(2024, 1, 1), sheet.CreateAt);
        Assert.AreEqual("TestUser11", sheet.Creator);
    }

    [TestMethod]
    public void Read02()
    {
        #region pre-process
        using var excel = TestExcelFile.Read(TEST_FILE);
        var tester = new PrivateMemberTester(excel);
        #endregion

        var sheets = tester.Invoke<IEnumerable<ExcelSheetIO>>("Sheets");
        Assert.AreEqual(3, sheets.Count());

        // sheet2 testing.
        var sheet = (TestExcelSheet2)sheets.ElementAt(1);
        Assert.AreEqual(new(2024, 2, 2), sheet.Area.CreateAt);
        Assert.AreEqual("TestUser21", sheet.Area.Creator);

        {
            var item = sheet.Items.ElementAt(0);
            Assert.AreEqual(21, item.No);
            Assert.AreEqual("Item21", item.Name);
            Assert.AreEqual(201, item.Quantity);
            Assert.AreEqual(20.1, item.Average);
            Assert.AreEqual(new(2024, 2, 11), item.At);
            Assert.IsTrue(item.Enable);
        }
        {
            var item = sheet.Items.ElementAt(2);
            Assert.AreEqual(23, item.No);
            Assert.AreEqual("Item23", item.Name);
            Assert.AreEqual(203, item.Quantity);
            Assert.AreEqual(20.3, item.Average);
            Assert.AreEqual(new(2024, 2, 13), item.At);
            Assert.IsTrue(item.Enable);
        }
    }

    [TestMethod]
    public void Read03()
    {
        #region pre-process
        using var excel = TestExcelFile.Read(TEST_FILE);
        var tester = new PrivateMemberTester(excel);
        #endregion

        var sheets = tester.Invoke<IEnumerable<ExcelSheetIO>>("Sheets");
        Assert.AreEqual(3, sheets.Count());

        // sheet3 testing.
        var sheet = (TestExcelSheet3)sheets.ElementAt(2);
        {
            var area = sheet.Area1;
            Assert.AreEqual(new(2024, 3, 3), area.CreateAt);
            Assert.AreEqual("TestUser31", area.Creator);

            {
                var item = area.Items.ElementAt(0);
                Assert.AreEqual(31, item.No);
                Assert.AreEqual("Item31", item.Name);
                Assert.AreEqual(301, item.Quantity);
                Assert.AreEqual(3001D, item.Average);
                Assert.AreEqual(new(2024, 3, 11), item.At);
                Assert.IsTrue(item.Enable);
            }
            {
                var item = area.Items.ElementAt(2);
                Assert.AreEqual(33, item.No);
                Assert.AreEqual("Item33", item.Name);
                Assert.AreEqual(303, item.Quantity);
                Assert.AreEqual(3003D, item.Average);
                Assert.AreEqual(new(2024, 3, 13), item.At);
                Assert.IsTrue(item.Enable);
            }
        }
        {
            var area = sheet.Area2;
            Assert.AreEqual(new(2024, 3, 4), area.CreateAt);
            Assert.AreEqual("TestUser32", area.Creator);

            {
                var item = area.Items.ElementAt(0);
                Assert.AreEqual(34, item.No);
                Assert.AreEqual("Item34", item.Name);
                Assert.AreEqual(304, item.Quantity);
                Assert.AreEqual(30.4, item.Average);
                Assert.AreEqual(new(2024, 3, 14), item.At);
                Assert.IsFalse(item.Enable);
            }
            {
                var item = area.Items.ElementAt(2);
                Assert.AreEqual(36, item.No);
                Assert.AreEqual("Item36", item.Name);
                Assert.AreEqual(306, item.Quantity);
                Assert.AreEqual(30.6, item.Average);
                Assert.AreEqual(new(2024, 3, 16), item.At);
                Assert.IsFalse(item.Enable);
            }
        }
    }

    [TestMethod]
    public void Write01()
    {
        var dir = $@"{ROOT_DIR}\write1";
        var writeFile = $@"{dir}\write.xlsx";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        using var excel = new TestExcelFile();
        excel.Write(writeFile);

        // sheet1 testing.
        using var book = ExcelTestUtil.OpenWorkbook(writeFile);
        var sheet = book.GetSheet("test_sheet1");

        Assert.AreEqual(new DateTime(2024, 1, 1), sheet.GetCell(2, 5).DateCellValue);
        Assert.AreEqual("TestUser", sheet.GetCell(3, 5).StringCellValue);
    }

    [TestMethod]
    public void Write02()
    {
        var dir = $@"{ROOT_DIR}\write2";
        var writeFile = $@"{dir}\write.xlsx";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        using var excel = new TestExcelFile();
        excel.Write(writeFile);

        // sheet2 testing.
        using var book = ExcelTestUtil.OpenWorkbook(writeFile);
        var sheet = book.GetSheet("test_sheet2");

        Assert.AreEqual(new DateTime(2024, 1, 1), sheet.GetCell(2, 5).DateCellValue);
        Assert.AreEqual("TestUser", sheet.GetCell(3, 5).StringCellValue);

        {
            var row = sheet.GetRow(6);
            Assert.AreEqual(10, row.GetCell(0).NumericCellValue);
            Assert.AreEqual("Item1", row.GetCell(1).StringCellValue);
            Assert.AreEqual(101, row.GetCell(2).NumericCellValue);
            Assert.AreEqual(10.1, row.GetCell(3).NumericCellValue);
            Assert.AreEqual(new DateTime(2024, 1, 2), row.GetCell(4).DateCellValue);
            Assert.IsTrue(row.GetCell(5).BooleanCellValue);
        }
        {
            var row = sheet.GetRow(8);
            Assert.AreEqual(30, row.GetCell(0).NumericCellValue);
            Assert.AreEqual("Item3", row.GetCell(1).StringCellValue);
            Assert.AreEqual(103, row.GetCell(2).NumericCellValue);
            Assert.AreEqual(10.3, row.GetCell(3).NumericCellValue);
            Assert.AreEqual(new DateTime(2024, 1, 4), row.GetCell(4).DateCellValue);
            Assert.IsTrue(row.GetCell(5).BooleanCellValue);
        }
    }

    [TestMethod]
    public void Write03()
    {
        var dir = $@"{ROOT_DIR}\write3";
        var writeFile = $@"{dir}\write.xlsx";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        using var excel = new TestExcelFile();
        excel.Write(writeFile);

        // shee3 testing.
        using var book = ExcelTestUtil.OpenWorkbook(writeFile);
        var sheet = book.GetSheet("test_sheet3");

        Assert.AreEqual(new(2024, 1, 1), sheet.GetCell(2, 5).DateCellValue);
        Assert.AreEqual("TestUser", sheet.GetCell(3, 5).StringCellValue);

        {
            var row = sheet.GetRow(6);
            Assert.AreEqual(10, row.GetCell(0).NumericCellValue);
            Assert.AreEqual("Item11", row.GetCell(1).StringCellValue);
            Assert.AreEqual(101, row.GetCell(2).NumericCellValue);
            Assert.AreEqual(10.1, row.GetCell(3).NumericCellValue);
            Assert.AreEqual(new(2024, 1, 2), row.GetCell(4).DateCellValue);
            Assert.IsTrue(row.GetCell(5).BooleanCellValue);
        }

        Assert.AreEqual(new(2024, 2, 1), sheet.GetCell(10, 5).DateCellValue);
        Assert.AreEqual("TestUser2", sheet.GetCell(11, 5).StringCellValue);

        {
            var row = sheet.GetRow(16);
            Assert.AreEqual(31, row.GetCell(0).NumericCellValue);
            Assert.AreEqual("Item23", row.GetCell(1).StringCellValue);
            Assert.AreEqual(113, row.GetCell(2).NumericCellValue);
            Assert.AreEqual(11.3, row.GetCell(3).NumericCellValue);
            Assert.AreEqual(new(2024, 2, 4), row.GetCell(4).DateCellValue);
            Assert.IsFalse(row.GetCell(5).BooleanCellValue);
        }
    }
}
