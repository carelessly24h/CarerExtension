using PdfSharp.Pdf;

namespace CarerExtension.IO.Pdf.Section;

/// <summary>
/// PDFのセクションの領域を表す構造体です。
/// </summary>
/// <remarks>
/// セクションとは、ヘッダー、フッター、および本文などの領域を表します。
/// </remarks>
/// <param name="Top">セクションの頂点。</param>
/// <param name="Left">セクションの左端。</param>
/// <param name="Right">セクションの右端。</param>
/// <param name="Bottom">セクションの底面。</param>
public record struct PdfSection(double Top, double Left, double Right, double Bottom)
{
    #region properties
    /// <summary>
    /// セクションの高さを取得します。
    /// </summary>
    public readonly double Height => Bottom - Top;

    /// <summary>
    /// セクションの幅を取得します。
    /// </summary>
    public readonly double Width => Right - Left;

    /// <summary>
    /// セクションの中央位置を取得します。
    /// </summary>
    public readonly (double X, double Y) Center => (Width / 2, Height / 2);
    #endregion

    #region methods
    /// <summary>
    /// ヘッダーのセクションを作成します。
    /// </summary>
    /// <param name="page">PDFのページ。</param>
    /// <param name="margin">PDFのマージン。</param>
    /// <param name="headerHeight">ヘッダの高さ。</param>
    /// <returns>指定されたパラメータでサイズを計算した、ヘッダーのセクション。</returns>
    public static PdfSection ToHeader(PdfPage page, in Margin margin, double headerHeight)
    {
        var headerWidth = CalculatePageWidth(page, margin);

        var top = margin.Top;
        var left = margin.Left;
        var right = left + headerWidth;
        var bottom = top + headerHeight;
        return new(top, left, right, bottom);
    }

    /// <summary>
    /// 本文のセクションを作成します。
    /// </summary>
    /// <param name="page">PDFのページ。</param>
    /// <param name="margin">PDFのマージン。</param>
    /// <param name="headerHeight">ヘッダの高さ。</param>
    /// <param name="footerHeight">フッターの高さ。</param>
    /// <returns>指定されたパラメータでサイズを計算した、PDF本文のセクション。</returns>
    public static PdfSection ToBody(PdfPage page, in Margin margin, double headerHeight, double footerHeight)
    {
        var bodyTop = CalculateBodyTop(margin, headerHeight);
        var bodyHeight = CalculateBodyHeight(page, margin, headerHeight, footerHeight);
        var bodyWidth = CalculatePageWidth(page, margin);

        var top = bodyTop;
        var left = margin.Left;
        var right = left + bodyWidth;
        var bottom = top + bodyHeight;
        return new(top, left, right, bottom);
    }

    /// <summary>
    /// フッターのセクションを作成します。
    /// </summary>
    /// <param name="page">PDFのページ。</param>
    /// <param name="margin">PDFのマージン。</param>
    /// <param name="headerHeight">ヘッダの高さ。</param>
    /// <param name="footerHeight">フッターの高さ。</param>
    /// <returns>指定されたパラメータでサイズを計算した、フッターのセクション。</returns>
    public static PdfSection ToFooter(PdfPage page, in Margin margin, double headerHeight, double footerHeight)
    {
        var bodyTop = CalculateBodyTop(margin, headerHeight);
        var bodyHeight = CalculateBodyHeight(page, margin, headerHeight, footerHeight);
        var footerWidth = CalculatePageWidth(page, margin);

        var top = bodyTop + bodyHeight;
        var left = margin.Left;
        var right = left + footerWidth;
        var bottom = top + footerHeight;
        return new(top, left, right, bottom);
    }

    /// <summary>
    /// 本文のセクションの上端の位置を計算します。
    /// </summary>
    /// <param name="margin">PDFのマージン。</param>
    /// <param name="headerHeight">ヘッダの高さ。</param>
    /// <returns>計算した本文のセクションの上端位置。</returns>
    public static double CalculateBodyTop(in Margin margin, double headerHeight) =>
        margin.Top + headerHeight;

    /// <summary>
    /// 本文のセクションの高さを計算します。
    /// </summary>
    /// <param name="page">PDFのページ</param>
    /// <param name="margin">PDFのマージン</param>
    /// <param name="headerHeight">ヘッダの高さ。</param>
    /// <param name="footerHeight">フッターの高さ。</param>
    /// <returns>計算した本文のセクションの高さ</returns>
    public static double CalculateBodyHeight(PdfPage page, in Margin margin, double headerHeight, double footerHeight) =>
        page.Height.Point - (margin.Top + margin.Bottom + headerHeight + footerHeight);

    /// <summary>
    /// ページの幅を計算します。
    /// </summary>
    /// <param name="page">PDFのページ</param>
    /// <param name="margin">PDFのマージン</param>
    /// <returns>計算したページの幅。</returns>
    public static double CalculatePageWidth(PdfPage page, in Margin margin) =>
        page.Width.Point - (margin.Left + margin.Right);
    #endregion
}
