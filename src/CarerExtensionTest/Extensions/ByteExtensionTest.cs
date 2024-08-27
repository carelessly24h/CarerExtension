namespace CarerExtensionTest.Extensions;

[TestClass]
public class ByteExtensionTest
{
    [TestMethod]
    public void All01()
    {
        byte b1 = 0b_1111_1111;
        Assert.IsTrue(b1.All());

        byte b2 = 0b_0111_1111;
        Assert.IsFalse(b2.All());
    }

    [TestMethod]
    public void All02()
    {
        byte b = 0b_0011_1100;

        Assert.IsTrue(b.All(0b_0000_1100));
        Assert.IsTrue(b.All(0b_0011_1100));

        Assert.IsFalse(b.All(0b_1111_0000));
    }

    [TestMethod]
    public void Bit01()
    {
        byte b1 = 0b_0000_0001;
        Assert.IsTrue(b1.Bit(0));
        Assert.IsFalse(b1.Bit(1));

        byte b2 = 0b_0001_0000;
        Assert.IsTrue(b2.Bit(4));
        Assert.IsFalse(b2.Bit(5));
    }
}
