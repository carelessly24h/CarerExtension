using YamlDotNet.Serialization;

namespace CarerExtension.IO.Yaml;

/// <summary>
/// YAMLファイルの入出力を行うクラス
/// </summary>
/// <typeparam name="T">YAMLファイルの型</typeparam>
public abstract class YamlIO<T> where T : new()
{
    #region constant
    /// <summary>
    /// ファイルのデフォルトエンコード
    /// </summary>
    private static readonly Encoding DEFAULT_ENCODING = Encoding.UTF8;
    #endregion

    #region method
    #region reading
    /// <summary>
    /// ファイルを読み込む
    /// </summary>
    /// <remarks>
    /// デフォルトのエンコードでファイルを読み込む
    /// </remarks>
    /// <param name="path">読み込みファイルパス</param>
    /// <returns>読み込んだYAMLファイル</returns>
    public static T Read(string path) => Read(path, DEFAULT_ENCODING);

    /// <summary>
    /// ファイルを読み込む
    /// </summary>
    /// <param name="path">読み込みファイルパス</param>
    /// <param name="encoding">ファイルのエンコード</param>
    /// <returns>読み込んだYAMLファイル</returns>
    public static T Read(string path, Encoding encoding)
    {
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        using var reader = new StreamReader(stream, encoding);

        var yaml = new Deserializer();
        return yaml.Deserialize<T>(reader);
    }
    #endregion

    #region writing
    /// <summary>
    /// ファイルに書き込む
    /// </summary>
    /// <remarks>
    /// デフォルトのエンコードでファイルを書き込む
    /// </remarks>
    /// <param name="path">出力先ファイルパス</param>
    public virtual void Write(string path) => Write(path, DEFAULT_ENCODING);

    /// <summary>
    /// ファイルに書き込む
    /// </summary>
    /// <param name="path">出力先ファイルパス</param>
    /// <param name="encoding">ファイルのエンコード</param>
    public virtual void Write(string path, Encoding encoding)
    {
        using var writer = new StreamWriter(path, false, encoding);

        var yaml = new Serializer();
        yaml.Serialize(writer, this);
    }
    #endregion
    #endregion
}
