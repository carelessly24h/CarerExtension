namespace CarerExtension.IO.Pdf.Attributes;

/// <summary>
/// PDFのフッターを指定する属性クラスです。
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class PdfFooterAttribute() : PdfSectionAttribute(0)
{
}
