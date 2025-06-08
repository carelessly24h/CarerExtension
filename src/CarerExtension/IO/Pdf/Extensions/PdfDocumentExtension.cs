using PdfSharp.Pdf;

namespace CarerExtension.IO.Pdf.Extensions;

/// <summary>
/// PdfDocumentの拡張メソッドを提供するクラス
/// </summary>
public static class PdfDocumentExtension
{
    /// <summary>
    /// ページ番号を指定して、PDFドキュメントからページを取得します。
    /// ページが存在しない場合は新しいページを追加します。
    /// </summary>
    /// <param name="document">ページを検索するPDFドキュメント。</param>
    /// <param name="pageNumber">ページ番号。</param>
    /// <returns>PDFドキュメントのページ</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PdfPage GetPage(this PdfDocument document, int pageNumber)
    {
        if (pageNumber <= document.PageCount)
        {
            return document.Pages[pageNumber - 1];
        }
        {
            return document.AddPage();
        }
    }
}
