using YamlDotNet.Serialization;

namespace CarerExtension.IO.Yaml;

public abstract class YamlIO<T> where T : new()
{
    #region constant
    private static readonly Encoding DEFAULT_ENCODING = Encoding.UTF8;
    #endregion

    #region method
    #region reading
    public static T Read(string path) => Read(path, DEFAULT_ENCODING);

    public static T Read(string path, Encoding encoding)
    {
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        using var reader = new StreamReader(stream, encoding);

        var yaml = new Deserializer();
        return yaml.Deserialize<T>(reader);
    }
    #endregion

    #region writing
    public virtual void Write(string path) => Write(path, DEFAULT_ENCODING);

    public virtual void Write(string path, Encoding encoding)
    {
        using var writer = new StreamWriter(path, false, encoding);

        var yaml = new Serializer();
        yaml.Serialize(writer, this);
    }
    #endregion
    #endregion
}
