namespace CarerExtension.IO.Xml;

/// <summary>
/// XMLファイルの入出力を行うクラス
/// </summary>
/// <typeparam name="T">XMLファイルの型</typeparam>
public abstract class XmlIO<T> where T : new()
{
    #region constant
    /// <summary>
    /// ファイルのデフォルトエンコード
    /// </summary>
    protected static readonly Encoding DEFAULT_ENCODING = Encoding.UTF8;
    #endregion

    #region method
    #region reading
    /// <summary>
    /// ファイルを読み込む
    /// </summary>
    /// <remarks>
    /// デフォルトのエンコードと設定でファイルを読み込む
    /// </remarks>
    /// <param name="path">読み込みファイルパス</param>
    /// <returns>読み込んだXMLファイル</returns>
    public static T Read(string path) =>
        Read(path, DEFAULT_ENCODING, ReadConfigure());

    /// <summary>
    /// ファイルを読み込む
    /// </summary>
    /// <remarks>
    /// デフォルトの設定でファイルを読み込む
    /// </remarks>
    /// <param name="path">読み込みファイルパス</param>
    /// <param name="encoding">ファイルのエンコード</param>
    /// <returns>読み込んだXMLファイル</returns>
    public static T Read(string path, Encoding encoding) =>
        Read(path, encoding, ReadConfigure());

    /// <summary>
    /// ファイルを読み込む
    /// </summary>
    /// <param name="path">読み込みファイルパス</param>
    /// <param name="encoding">ファイルのエンコード</param>
    /// <param name="settings">読み込み設定</param>
    /// <returns>読み込んだXMLファイル</returns>
    public static T Read(string path, Encoding encoding, XmlReaderSettings settings)
    {
        using var stream = new StreamReader(path, encoding);
        using var reader = XmlReader.Create(stream, settings);

        var xml = new XmlSerializer(typeof(T));
        return (T?)xml.Deserialize(reader) ?? new T();
    }

    /// <summary>
    /// デフォルトの読み込み設定
    /// </summary>
    /// <returns>読み込み設定</returns>
    private static XmlReaderSettings ReadConfigure() => new()
    {
        CheckCharacters = true,
    };
    #endregion

    #region writing
    /// <summary>
    /// ファイルに書き込む
    /// </summary>
    /// <remarks>
    /// デフォルトのエンコードと設定でファイルを書き込む
    /// </remarks>
    /// <param name="path">出力先ファイルパス</param>
    public virtual void Write(string path) =>
        Write(path, DEFAULT_ENCODING, WriteConfigure());

    /// <summary>
    /// ファイルに書き込む
    /// </summary>
    /// <remarks>
    /// デフォルトの設定でファイルを書き込む
    /// </remarks>
    /// <param name="path">出力先ファイルパス</param>
    /// <param name="encoding">ファイルのエンコード</param>
    public virtual void Write(string path, Encoding encoding) =>
        Write(path, encoding, WriteConfigure());

    /// <summary>
    /// ファイルに書き込む
    /// </summary>
    /// <param name="path">出力先ファイルパス</param>
    /// <param name="encoding">ファイルのエンコード</param>
    /// <param name="settings">書き込み設定</param>
    public virtual void Write(string path, Encoding encoding, XmlWriterSettings settings)
    {
        using var stream = new StreamWriter(path, false, encoding);
        using var writer = XmlWriter.Create(stream, settings);

        var xml = new XmlSerializer(typeof(T));
        xml.Serialize(writer, this);
        stream.Flush();
    }

    /// <summary>
    /// デフォルトの書き込み設定
    /// </summary>
    /// <returns>書き込み設定</returns>
    protected virtual XmlWriterSettings WriteConfigure() => new()
    {
        CheckCharacters = true,
    };
    #endregion
    #endregion
}
