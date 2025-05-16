namespace CarerExtensionTest.Extensions;

[TestClass]
public class IComparableExtensionTest
{
    #region Clamp
    [TestMethod]
    public void Clamp01()
    {
        {
            // in range.
            Assert.AreEqual(8, 8.Clamp(8, 10));
            Assert.AreEqual(10, 10.Clamp(8, 10));

            Assert.AreEqual(8, 8.Clamp(10, 8));
            Assert.AreEqual(10, 10.Clamp(10, 8));
        }
        {
            // out of range.
            Assert.AreEqual(8, 7.Clamp(8, 10));
            Assert.AreEqual(10, 11.Clamp(8, 10));

            Assert.AreEqual(8, 7.Clamp(10, 8));
            Assert.AreEqual(10, 11.Clamp(10, 8));
        }
    }

    [TestMethod]
    public void Clamp02()
    {
        {
            // in range.
            Assert.AreEqual("b", "b".Clamp("b", "d"));
            Assert.AreEqual("d", "d".Clamp("b", "d"));
        }
        {
            // out of range.
            Assert.AreEqual("b", "a".Clamp("b", "d"));
            Assert.AreEqual("d", "e".Clamp("b", "d"));
        }
    }
    #endregion

    #region IsBetween
    [TestMethod]
    public void IsBetween01()
    {
        {
            // in range.
            Assert.IsTrue(9.IsBetween(8, 10));
            Assert.IsTrue(10.IsBetween(8, 10));

            Assert.IsTrue(8.IsBetween(10, 8));
            Assert.IsTrue(10.IsBetween(10, 8));
        }
        {
            // out of range.
            Assert.IsFalse(7.IsBetween(8, 10));
            Assert.IsFalse(11.IsBetween(8, 10));

            Assert.IsFalse(7.IsBetween(10, 8));
            Assert.IsFalse(11.IsBetween(10, 8));
        }
    }

    [TestMethod]
    public void IsBetween02()
    {
        {
            // in range.
            Assert.IsTrue("b".IsBetween("b", "d"));
            Assert.IsTrue("d".IsBetween("b", "d"));
        }
        {
            // out of range.
            Assert.IsFalse("a".IsBetween("b", "d"));
            Assert.IsFalse("e".IsBetween("b", "d"));
        }
    }
    #endregion

    #region IsExclude
    [TestMethod]
    public void IsExclude01()
    {
        Assert.IsTrue(0.IsExclude([1, 2]));
        Assert.IsFalse(0.IsExclude([0, 1, 2]));
    }

    [TestMethod]
    public void IsExclude02()
    {
        Assert.IsTrue("ab".IsExclude(["aa", "bb"]));
        Assert.IsFalse("ab".IsExclude(["aa", "bb", "ab"]));
    }
    #endregion

    #region IsInclude
    [TestMethod]
    public void IsInclude01()
    {
        Assert.IsTrue(0.IsInclude([0, 1, 2]));
        Assert.IsFalse(0.IsInclude([1, 2]));
    }

    [TestMethod]
    public void IsInclude02()
    {
        Assert.IsTrue("ab".IsInclude(["aa", "bb", "ab"]));
        Assert.IsFalse("ab".IsInclude(["aa", "bb"]));

        string? s = null;
        Assert.IsTrue(s.IsInclude(["a", "b", null]));
    }
    #endregion
}
