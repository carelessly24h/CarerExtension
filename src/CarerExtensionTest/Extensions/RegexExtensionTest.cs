namespace CarerExtensionTest.Extensions;

[TestClass]
public class RegexExtensionTest
{
    [TestMethod]
    [SuppressMessage("Performance", "SYSLIB1045:'GeneratedRegexAttribute' に変換します。", Justification = "<保留中>")]
    public void MultiLine01()
    {
        {
            var r = new Regex(".", RegexOptions.Multiline);
            Assert.IsTrue(r.MultiLine());
        }
        {
            var r = new Regex(".");
            Assert.IsFalse(r.MultiLine());
        }
    }
}
