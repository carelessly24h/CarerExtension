using System.Xml.Serialization;

namespace CarerExtensionTest.IO.TestModels;

[XmlRoot("RootNode")]
public class TestXmlFile : XmlIO<TestXmlFile>
{
    [XmlElement("int-Node")]
    public int? IntNode { get; set; }

    [XmlElement("double-Node")]
    public double? DoubleNode { get; set; }

    [XmlElement("child-Node1")]
    public ChildNode1? Child1 { get; set; }

    [XmlElement("child-Node2")]
    public ChildNode2? Child2 { get; set; }

    public class ChildNode1
    {
        [XmlElement("string-Node")]
        public string? StringNode { get; set; }

        [XmlElement("date-Node")]
        public DateTime? DateTimeNode { get; set; }
    }

    public class ChildNode2
    {
        [XmlElement("bool-Node")]
        public bool? BoolNode { get; set; }
    }
}
