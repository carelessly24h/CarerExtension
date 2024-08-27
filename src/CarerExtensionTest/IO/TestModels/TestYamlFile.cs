using YamlDotNet.Serialization;

namespace CarerExtensionTest.IO.TestModels;

internal class TestYamlFile : YamlIO<TestYamlFile>
{
    [YamlMember(Alias = "intItem")]
    public int? IntValue { get; set; }

    [YamlMember(Alias = "doubleItem")]
    public double? DoubleValue { get; set; }

    [YamlMember(Alias = "stringItem")]
    public string? StringValue { get; set; }

    [YamlMember(Alias = "dateItem")]
    public DateTime? DateTimeValue { get; set; }

    [YamlMember(Alias = "boolItem")]
    public bool? BoolValue { get; set; }

    [YamlMember(Alias = "dictionaryItem")]
    public Dictionary<string, string> DictionaryValue { get; set; } = [];

    [YamlIgnore]
    public int NoTarget1 = 1;
}
