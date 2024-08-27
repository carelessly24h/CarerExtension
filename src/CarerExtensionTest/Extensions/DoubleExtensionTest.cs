namespace CarerExtensionTest.Extensions;

[TestClass]
public class DoubleExtensionTest
{
    #region MultipleOf
    [TestMethod]
    public void MultipleOf01()
    {
        Assert.IsTrue(0d.MultipleOf(0));
        Assert.IsTrue(0d.MultipleOf(1));
        Assert.IsFalse(0.01.MultipleOf(0));

        Assert.IsTrue(10d.MultipleOf(10));
        Assert.IsTrue(10d.MultipleOf(5));
        Assert.IsFalse(10d.MultipleOf(3));
    }

    [TestMethod]
    public void MultipleOf02()
    {
        Assert.IsTrue(0d.MultipleOf(0L));
        Assert.IsTrue(0d.MultipleOf(1L));
        Assert.IsFalse(0.01.MultipleOf(0L));

        Assert.IsTrue(10d.MultipleOf(10L));
        Assert.IsTrue(10d.MultipleOf(5L));
        Assert.IsFalse(10d.MultipleOf(3L));
    }

    [TestMethod]
    public void MultipleOf03()
    {
        Assert.IsTrue(0d.MultipleOf(0d));
        Assert.IsTrue(0d.MultipleOf(1d));
        Assert.IsFalse(0.01.MultipleOf(0d));

        Assert.IsTrue(10d.MultipleOf(10d));
        Assert.IsTrue(10d.MultipleOf(0.5));
        Assert.IsFalse(10d.MultipleOf(5.01));
    }

    [TestMethod]
    public void MultipleOf04()
    {
        Assert.IsTrue(0d.MultipleOf(0m));
        Assert.IsTrue(0d.MultipleOf(1m));
        Assert.IsFalse(0.01.MultipleOf(0m));

        Assert.IsTrue(10d.MultipleOf(10m));
        Assert.IsTrue(10d.MultipleOf(5m));
        Assert.IsFalse(10d.MultipleOf(3m));
    }
    #endregion
}
