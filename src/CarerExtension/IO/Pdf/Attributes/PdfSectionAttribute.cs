namespace CarerExtension.IO.Pdf.Attributes;

/// <summary>
/// PDFのセクションを指定する属性クラスです。
/// </summary>
/// <param name="pageNumber">ページ番号を指定します。</param>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public abstract class PdfSectionAttribute(int pageNumber) : Attribute
{
    /// <summary>
    /// ページを指定します。
    /// </summary>
    /// <remarks>
    /// 1以上の数値を指定できます。
    /// 0以下の数値を指定した場合、そのセクションは描画されません。
    /// </remarks>
    public int PageNumber => pageNumber;
}
