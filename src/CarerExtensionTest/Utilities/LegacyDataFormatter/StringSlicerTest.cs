namespace CarerExtensionTest.Utilities.LegacyDataFormatter;

[TestClass]
public class StringSlicerTest
{
    [TestMethod]
    public void IsEmpty01()
    {
        var s = new StringSlicer("abc123");

        Assert.IsFalse(s.IsEmpty);
        s.Shift(6);
        Assert.IsTrue(s.IsEmpty);
    }

    [TestMethod]
    public void IsPresent01()
    {
        var s = new StringSlicer("abc123");

        Assert.IsTrue(s.IsPresent);
        s.Shift(6);
        Assert.IsFalse(s.IsPresent);
    }

    [TestMethod]
    public void Peek01()
    {
        var s = new StringSlicer("abc123");
        Assert.AreEqual("abc", s.Peek(3));
        Assert.AreEqual("a", s.Peek(1));
    }

    [TestMethod]
    public void PeekAll01()
    {
        var s = new StringSlicer("abc123");
        Assert.AreEqual("abc123", s.PeekAll());
        Assert.AreEqual("a", s.Peek(1));
    }

    [TestMethod]
    public void Refresh01()
    {
        var s = new StringSlicer("abc123");
        s.Shift(3);

        Assert.AreEqual("1", s.Peek(1));
        s.Refresh();
        Assert.AreEqual("a", s.Peek(1));
    }

    [TestMethod]
    public void Shift01()
    {
        var s = new StringSlicer("abc123");
        s.Shift(3);

        Assert.AreEqual("123", s.Slice(3));
    }

    [TestMethod]
    public void Shift02()
    {
        var s = new StringSlicer("abc123");
        var s1 = s + 3;
        var s2 = s1 - 3;

        Assert.AreEqual("abc", s.Peek(3));
        Assert.AreEqual("123", s1.Peek(3));
        Assert.AreEqual("abc", s2.Peek(3));
    }

    [TestMethod]
    public void Slice01()
    {
        var s = new StringSlicer("abc123");
        Assert.AreEqual("abc", s.Slice(3));
        Assert.AreEqual("123", s.Slice(3));
    }

    [TestMethod]
    public void SliceAll01()
    {
        var s = new StringSlicer("abc123");
        Assert.AreEqual("abc123", s.SliceAll());
        Assert.AreEqual("", s.Peek(1));
    }
}
