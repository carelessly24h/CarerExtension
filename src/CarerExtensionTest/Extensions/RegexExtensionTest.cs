namespace CarerExtensionTest.Extensions;

[TestClass]
public class RegexExtensionTest
{
    [TestMethod]
    public void IsMultiLine01()
    {
        {
            var r = new Regex(".", RegexOptions.Multiline);
            Assert.IsTrue(r.IsMultiLine());
        }
        {
            var r = new Regex(".");
            Assert.IsFalse(r.IsMultiLine());
        }
    }
}
