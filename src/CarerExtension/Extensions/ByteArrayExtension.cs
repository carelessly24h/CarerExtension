namespace CarerExtension.Extensions;

public static class ByteArrayExtension
{
    public static bool ToBoolean(this byte[] value, int startIndex = 0) =>
        BitConverter.ToBoolean(value, startIndex);

    public static double ToDouble(this byte[] value, int startIndex = 0) =>
        BitConverter.ToDouble(value, startIndex);

    public static int ToInt32(this byte[] value, int startIndex = 0) =>
        BitConverter.ToInt32(value, startIndex);

    public static long ToInt64(this byte[] value, int startIndex = 0) =>
        BitConverter.ToInt64(value, startIndex);
}
