namespace CarerExtensionTest.Extensions;

[TestClass]
public class ArrayExtensionTest
{
    #region CopyFrom
    [TestMethod]
    public void CopyFrom01()
    {
        int[] source = [10, 20, 30];
        int[] result = [0, 0, 1];

        {
            // int type arg.
            result.CopyFrom(source, 2);

            // change.
            Assert.AreEqual(10, result[0]);
            Assert.AreEqual(20, result[1]);
            // no change.
            Assert.AreEqual(1, result[2]);
        }
        {
            // long type arg.
            result.CopyFrom(source, 2L);

            // change.
            Assert.AreEqual(10, result[0]);
            Assert.AreEqual(20, result[1]);
            // no change.
            Assert.AreEqual(1, result[2]);
        }
    }

    [TestMethod]
    public void CopyFrom02()
    {
        string[] source = ["a", "b", "c"];
        string?[] result = [null, null, null];

        {
            // int type arg.
            result.CopyFrom(source, 2);

            // changed.
            Assert.AreEqual("a", result[0]);
            Assert.AreEqual("b", result[1]);
            // no change.
            Assert.IsNull(result[2]);
        }
        {
            // long type arg.
            result.CopyFrom(source, 2L);

            // changed.
            Assert.AreEqual("a", result[0]);
            Assert.AreEqual("b", result[1]);
            // no change.
            Assert.IsNull(result[2]);
        }
    }

    [TestMethod]
    public void CopyFrom03()
    {
        int[] source = [10, 20, 30, 40];
        int[] result = [1, 0, 0, 2];

        {
            // int type arg.
            result.CopyFrom(source, 1, 1, 2);

            // changed.
            Assert.AreEqual(20, result[1]);
            Assert.AreEqual(30, result[2]);
            // no change.
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[3]);
        }
        {
            // long type arg.
            result.CopyFrom(source, 1L, 1L, 2L);

            // changed.
            Assert.AreEqual(20, result[1]);
            Assert.AreEqual(30, result[2]);
            // no change.
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[3]);
        }
    }
    #endregion
}
