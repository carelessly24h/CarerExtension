using CsvHelper;

namespace CarerExtension.IO.Csv;

public abstract class TolerantCsvIO<T>(string path, Encoding encoding) : CsvIO<T>(path, encoding), IDisposable where T : new()
{
    #region constructor
    public TolerantCsvIO(string path) : this(path, DEFAULT_ENCODING)
    { }
    #endregion

    #region reading
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

    protected virtual void HandleConversionFailure(CsvHelperException exsception)
    {
        // define additional processing if necessary.
    }
    #endregion
}
