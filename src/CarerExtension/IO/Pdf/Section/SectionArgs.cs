using PdfSharp.Pdf;

namespace CarerExtension.IO.Pdf.Section;

/// <summary>
/// セクションを描画する際に必要なパラメータを提供するクラスです。
/// </summary>
/// <param name="Document">PDFドキュメント。</param>
/// <param name="Page">現在のページ。</param>
/// <param name="Section">セクションの情報。</param>
/// <param name="PageNumber">現在のページ番号。</param>
/// <param name="TotalPageNumber">PDFの総ページ数。</param>
public record SectionArgs(PdfDocument Document, PdfPage Page, PdfSection Section, int PageNumber, int TotalPageNumber)
{
    /// <summary>
    /// PDFのページに描画するためのグラフィックスオブジェクトを作成します。
    /// </summary>
    /// <returns>現在のページから作成したグラフィックスオブジェクト。</returns>
    public PdfGraphics GetGraphics() => new(Page, Section);
}
