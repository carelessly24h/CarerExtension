namespace CarerExtension.IO.Pdf.Attributes;

/// <summary>
/// PDFのヘッダーを指定する属性クラスです。
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class PdfHeaderAttribute() : PdfSectionAttribute(0)
{
}
