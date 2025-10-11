namespace CarerExtension.IO.Json;

/// <summary>
/// JSONファイルの入出力を行うクラス
/// </summary>
/// <typeparam name="T">JSONファイルの型</typeparam>
public abstract class JsonIO<T> where T : JsonIO<T>, new()
{
    #region constant
    /// <summary>
    /// ファイルのデフォルトエンコード
    /// </summary>
    protected static readonly Encoding defaultEncoding = Encoding.UTF8;
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
    /// <returns>読み込んだJSONファイル</returns>
    public static T Read(string path) =>
        Read(path, defaultEncoding, ReadConfigure());

    /// <summary>
    /// ファイルを読み込む
    /// </summary>
    /// <remarks>
    /// デフォルトの設定でファイルを読み込む
    /// </remarks>
    /// <param name="path">読み込みファイルパス</param>
    /// <param name="encoding">ファイルのエンコード</param>
    /// <returns>読み込んだJSONファイル</returns>
    public static T Read(string path, Encoding encoding) =>
        Read(path, encoding, ReadConfigure());

    /// <summary>
    /// ファイルを読み込む
    /// </summary>
    /// <param name="path">読み込みファイルパス</param>
    /// <param name="encoding">ファイルのエンコード</param>
    /// <param name="options">読み込み設定</param>
    /// <returns>読み込んだJSONファイル</returns>
    public static T Read(string path, JsonSerializerOptions options) =>
        Read(path, defaultEncoding, options);

    /// <summary>
    /// ファイルを読み込む
    /// </summary>
    /// <param name="path">読み込みファイルパス</param>
    /// <param name="encoding">ファイルのエンコード</param>
    /// <param name="options">読み込み設定</param>
    /// <returns>読み込んだJSONファイル</returns>
    public static T Read(string path, Encoding encoding, JsonSerializerOptions options)
    {
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var reader = new StreamReader(stream, encoding);

        var jsonText = reader.ReadToEnd();
        var json = JsonSerializer.Deserialize<T>(jsonText, options);
        return json ?? new();
    }

    /// <summary>
    /// デフォルトの読み込み設定
    /// </summary>
    /// <returns>読み込み設定</returns>
    private static JsonSerializerOptions ReadConfigure() => new()
    {
        WriteIndented = true,
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
        Write(path, defaultEncoding, WriteConfigure());

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
    /// <param name="options">書き込み設定</param>
    public virtual void Write(string path, Encoding encoding, JsonSerializerOptions options)
    {
        using var writer = new StreamWriter(path, false, encoding);
        var json = JsonSerializer.Serialize((T)this, options);
        writer.Write(json);
    }

    /// <summary>
    /// デフォルトの書き込み設定
    /// </summary>
    /// <returns>書き込み設定</returns>
    private static JsonSerializerOptions WriteConfigure() => new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true,
    };
    #endregion
    #endregion
}
