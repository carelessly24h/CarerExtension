namespace CarerExtensionTest.IO.Xml;

[TestClass]
public class XmlTest
{
    private const string ROOT_DIR = @"test\xml_test";

    [ClassInitialize]
    public static void Initialize(TestContext _)
    {
        Directory.CreateDirectory(ROOT_DIR);
    }

    [TestMethod]
    public void Read01()
    {
        var dir = $@"{ROOT_DIR}\read1";
        var readFile = $@"{dir}\read.xml";

        #region pre-process
        Directory.CreateDirectory(dir);
        File.WriteAllText(readFile, XML_CONTENT);
        #endregion

        var xml = TestXmlFile.Read(readFile);

        Assert.AreEqual(1, xml.IntNode);
        Assert.AreEqual(1.5, xml.DoubleNode);
        Assert.AreEqual("string1", xml.Child1?.StringNode);
        Assert.AreEqual(new(2001, 1, 1), xml.Child1?.DateTimeNode);
        Assert.IsTrue(xml.Child2?.BoolNode);
    }

    [TestMethod]
    public void Write01()
    {
        var dir = $@"{ROOT_DIR}\write1";
        var writeFile = $@"{dir}\write.xml";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        var xml = new TestXmlFile()
        {
            IntNode = 1,
            DoubleNode = 1.5,
            Child1 = new()
            {
                StringNode = "string1",
                DateTimeNode = new(2001, 1, 1),
            },
            Child2 = new()
            {
                BoolNode = false,
            },
        };
        xml.Write(writeFile);

        Assert.IsTrue(File.Exists(writeFile));
    }

    private const string XML_CONTENT = @"<?xml version=""1.0"" encoding=""utf-8""?>
<RootNode xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <int-Node>1</int-Node>
  <double-Node>1.5</double-Node>
  <child-Node1>
    <string-Node>string1</string-Node>
    <date-Node>2001-01-01T00:00:00</date-Node>
  </child-Node1>
  <child-Node2>
    <bool-Node>true</bool-Node>
  </child-Node2>
</RootNode>";
}
