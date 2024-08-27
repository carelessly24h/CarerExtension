namespace CarerExtension.IO.Xml;

public abstract class XmlIO<T> where T : new()
{
    #region constant
    protected static readonly Encoding DEFAULT_ENCODING = Encoding.UTF8;
    #endregion

    #region method
    #region reading
    public static T Read(string path) =>
        Read(path, DEFAULT_ENCODING, ReadConfigure());

    public static T Read(string path, Encoding encoding) =>
        Read(path, encoding, ReadConfigure());

    public static T Read(string path, Encoding encoding, XmlReaderSettings settings)
    {
        using var stream = new StreamReader(path, encoding);
        using var reader = XmlReader.Create(stream, settings);

        var xml = new XmlSerializer(typeof(T));
        return (T?)xml.Deserialize(reader) ?? new T();
    }

    private static XmlReaderSettings ReadConfigure() => new()
    {
        CheckCharacters = true,
    };
    #endregion

    #region writing
    public virtual void Write(string path) =>
        Write(path, DEFAULT_ENCODING, WriteConfigure());

    public virtual void Write(string path, Encoding encoding) =>
        Write(path, encoding, WriteConfigure());

    public virtual void Write(string path, Encoding encoding, XmlWriterSettings settings)
    {
        using var stream = new StreamWriter(path, false, encoding);
        using var writer = XmlWriter.Create(stream, settings);

        var xml = new XmlSerializer(typeof(T));
        xml.Serialize(writer, this);
        stream.Flush();
    }

    protected virtual XmlWriterSettings WriteConfigure() => new()
    {
        CheckCharacters = true,
    };
    #endregion
    #endregion
}
