namespace CarerExtensionTest.IO.Yaml;

[TestClass]
public class YamlTest
{
    private const string RootDir = @"test\yaml_test";

    [ClassInitialize]
    public static void Initialize(TestContext _)
    {
        Directory.CreateDirectory(RootDir);
    }

    [TestMethod]
    public void Read01()
    {
        var dir = $@"{RootDir}\read1";
        var readFile = $@"{dir}\read.json";

        #region pre-process
        Directory.CreateDirectory(dir);
        File.WriteAllText(readFile, YamlContent);
        #endregion

        var yaml = TestYamlFile.Read(readFile);

        Assert.AreEqual(1, yaml.IntValue);
        Assert.AreEqual(1.5, yaml.DoubleValue);
        Assert.AreEqual("string1", yaml.StringValue);
        Assert.AreEqual(new(2001, 1, 1), yaml.DateTimeValue);
        Assert.IsTrue(yaml.BoolValue);
        Assert.AreEqual("value1", yaml.DictionaryValue["item1"]);
        Assert.AreEqual("value2", yaml.DictionaryValue["item2"]);
    }

    [TestMethod]
    public void Write01()
    {
        var dir = $@"{RootDir}\write1";
        var writeFile = $@"{dir}\write.yaml";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        var yaml = new TestYamlFile()
        {
            IntValue = 1,
            DoubleValue = 1.5,
            StringValue = "string1",
            DateTimeValue = new(2001, 1, 1),
            BoolValue = false,
            DictionaryValue = {
               ["item1"] = "value1",
               ["item2"] = "value2",
            },
        };
        yaml.Write(writeFile);

        Assert.IsTrue(File.Exists(writeFile));
    }

    private const string YamlContent = @"
intItem: 1
doubleItem: 1.5
stringItem: string1
dateItem: 2001/01/01
boolItem: true
dictionaryItem:
  item1: value1
  item2: value2
";
}
