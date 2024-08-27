namespace CarerExtensionTest.Utilities.LegacyDataFormatter;

[TestClass]
public class ByteArraySlicerTest
{
    [TestMethod]
    public void IsEmpty01()
    {
        var s = new ByteArraySlicer([0, 0]);

        Assert.IsFalse(s.IsEmpty);
        s.Shift(2);
        Assert.IsTrue(s.IsEmpty);
    }

    [TestMethod]
    public void IsPresent01()
    {
        var s = new ByteArraySlicer([0, 0]);

        Assert.IsTrue(s.IsPresent);
        s.Shift(2);
        Assert.IsFalse(s.IsPresent);
    }

    [TestMethod]
    public void Peek01()
    {
        var s = new ByteArraySlicer([0, 1, 2]);
        {
            var actual = s.Peek(2);

            Assert.AreEqual(2, actual.Count());
            Assert.AreEqual(0, actual.ElementAt(0));
            Assert.AreEqual(1, actual.ElementAt(1));
        }
        {
            var actual = s.Peek(2);

            Assert.AreEqual(2, actual.Count());
            Assert.AreEqual(0, actual.ElementAt(0));
        }
    }

    [TestMethod]
    public void PeekAll01()
    {
        var s = new ByteArraySlicer([0, 1, 2]);
        {
            var actual = s.PeekAll();

            Assert.AreEqual(3, actual.Count());
            Assert.AreEqual(0, actual.ElementAt(0));
            Assert.AreEqual(2, actual.ElementAt(2));
        }
        {
            var actual = s.PeekAll();

            Assert.AreEqual(3, actual.Count());
            Assert.AreEqual(0, actual.ElementAt(0));
        }
    }

    [TestMethod]
    public void Refresh01()
    {
        var s = new ByteArraySlicer([0, 1]);
        s.Shift(10);

        Assert.IsTrue(s.IsEmpty);
        s.Refresh();

        Assert.AreEqual(0, s.Peek(1).ElementAt(0));
    }

    [TestMethod]
    public void Shift01()
    {
        var s = new ByteArraySlicer([0, 1]);
        s.Shift(1);

        Assert.AreEqual(1, s.Peek(1).ElementAt(0));
    }

    [TestMethod]
    public void Shift02()
    {
        var s = new ByteArraySlicer([0, 1, 2]);
        s.Shift(10);

        Assert.IsTrue(s.IsEmpty);
    }

    [TestMethod]
    public void Shift03()
    {
        var s = new ByteArraySlicer([0, 1, 2]);
        var s1 = s + 1;
        var s2 = s1 - 1;

        Assert.AreEqual(0, s.Peek(1).ElementAt(0));
        Assert.AreEqual(1, s1.Peek(1).ElementAt(0));
        Assert.AreEqual(0, s2.Peek(1).ElementAt(0));
    }

    [TestMethod]
    public void Slice01()
    {
        var s = new ByteArraySlicer([0, 1, 2, 3]);
        {
            var actual = s.Slice(2);

            Assert.AreEqual(2, actual.Count());
            Assert.AreEqual(0, actual.ElementAt(0));
            Assert.AreEqual(1, actual.ElementAt(1));
        }
        {
            var actual = s.Slice(2);

            Assert.AreEqual(2, actual.Count());
            Assert.AreEqual(2, actual.ElementAt(0));
            Assert.AreEqual(3, actual.ElementAt(1));
        }
        {
            var actual = s.Slice(2);
            Assert.AreEqual(0, actual.Count());
        }
    }

    [TestMethod]
    public void SliceAll01()
    {
        var s = new ByteArraySlicer([0, 1, 2]);
        {
            var actual = s.SliceAll();

            Assert.AreEqual(3, actual.Count());
            Assert.AreEqual(0, actual.ElementAt(0));
            Assert.AreEqual(2, actual.ElementAt(2));
        }
        {
            var actual = s.SliceAll();
            Assert.AreEqual(0, actual.Count());
        }
    }
}
