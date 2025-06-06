using CarerExtension.IO.Pdf;
using CarerExtension.IO.Pdf.Attributes;
using CarerExtension.IO.Pdf.Section;
using CarerExtensionTest.IO.TestModels.Resolver;
using PdfSharp.Drawing;
using PdfSharp.Fonts;

namespace CarerExtensionTest.IO.TestModels;

internal class TestPdfFile1 : PdfIO
{
    private readonly XFont font;

    public TestPdfFile1()
    {
        GlobalFontSettings.FontResolver = new PdfFontResolver();
        font = new("Arial", 12);
    }

    #region Body
    [PdfBody(1)]
    public void Body1(SectionArgs e)
    {
        using var g = e.GetGraphics();
        g.DrawString("abcdefghij", font, XBrushes.Black, 10, 20);
        // not displayable...
        g.DrawString("あいうえお", font, XBrushes.Black, 10, 40);
    }
    #endregion
}
