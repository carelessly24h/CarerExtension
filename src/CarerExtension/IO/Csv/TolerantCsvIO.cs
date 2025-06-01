using CsvHelper;

namespace CarerExtension.IO.Csv;

/// <summary>
/// CSVファイルの入出力を行うクラス
/// </summary>
/// <remarks>
/// CSVファイルの読み込み時に、変換失敗を無視して読み込むクラス。
/// 変換失敗時に処理が必要な場合、その処理は継承先にて実装する。
/// </remarks>
/// <typeparam name="T">CSV行の型</typeparam>
/// <param name="path">ファイルパス</param>
/// <param name="encoding">ファイルのエンコード</param>
public abstract class TolerantCsvIO<T>(string path, Encoding encoding) : CsvIO<T>(path, encoding), IDisposable where T : new()
{
    #region constructor
    /// <summary>
    /// CSVファイルの入出力クラス
    /// </summary>
    /// <remarks>
    /// UTF8でエンコードされたCSVファイルを読み書きするクラス。
    /// </remarks>
    /// <param name="path">ファイルパス</param>
    public TolerantCsvIO(string path) : this(path, DEFAULT_ENCODING)
    { }
    #endregion

    #region reading
    /// <summary>
    /// ファイルを読み込む
    /// </summary>
    /// <returns>読み込んだCSV行データ</returns>
    protected override IEnumerable<T> Read()
    {
        var contents = new List<T>();
        try
        {
            using var reader = new StreamReader(stream, encoding);
            using var csv = new CsvReader(reader, ReadConfigure());
            while (csv.Read())
            {
                var row = ReadRow(csv);
                if (row != null)
                {
                    contents.Add(row);
                }
            }
        }
        catch (CsvHelperException e)
        {
            HandleConversionFailure(e);
        }

        return contents;
    }

    /// <summary>
    /// CSV行を読み込む
    /// 読み込みに失敗した場合は、nullを返す
    /// </summary>
    /// <param name="csv">CSVリーダー</param>
    /// <returns>読み込んだCSV行データ</returns>
    protected virtual T? ReadRow(CsvReader csv)
    {
        try
        {
            return csv.GetRecord<T>();
        }
        catch (CsvHelperException e)
        {
            HandleConversionFailure(e);
            return default;
        }
    }

    /// <summary>
    /// CSV行の読み込みに失敗した場合の処理
    /// </summary>
    /// <param name="exsception">失敗時の例外</param>
    protected virtual void HandleConversionFailure(CsvHelperException exsception)
    {
        // define additional processing if necessary.
    }
    #endregion
}
