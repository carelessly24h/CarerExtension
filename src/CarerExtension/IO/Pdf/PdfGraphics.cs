using CarerExtension.IO.Pdf.Section;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace CarerExtension.IO.Pdf;

public class PdfGraphics(XGraphics graphics, PdfSection section) : IDisposable
{
    #region constructor
    public PdfGraphics(PdfPage page, PdfSection section) : this(XGraphics.FromPdfPage(page), section)
    {
    }
    #endregion

    #region methods
    public void Dispose()
    {
        try
        {
            graphics.Dispose();
        }
        finally
        {
            GC.SuppressFinalize(this);
        }
    }

    #region DrawImage
    public void DrawImage(XImage image, XPoint point)
    {
        var p = CalculatePoint(point);
        graphics.DrawImage(image, p);
    }

    public void DrawImage(XImage image, double x, double y)
    {
        var (left, top) = CalculateLocation(x, y);
        graphics.DrawImage(image, left, top);
    }

    public void DrawImage(XImage image, double x, double y, double width, double height)
    {
        var (left, top) = CalculateLocation(x, y);
        graphics.DrawImage(image, left, top, width, height);
    }

    public void DrawImage(XImage image, XRect rect)
    {
        var r = CalculateRect(rect);
        graphics.DrawImage(image, r);
    }

    public void DrawImage(XImage image, XRect destRect, XRect srcRect, XGraphicsUnit srcUnit)
    {
        var d = CalculateRect(destRect);
        var s = CalculateRect(srcRect);
        graphics.DrawImage(image, d, s, srcUnit);
    }
    #endregion

    #region DrawLine
    public void DrawLine(XPen pen, double x1, double y1, double x2, double y2)
    {
        var (left1, top1) = CalculateLocation(x1, y1);
        var (left2, top2) = CalculateLocation(x2, y2);
        graphics.DrawLine(pen, left1, top1, left2, top2);
    }
    #endregion

    #region DrawRectangle
    public void DrawRectangle(XPen pen, double x, double y, double width, double height)
    {
        var (left, top) = CalculateLocation(x, y);
        graphics.DrawRectangle(pen, left, top, width, height);
    }

    public void DrawRectangle(XPen pen, XRect rect)
    {
        var r = CalculateRect(rect);
        graphics.DrawRectangle(pen, r);
    }

    public void DrawRectangle(XBrush brush, double x, double y, double width, double height)
    {
        var (left, top) = CalculateLocation(x, y);
        graphics.DrawRectangle(brush, left, top, width, height);
    }

    public void DrawRectangle(XBrush brush, XRect rect)
    {
        var r = CalculateRect(rect);
        graphics.DrawRectangle(brush, r);
    }

    public void DrawRectangle(XPen? pen, XBrush? brush, double x, double y, double width, double height)
    {
        var (left, top) = CalculateLocation(x, y);
        graphics.DrawRectangle(pen, brush, left, top, width, height);
    }

    public void DrawRectangle(XPen? pen, XBrush? brush, XRect rect)
    {
        var r = CalculateRect(rect);
        graphics.DrawRectangle(pen, brush, r);
    }
    #endregion

    #region DrawRoundedRectangle
    public void DrawRoundedRectangle(XPen pen, double x, double y, double width, double height, double ellipseWidth, double ellipseHeight)
    {
        var (left, top) = CalculateLocation(x, y);
        graphics.DrawRoundedRectangle(pen, left, top, width, height, ellipseWidth, ellipseHeight);
    }

    public void DrawRoundedRectangle(XPen pen, XRect rect, XSize ellipseSize)
    {
        var r = CalculateRect(rect);
        graphics.DrawRoundedRectangle(pen, r, ellipseSize);
    }

    public void DrawRoundedRectangle(XBrush brush, double x, double y, double width, double height, double ellipseWidth, double ellipseHeight)
    {
        var (left, top) = CalculateLocation(x, y);
        graphics.DrawRoundedRectangle(brush, left, top, width, height, ellipseWidth, ellipseHeight);
    }

    public void DrawRoundedRectangle(XBrush brush, XRect rect, XSize ellipseSize)
    {
        var r = CalculateRect(rect);
        graphics.DrawRoundedRectangle(brush, r, ellipseSize);
    }

    public void DrawRoundedRectangle(XPen? pen, XBrush? brush, double x, double y, double width, double height, double ellipseWidth, double ellipseHeight)
    {
        var (left, top) = CalculateLocation(x, y);
        graphics.DrawRoundedRectangle(pen, brush, left, top, width, height, ellipseWidth, ellipseHeight);
    }

    public void DrawRoundedRectangle(XPen? pen, XBrush? brush, XRect rect, XSize ellipseSize)
    {
        var r = CalculateRect(rect);
        graphics.DrawRoundedRectangle(pen, brush, r, ellipseSize);
    }
    #endregion

    #region DrawString
    public void DrawString(string s, XFont font, XBrush brush, XPoint point)
    {
        var p = CalculatePoint(point);
        graphics.DrawString(s, font, brush, p);
    }

    public void DrawString(string s, XFont font, XBrush brush, XPoint point, XStringFormat format)
    {
        var p = CalculatePoint(point);
        graphics.DrawString(s, font, brush, p, format);
    }

    public void DrawString(string text, XFont font, XBrush brush, double x, double y)
    {
        var (left, top) = CalculateLocation(x, y);
        graphics.DrawString(text, font, brush, left, top);
    }

    public void DrawString(string s, XFont font, XBrush brush, double x, double y, XStringFormat format)
    {
        var (left, top) = CalculateLocation(x, y);
        graphics.DrawString(s, font, brush, left, top, format);
    }

    public void DrawString(string s, XFont font, XBrush brush, XRect layoutRectangle)
    {
        var r = CalculateRect(layoutRectangle);
        graphics.DrawString(s, font, brush, r);
    }

    public void DrawString(string text, XFont font, XBrush brush, XRect layoutRectangle, XStringFormat format)
    {
        var r = CalculateRect(layoutRectangle);
        graphics.DrawString(text, font, brush, r, format);
    }
    #endregion

    #region Calculate
    private (double left, double top) CalculateLocation(double x, double y) => (section.Left + x, section.Top + y);

    private XPoint CalculatePoint(XPoint point) => new(section.Left + point.X, section.Top + point.Y);

    private XRect CalculateRect(XRect rect) => new(section.Left + rect.X, section.Top + rect.Y, rect.Width, rect.Height);
    #endregion
    #endregion
}
