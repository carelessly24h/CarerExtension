using PdfSharp;

namespace CarerExtensionTest.IO.Pdf;

[TestClass]
public class PdfTest
{
    private const string TestFile = @"IO\TestFiles\TestPdfFile.pdf";
    private const string RootDir = @"test\pdf_test";

    [ClassInitialize]
    public static void Initialize(TestContext _)
    {
        Directory.CreateDirectory(RootDir);
    }

    [TestMethod]
    public void Update01()
    {
        var dir = $@"{RootDir}\read1";
        var writePath = $@"{dir}\read1.pdf";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        using var pdf = new TestPdfFile3(TestFile);
        pdf.Write(writePath);

        Assert.IsTrue(File.Exists(writePath));
    }

    [TestMethod]
    public void Write01()
    {
        var dir = $@"{RootDir}\write1";
        var pdfPath = $@"{dir}\test1.pdf";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        using var pdf = new TestPdfFile1();
        pdf.Write(pdfPath);

        Assert.IsTrue(File.Exists(pdfPath));
    }

    [TestMethod]
    public void Write02()
    {
        var dir = $@"{RootDir}\write2";
        var pdfPath = $@"{dir}\test2.pdf";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        using var pdf = new TestPdfFile2();
        pdf.Write(pdfPath);

        Assert.IsTrue(File.Exists(pdfPath));
    }

    [TestMethod]
    public void Write03()
    {
        var dir = $@"{RootDir}\write3";
        var pdfPath = $@"{dir}\test3.pdf";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        using var pdf = new TestPdfFile3();
        pdf.Write(pdfPath);

        Assert.IsTrue(File.Exists(pdfPath));
    }
}
