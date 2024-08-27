using CsvHelper;
using CsvHelper.Configuration;

namespace CarerExtension.IO.Csv;

public abstract class CsvIO<T>(string path, Encoding encoding) : IDisposable where T : new()
{
    #region constant
    protected static readonly Encoding DEFAULT_ENCODING = Encoding.UTF8;
    #endregion

    #region variable
    protected FileStream stream = new(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

    protected StreamReader? streamReader;

    protected CsvReader? csvReader;

    protected Encoding encoding = encoding;
    #endregion

    #region constructor
    public CsvIO(string path) : this(path, DEFAULT_ENCODING) { }
    #endregion

    #region method
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
    protected virtual IEnumerable<T> Read()
    {
        streamReader = new(stream, encoding);
        csvReader = new(streamReader, ReadConfigure());
        return csvReader.GetRecords<T>();
    }

    protected virtual CsvConfiguration ReadConfigure() => new(CultureInfo.CurrentCulture);
    #endregion

    #region writing
    protected virtual void Write(IEnumerable<T> contents) => Write(stream, encoding, contents);

    protected virtual void Write(string path, IEnumerable<T> contents) => Write(path, encoding, contents);

    protected virtual void Write(string path, Encoding encoding, IEnumerable<T> contents)
    {
        using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write);
        Write(stream, encoding, contents);
    }

    protected virtual void Write(FileStream stream, Encoding encoding, IEnumerable<T> contents)
    {
        using var writer = new StreamWriter(stream, encoding);
        using var csv = new CsvWriter(writer, WriteConfigure());
        csv.WriteRecords(contents);
    }

    protected virtual CsvConfiguration WriteConfigure() => new(CultureInfo.CurrentCulture);
    #endregion
    #endregion
}
