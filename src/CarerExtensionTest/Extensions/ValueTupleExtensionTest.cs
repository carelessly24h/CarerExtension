namespace CarerExtensionTest.Extensions;

[TestClass]
public class ValueTupleExtensionTest
{
    [TestMethod]
    public void Range01()
    {
        var actual = (10, 5).Range();
        Assert.AreEqual(5, actual.Count());
        Assert.AreEqual(10, actual.ElementAt(0));
        Assert.AreEqual(14, actual.ElementAt(4));
    }
}
