using CarerExtension.IO.Pdf.Attributes;
using CarerExtension.IO.Pdf.Extensions;
using CarerExtension.IO.Pdf.Section;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace CarerExtension.IO.Pdf;

/// <summary>
/// PDFファイルの入出力を行うクラス
/// </summary>
public abstract class PdfIO : IDisposable
{
    #region variables
    /// <summary>
    /// PDFドキュメント。
    /// </summary>
    private readonly PdfDocument document;

    /// <summary>
    /// インスタンスから、本文のセクションを取得するセレクター。
    /// </summary>
    private readonly PdfSectionSelector<PdfBodyAttribute> bodySelector;

    /// <summary>
    /// インスタンスから、ヘッダーのセクションを取得するセレクター。
    /// </summary>
    private readonly PdfSectionSelector<PdfHeaderAttribute> headerSelector;

    /// <summary>
    /// インスタンスから、フッターのセクションを取得するセレクター。
    /// </summary>
    private readonly PdfSectionSelector<PdfFooterAttribute> footerSelector;
    #endregion

    #region properties
    /// <summary>
    /// PDFのページサイズ。
    /// </summary>
    protected virtual PageSize PageSize => PageSize.A4;

    /// <summary>
    /// PDFドキュメントのマージン
    /// </summary>
    protected virtual Margin Margin => new(0, 0, 0, 0);

    /// <summary>
    /// ヘッダーの高さを設定・取得します。
    /// </summary>
    protected virtual double HeaderHeight => 0;

    /// <summary>
    /// フッターの高さを設定・取得します。
    /// </summary>
    protected virtual double FooterHeight => 0;
    #endregion

    #region constructor
    /// <summary>
    /// PDFファイルの入出力を行うクラスを初期化します。
    /// </summary>
    protected PdfIO() : this(new PdfDocument())
    {
    }

    /// <summary>
    /// PDFファイルを読み込んで、入出力を行うクラスを初期化します。
    /// </summary>
    /// <param name="filePath">PDFファイルパス</param>
    protected PdfIO(string filePath) : this(PdfReader.Open(filePath, PdfDocumentOpenMode.Modify))
    {
    }

    /// <summary>
    /// PDFファイルの入出力を行うクラスを初期化します。
    /// </summary>
    /// <param name="document">PDFドキュメント</param>
    protected PdfIO(PdfDocument document)
    {
        this.document = document;

        bodySelector = new(this);
        headerSelector = new(this);
        footerSelector = new(this);
    }
    #endregion

    #region methods
    /// <summary>
    /// 解放処理
    /// </summary>
    public virtual void Dispose()
    {
        try
        {
            document.Dispose();
            bodySelector.Dispose();
            headerSelector.Dispose();
            footerSelector.Dispose();
        }
        finally
        {
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// ドキュメントを描画します。
    /// </summary>
    private void DrawDocument()
    {
        DocumentSettings(document.Info);
        DrawPages();
    }

    /// <summary>
    /// ドキュメントの設定を行います。
    /// </summary>
    /// <param name="information">PDFドキュメントの設定</param>
    protected virtual void DocumentSettings(PdfDocumentInformation information)
    {
        // default settings.
        information.Author = "";
        information.Creator = "";
        information.Keywords = "";
        information.Subject = "";
        information.Title = "";
        information.CreationDate = DateTime.Now;
        information.ModificationDate = DateTime.Now;
    }

    /// <summary>
    /// ページを描画します。
    /// </summary>
    private void DrawPages()
    {
        var totalPageNumber = bodySelector.GetTotalPageNumber();
        for (var pageNumber = 1; pageNumber <= totalPageNumber; pageNumber++)
        {
            var page = document.GetPage(pageNumber);
            DrawPage(page, pageNumber);
        }
    }

    /// <summary>
    /// ページを描画します。
    /// </summary>
    /// <param name="page">PDFのページ。</param>
    /// <param name="pageNumber">ページ番号。</param>
    private void DrawPage(PdfPage page, int pageNumber)
    {
        PageSettings(page, pageNumber);
        DrawHeader(page, pageNumber);
        DrawFooter(page, pageNumber);
        DrawBody(page, pageNumber);
    }

    /// <summary>
    /// ページの設定を行います。
    /// </summary>
    /// <param name="page">PDFのページ。</param>
    /// <param name="pageNumber">ページ番号。</param>
    protected virtual void PageSettings(PdfPage page, int pageNumber)
    {
        // default settings.
        page.Comment = "";
        page.Orientation = PageOrientation.Portrait;
        page.Size = PageSize;

        // don't use TrimMargins.
        // page size will not be correct.
        // page.TrimMargins.Top = new(Margin.Top);
        // page.TrimMargins.Left = new(Margin.Left);
        // page.TrimMargins.Right = new(Margin.Right);
        // page.TrimMargins.Bottom = new(Margin.Bottom);
    }

    /// <summary>
    /// ヘッダーを描画します。
    /// </summary>
    /// <param name="page">PDFのページ。</param>
    /// <param name="pageNumber">ページ番号。</param>
    private void DrawHeader(PdfPage page, int pageNumber)
    {
        var methods = headerSelector.Select();
        var section = PdfSection.ToHeader(page, Margin, HeaderHeight);
        var totalPageNumber = bodySelector.GetTotalPageNumber();
        var args = new SectionArgs(document, page, section, pageNumber, totalPageNumber);
        DrawSection(methods, args);
    }

    /// <summary>
    /// フッターを描画します。
    /// </summary>
    /// <param name="page">PDFのページ。</param>
    /// <param name="pageNumber">ページ番号。</param>
    private void DrawFooter(PdfPage page, int pageNumber)
    {
        var methods = footerSelector.Select();
        var section = PdfSection.ToFooter(page, Margin, HeaderHeight, FooterHeight);
        var totalPageNumber = bodySelector.GetTotalPageNumber();
        var args = new SectionArgs(document, page, section, pageNumber, totalPageNumber);
        DrawSection(methods, args);
    }

    /// <summary>
    /// 本文を描画します。
    /// </summary>
    /// <param name="page">PDFのページ。</param>
    /// <param name="pageNumber">ページ番号。</param>
    private void DrawBody(PdfPage page, int pageNumber)
    {
        var methods = bodySelector.Select(pageNumber);
        var section = PdfSection.ToBody(page, Margin, HeaderHeight, FooterHeight);
        var totalPageNumber = bodySelector.GetTotalPageNumber();
        var args = new SectionArgs(document, page, section, pageNumber, totalPageNumber);
        DrawSection(methods, args);
    }

    /// <summary>
    /// セクションを描画します。
    /// </summary>
    /// <typeparam name="T">セクションの型。</typeparam>
    /// <param name="sectionMethods">セクションを描画するメソッドの一覧。</param>
    /// <param name="e">セクションの引数。</param>
    private void DrawSection<T>(IEnumerable<PdfSectionSelector<T>.PdfSectionMethod> sectionMethods, SectionArgs e) where T : PdfSectionAttribute
    {
        var methods = sectionMethods.Select(m => m.Method);
        foreach (var method in methods)
        {
            method.Invoke(this, [e]);
        }
    }

    /// <summary>
    /// PDFファイルを書き込みます。
    /// </summary>
    /// <param name="filePath">出力先ファイルパス。</param>
    public void Write(string filePath)
    {
        DrawDocument();
        document.Save(filePath);
    }
    #endregion
}
