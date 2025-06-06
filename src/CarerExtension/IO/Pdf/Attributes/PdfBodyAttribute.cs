namespace CarerExtension.IO.Pdf.Attributes;

/// <summary>
/// PDFの本文を指定する属性クラスです。
/// </summary>
/// <param name="pageNumber">ページ番号を指定します。</param>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class PdfBodyAttribute(int pageNumber) : PdfSectionAttribute(pageNumber)
{
}
