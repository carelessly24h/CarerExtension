using CsvHelper;
using CsvHelper.Configuration;

namespace CarerExtension.IO.Csv;

/// <summary>
/// CSVファイルの入出力を行うクラス
/// </summary>
/// <typeparam name="T">CSV行の型</typeparam>
/// <param name="path">ファイルパス</param>
/// <param name="encoding">ファイルのエンコード</param>
public abstract class CsvIO<T>(string path, Encoding encoding) : IDisposable where T : new()
{
    #region constant
    /// <summary>
    /// ファイルのデフォルトエンコード
    /// </summary>
    protected static readonly Encoding DEFAULT_ENCODING = Encoding.UTF8;
    #endregion

    #region variable
    /// <summary>
    /// CSVファイルの入出力用のストリーム
    /// </summary>
    protected FileStream stream = new(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

    /// <summary>
    /// CSVファイル読み込み用のストリーム
    /// </summary>
    protected StreamReader? streamReader;

    /// <summary>
    /// CSVファイル読み込み用のリーダー
    /// </summary>
    protected CsvReader? csvReader;

    /// <summary>
    /// CSVファイルのエンコード
    /// </summary>
    protected Encoding encoding = encoding;
    #endregion

    #region constructor
    /// <summary>
    /// CSVファイルの入出力クラス
    /// </summary>
    /// <remarks>
    /// UTF8でエンコードされたCSVファイルを読み書きするクラス。
    /// </remarks>
    /// <param name="path">ファイルパス</param>
    public CsvIO(string path) : this(path, DEFAULT_ENCODING) { }
    #endregion

    #region method
    /// <summary>
    /// 解放処理
    /// </summary>
    public void Dispose()
    {
        try
        {
            csvReader?.Dispose();
            streamReader?.Dispose();
            stream.Dispose();
        }
        finally
        {
            GC.SuppressFinalize(this);
        }
    }

    #region reading
    /// <summary>
    /// ファイルを読み込む
    /// </summary>
    /// <returns>読み込んだCSV行データ</returns>
    protected virtual IEnumerable<T> Read()
    {
        streamReader = new(stream, encoding);
        csvReader = new(streamReader, ReadConfigure());
        return csvReader.GetRecords<T>();
    }

    /// <summary>
    /// デフォルトの読み込み設定
    /// </summary>
    /// <returns>読み込み設定</returns>
    protected virtual CsvConfiguration ReadConfigure() => new(CultureInfo.CurrentCulture);
    #endregion

    #region writing
    /// <summary>
    /// ファイルを書き込む
    /// </summary>
    /// <param name="contents">書き込むCSV行データ</param>
    protected virtual void Write(IEnumerable<T> contents) => Write(stream, encoding, contents);

    /// <summary>
    /// ファイルを書き込む
    /// </summary>
    /// <param name="path">出力先ファイルパス</param>
    /// <param name="contents">書き込むCSV行データ</param>
    protected virtual void Write(string path, IEnumerable<T> contents) => Write(path, encoding, contents);

    /// <summary>
    /// ファイルを書き込む
    /// </summary>
    /// <param name="path">出力先ファイルパス</param>
    /// <param name="encoding">ファイルのエンコード</param>
    /// <param name="contents">書き込むCSV行データ</param>
    protected virtual void Write(string path, Encoding encoding, IEnumerable<T> contents)
    {
        using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write);
        Write(stream, encoding, contents);
    }

    /// <summary>
    /// ファイルを書き込む
    /// </summary>
    /// <param name="stream">出力ファイルのストリーム</param>
    /// <param name="encoding">ファイルのエンコード</param>
    /// <param name="contents">書き込むCSV行データ</param>
    protected virtual void Write(FileStream stream, Encoding encoding, IEnumerable<T> contents)
    {
        using var writer = new StreamWriter(stream, encoding);
        using var csv = new CsvWriter(writer, WriteConfigure());
        csv.WriteRecords(contents);
    }

    /// <summary>
    /// デフォルトの書き込み設定
    /// </summary>
    /// <returns>書き込み設定</returns>
    protected virtual CsvConfiguration WriteConfigure() => new(CultureInfo.CurrentCulture);
    #endregion
    #endregion
}
