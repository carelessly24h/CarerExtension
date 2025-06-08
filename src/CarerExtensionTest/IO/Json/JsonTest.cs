namespace CarerExtensionTest.IO.Json;

[TestClass]
public class JsonTest
{
    private const string RootDir = @"test\json_test";

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
        File.WriteAllText(readFile, JsonContent);
        #endregion

        var json = TestJsonFile.Read(readFile);

        Assert.AreEqual(1, json.IntValue);
        Assert.AreEqual(1.5, json.DoubleValue);
        Assert.AreEqual("string1", json.StringValue);
        Assert.AreEqual(new(2001, 1, 1), json.DateTimeValue);
        Assert.IsTrue(json.BoolValue);
        Assert.AreEqual("value1", json.DictionaryValue["item1"]);
        Assert.AreEqual("value2", json.DictionaryValue["item2"]);
    }

    [TestMethod]
    public void Write01()
    {
        var dir = $@"{RootDir}\write1";
        var writeFile = $@"{dir}\write.json";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        var json = new TestJsonFile()
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
        json.Write(writeFile);

        Assert.IsTrue(File.Exists(writeFile));
    }

    private const string JsonContent = @"
{
  ""intItem"": 1,
  ""doubleItem"": 1.5,
  ""stringItem"": ""string1"",
  ""dateItem"": ""20010101000000"",
  ""boolItem"": true,
  ""dictionaryItem"": {
    ""item1"": ""value1"",
    ""item2"": ""value2""
  }
}";
}
