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
            Assert.AreEqual(9, 9.Clamp(9, 11));
            Assert.AreEqual(11, 11.Clamp(9, 11));
        }
        {
            // out of range.
            Assert.AreEqual(9, 8.Clamp(9, 11));
            Assert.AreEqual(11, 12.Clamp(9, 11));
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
            Assert.IsTrue(9.IsBetween(9, 11));
            Assert.IsTrue(11.IsBetween(9, 11));
        }
        {
            // out of range.
            Assert.IsFalse(8.IsBetween(9, 11));
            Assert.IsFalse(12.IsBetween(9, 11));
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
        string? s = null;
    var r=    s.IsInclude(["a", "b", null]);

        Assert.IsTrue("ab".IsInclude(["aa", "bb", "ab"]));
        Assert.IsFalse("ab".IsInclude(["aa", "bb"]));
    }
    #endregion
}
