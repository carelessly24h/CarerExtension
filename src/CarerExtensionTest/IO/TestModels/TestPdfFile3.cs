using CarerExtension.IO.Pdf;
using CarerExtension.IO.Pdf.Attributes;
using CarerExtension.IO.Pdf.Section;
using CarerExtensionTest.IO.TestModels.Resolver;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Fonts;

namespace CarerExtensionTest.IO.TestModels;

internal class TestPdfFile3 : PdfIO
{
    protected override PageSize PageSize => PageSize.A4;

    protected override Margin Margin => new(40, 40, 40, 40);

    protected override double HeaderHeight => 80;

    protected override double FooterHeight => 80;

    private readonly XFont font;

    public TestPdfFile3()
    {
        GlobalFontSettings.FontResolver = new PdfFontResolver();
        font = new("GenShin Gothic", 12);
    }

    public TestPdfFile3(string filePath) : base(filePath)
    {
        GlobalFontSettings.FontResolver = new PdfFontResolver();
        font = new("GenShin Gothic", 12);
    }

    #region Header/Footer
    [PdfHeader]
    public void Header(SectionArgs e)
    {
        using var g = e.GetGraphics();
        var (left, top) = e.Section.Center;
        g.DrawString("# Title: SAMPLE PDF DOCUMENT #", font, XBrushes.Black, left, top, XStringFormats.Center);
    }

    [PdfHeader]
    public static void HeaderRect(SectionArgs e)
    {
        using var g = e.GetGraphics();
        g.DrawRoundedRectangle(XPens.Red, 0, 0, e.Section.Width, e.Section.Height, 10, 10);
    }

    [PdfFooter]
    public void Footer(SectionArgs e)
    {
        using var g = e.GetGraphics();
        var (left, top) = e.Section.Center;
        g.DrawString($"- {e.PageNumber} / {e.TotalPageNumber} -", font, XBrushes.Black, left, top, XStringFormats.Center);
    }

    [PdfFooter]
    public static void FooterRect(SectionArgs e)
    {
        using var g = e.GetGraphics();
        g.DrawRoundedRectangle(XPens.Blue, 0, 0, e.Section.Width, e.Section.Height, 10, 10);
    }
    #endregion

    #region Body
    [PdfBody(1)]
    public void Body1(SectionArgs e)
    {
        using var g = e.GetGraphics();
        g.DrawString("abcdefghij", font, XBrushes.Black, 10, 20);
        g.DrawString("あいうえお", font, XBrushes.Black, 10, 40);

        g.DrawRoundedRectangle(XPens.Green, 0, 0, e.Section.Width, e.Section.Height, 10, 10);
    }

    [PdfBody(2)]
    public void Body2(SectionArgs e)
    {
        using var g = e.GetGraphics();
        g.DrawString("aaaaabbbbb", font, XBrushes.Black, 10, 20);
        g.DrawString("かきくけこ", font, XBrushes.Black, new XRect(10, 40, 20, 100), XStringFormats.BottomLeft);
    }

    [PdfBody(3)]
    public static void BlankPage(SectionArgs _)
    {
    }

    [PdfBody(1)]
    [PdfBody(2)]
    [PdfBody(3)]
    public static void BodyRect(SectionArgs e)
    {
        using var g = e.GetGraphics();
        g.DrawRoundedRectangle(XPens.Green, 0, 0, e.Section.Width, e.Section.Height, 10, 10);
    }
    #endregion
}
