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

    #region ToBoolean
    [TestMethod]
    public void ToBoolean01()
    {
        byte[] value = [0x01, 0x00, 0x80];
        Assert.IsTrue(value.ToBoolean(0));
        Assert.IsTrue(value.ToBoolean(2));
    }

    [TestMethod]
    public void ToBoolean02()
    {
        byte[] value = [0, 1, 0];
        Assert.IsFalse(value.ToBoolean(0));
        Assert.IsFalse(value.ToBoolean(2));
    }
    #endregion

    #region ToInt32
    [TestMethod]
    public void ToInt32_01()
    {
        {
            byte[] value = [1, 0, 0, 0];
            var excepted = 1;
            Assert.AreEqual(excepted, value.ToInt32(0));
        }
        {
            byte[] value = [0, 0, 0, 1];
            var expected = 1 << 24;
            Assert.AreEqual(expected, value.ToInt32(0));
        }
    }
    #endregion

    #region ToInt64
    [TestMethod]
    public void ToInt64_01()
    {
        {
            byte[] value = [1, 0, 0, 0, 0, 0, 0, 0];
            var excepted = 1L;
            Assert.AreEqual(excepted, value.ToInt64(0));
        }
        {
            byte[] value = [0, 0, 0, 0, 0, 0, 0, 1];
            var expected = 1L << 56;
            Assert.AreEqual(expected, value.ToInt64(0));
        }
    }
    #endregion
}
