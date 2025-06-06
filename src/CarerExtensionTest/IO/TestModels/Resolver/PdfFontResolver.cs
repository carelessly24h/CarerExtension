using PdfSharp.Fonts;

namespace CarerExtensionTest.IO.TestModels.Resolver;

internal class PdfFontResolver : IFontResolver
{
    const string WindowsFontPath = @"C:\Windows\Fonts";

    public FontResolverInfo? ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        var name = familyName.ToLower();
        return name switch
        {
            "genshin gothic" => new("GenShin Gothic#Medium"),
            _ => new(familyName, isBold, isItalic),
        };
    }

    public byte[]? GetFont(string faceName) =>
        faceName switch
        {
            "Arial" => File.ReadAllBytes(Path.Combine(WindowsFontPath, "arial.ttf")),
            "MS Gothic" => File.ReadAllBytes(Path.Combine(WindowsFontPath, "msgothic.ttc")),
            "GenShin Gothic#Medium" => LoadFontFromResource(ResourceFiles.GenShinGothicMedium),
            _ => LoadFontFromFile(Path.Combine(WindowsFontPath, faceName)),
        };

    public static byte[] LoadFontFromResource(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(resourceName) ??
            throw new NullReferenceException($"Resource '{resourceName}' not found.");

        return LoadFontFromResource(stream);
    }

    public static byte[] LoadFontFromResource(Stream stream)
    {
        var data = new byte[stream.Length];
        stream.Read(data, 0, data.Length);
        return data;
    }

    private static byte[] LoadFontFromFile(string filePath) => File.ReadAllBytes(filePath);
}
