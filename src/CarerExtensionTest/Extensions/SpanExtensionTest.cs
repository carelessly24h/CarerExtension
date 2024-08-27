namespace CarerExtensionTest.Extensions;

[TestClass]
public class SpanExtensionTest
{
    #region All
    [TestMethod]
    public void All01()
    {
        Span<int> arr = [1, 2, 3, 4, 5];

        Assert.IsTrue(arr.All(a => a < 10));
        Assert.IsFalse(arr.All(a => a < 5));
    }

    [TestMethod]
    public void All02()
    {
        ReadOnlySpan<int> arr = [1, 2, 3, 4, 5];

        Assert.IsTrue(arr.All(a => a < 10));
        Assert.IsFalse(arr.All(a => a < 5));
    }
    #endregion

    #region Any
    [TestMethod]
    public void Any01()
    {
        Span<int> arr = [1, 2, 3, 4, 5];

        Assert.IsTrue(arr.Any(a => a == 1));
        Assert.IsFalse(arr.All(a => a == 0));
    }

    [TestMethod]
    public void Any02()
    {
        ReadOnlySpan<int> arr = [1, 2, 3, 4, 5];

        Assert.IsTrue(arr.Any(a => a == 1));
        Assert.IsFalse(arr.All(a => a == 0));
    }
    #endregion

    #region SafetySlice
    [TestMethod]
    public void SafetySlice01()
    {
        Span<int> arr = [1, 2, 3, 4, 5];
        {
            var results = arr.SafetySlice(2);

            Assert.AreEqual(3, results.Length);
            Assert.AreEqual(3, results[0]);
            Assert.AreEqual(5, results[2]);
        }
        {
            var results = arr.SafetySlice(5);

            Assert.AreEqual(0, results.Length);
        }
    }

    [TestMethod]
    public void SafetySlice02()
    {
        ReadOnlySpan<int> arr = [1, 2, 3, 4, 5];
        var results = arr.SafetySlice(2);

        Assert.AreEqual(3, results.Length);
        Assert.AreEqual(3, results[0]);
        Assert.AreEqual(5, results[2]);
    }

    [TestMethod]
    public void SafetySlice03()
    {
        Span<int> arr = [1, 2, 3, 4, 5];

        {
            var results = arr.SafetySlice(1, 3);

            Assert.AreEqual(3, results.Length);
            Assert.AreEqual(2, results[0]);
            Assert.AreEqual(4, results[2]);
        }
        {
            var results = arr.SafetySlice(3, 5);

            Assert.AreEqual(2, results.Length);
            Assert.AreEqual(4, results[0]);
            Assert.AreEqual(5, results[1]);
        }
        {
            var results = arr.SafetySlice(5, 5);

            Assert.AreEqual(0, results.Length);
        }
    }

    [TestMethod]
    public void SafetySlice04()
    {
        ReadOnlySpan<int> arr = [1, 2, 3, 4, 5];
        var results = arr.SafetySlice(1, 3);

        Assert.AreEqual(3, results.Length);
        Assert.AreEqual(2, results[0]);
        Assert.AreEqual(4, results[2]);
    }
    #endregion
}
