namespace CarerExtension.IO.Json;

public abstract class JsonIO<T> where T : JsonIO<T>, new()
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

    public static T Read(string path, Encoding encoding, JsonSerializerOptions options)
    {
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var reader = new StreamReader(stream, encoding);

        var jsonText = reader.ReadToEnd();
        var json = JsonSerializer.Deserialize<T>(jsonText, options);
        return json ?? new();
    }

    private static JsonSerializerOptions ReadConfigure() => new()
    {
        WriteIndented = true,
    };
    #endregion

    #region writing
    public virtual void Write(string path) =>
        Write(path, DEFAULT_ENCODING, WriteConfigure());

    public virtual void Write(string path, Encoding encoding) =>
        Write(path, encoding, WriteConfigure());

    public virtual void Write(string path, Encoding encoding, JsonSerializerOptions options)
    {
        using var writer = new StreamWriter(path, false, encoding);
        var json = JsonSerializer.Serialize((T)this, options);
        writer.Write(json);
    }

    private static JsonSerializerOptions WriteConfigure() => new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true,
    };
    #endregion
    #endregion
}
