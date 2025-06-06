using CarerExtension.IO.Pdf.Attributes;

namespace CarerExtension.IO.Pdf.Section;

/// <summary>
/// インスタンスから、セクションを描画するメソッドを取得するためのクラスです。
/// </summary>
/// <typeparam name="T">セクションの種類を示す型。</typeparam>
/// <param name="methods">インスタンスのメソッド。</param>
internal class PdfSectionSelector<T>(IEnumerable<MethodInfo> methods) : IDisposable where T : PdfSectionAttribute
{
    /// <summary>
    /// セクションを描画するメソッドを表す構造体です。
    /// セクションを描画するために必要な情報をすべて保持します。
    /// </summary>
    /// <param name="Method">セクションを描画するメソッド。</param>
    /// <param name="PageNumber">セクションを描画するページ。</param>
    internal record struct PdfSectionMethod(MethodInfo Method, int PageNumber);

    #region variables
    /// <summary>
    /// セクションを描画するメソッドのキャッシュ。
    /// </summary>
    private IEnumerable<PdfSectionMethod>? methodsCache = null;

    /// <summary>
    /// 総ページ数のキャッシュ。
    /// </summary>
    /// <remarks>
    /// 総ページ数は、本文のセクションから取得します。
    /// </remarks>
    private int? totalPageNumberCache = null;
    #endregion

    #region constructor
    /// <summary>
    /// セクションを描画するメソッドを取得するためのクラスを初期化します。
    /// </summary>
    /// <param name="instance">メソッドを取得するインスタンス。</param>
    internal PdfSectionSelector(object instance) : this(instance.GetType().GetMethods())
    {
    }
    #endregion

    #region methods
    /// <summary>
    /// 解放処理
    /// </summary>
    public void Dispose()
    {
        methodsCache = null;
        totalPageNumberCache = null;
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// セクションを描画するメソッドを検索します。
    /// </summary>
    /// <returns>セクションを描画するメソッドの一覧。</returns>
    public IEnumerable<PdfSectionMethod> Select()
    {
        // this is heavy.so it's cache it.
        methodsCache ??= BundleSectionInfo(methods);
        return methodsCache;
    }

    /// <summary>
    /// ページ番号をセクションを描画するメソッドを検索します。
    /// </summary>
    /// <param name="pageNumber">ページ番号。</param>
    /// <returns>指定したページに対応する、セクションを描画するメソッドの一覧。</returns>
    public IEnumerable<PdfSectionMethod> Select(int pageNumber) =>
        Select().Where(m => m.PageNumber == pageNumber);

    /// <summary>
    /// 総ページ数を取得します。
    /// </summary>
    /// <returns>総ページ数</returns>
    public int GetTotalPageNumber()
    {
        // this is heavy.so it's cache it.
        totalPageNumberCache ??= Select().Max(m => m.PageNumber);
        return totalPageNumberCache ?? 0;
    }

    /// <summary>
    /// セクションの描画用のメソッドを検索します。
    /// その際に描画に必要な情報をまとめます。
    /// </summary>
    /// <param name="methods">検索対象のメソッドの一覧</param>
    /// <returns>セクションの描画に必要な情報の一覧</returns>
    private static IEnumerable<PdfSectionMethod> BundleSectionInfo(IEnumerable<MethodInfo> methods)
    {
        foreach (var method in methods)
        {
            var attrs = method.GetCustomAttributes<T>(true);
            foreach (var attr in attrs)
            {
                yield return new(method, attr.PageNumber);
            }
        }
    }
    #endregion
}
