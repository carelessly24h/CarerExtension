namespace CarerExtensionTest.Utilities.DateTimeCalculator;

[TestClass]
public class DurationTest
{
    [TestMethod]
    public void Add01()
    {
        {
            var expected = 2.Days();
            var actual = 1.Days() + 1.Days();
            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            var expected = 1.Days();
            var actual = 2.Days() + (-1).Days();
            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
    }

    [TestMethod]
    public void Add02()
    {
        {
            var expected = 2.Seconds();
            var actual1 = 1.Seconds() + 1;
            var actual2 = 1 + 1.Seconds();
            Assert.AreEqual(expected.Ticks, actual1.Ticks);
            Assert.AreEqual(expected.Ticks, actual2.Ticks);
        }
        {
            var expected = 2.Seconds();
            var actual1 = 1.Seconds() + 1L;
            var actual2 = 1L + 1.Seconds();
            Assert.AreEqual(expected.Ticks, actual1.Ticks);
            Assert.AreEqual(expected.Ticks, actual2.Ticks);
        }
        {
            var expected = 2.Seconds();
            var actual1 = 1.Seconds() + 1.0;
            var actual2 = 1.0 + 1.Seconds();
            Assert.AreEqual(expected.Ticks, actual1.Ticks);
            Assert.AreEqual(expected.Ticks, actual2.Ticks);
        }
    }

    [TestMethod]
    public void Add03()
    {
        var d = new DateTime(2000, 1, 1);
        {
            Assert.AreEqual(new DateTime(2000, 1, 2), d + 1.Days());
            Assert.AreEqual(new DateTime(2000, 2, 1), d + 1.Months());
            Assert.AreEqual(new DateTime(2001, 1, 1), d + 1.Years());
        }
        {
            Assert.AreEqual(new DateTime(2000, 1, 2), 1.Days() + d);
            Assert.AreEqual(new DateTime(2000, 2, 1), 1.Months() + d);
            Assert.AreEqual(new DateTime(2001, 1, 1), 1.Years() + d);
        }
    }

    [TestMethod]
    public void Ago01()
    {
        var expected = DateTime.Now.AddDays(-1);
        var actual = 1.Days().Ago();
        Assert.AreEqual(expected.Ticks, actual.Ticks, 4000);
    }

    [TestMethod]
    public void Deconstruct01()
    {
        var d = 1.Years() + 2.Months() + 3.Days() + 4.Hours() + 5.Minutes() + 6.Seconds() + 7.Milliseconds();
        {
            var (year, month, day) = d;
            Assert.AreEqual(1, year);
            Assert.AreEqual(2, month);
            Assert.AreEqual(3, day);
        }
        {
            var (year, month, day, hour, minute, second) = d;
            Assert.AreEqual(1, year);
            Assert.AreEqual(2, month);
            Assert.AreEqual(3, day);
            Assert.AreEqual(4, hour);
            Assert.AreEqual(5, minute);
            Assert.AreEqual(6, second);
        }
        {
            var (year, month, day, hour, minute, second, milliSecond) = d;
            Assert.AreEqual(1, year);
            Assert.AreEqual(2, month);
            Assert.AreEqual(3, day);
            Assert.AreEqual(4, hour);
            Assert.AreEqual(5, minute);
            Assert.AreEqual(6, second);
            Assert.AreEqual(7, milliSecond);
        }
    }

    [TestMethod]
    public void Div01()
    {
        {
            var expected = 1.Days();
            var actual = 2.Days() / 2;
            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            var expected = (-1).Days();
            var actual = 2.Days() / -2;
            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            var expected = 1.Days();
            var actual = 2.Days() / 2L;
            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            var expected = 1.Days();
            var actual = 2.Days() / 2.0;
            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
    }

    [TestMethod]
    public void Equals01()
    {
        Assert.IsTrue(1.Days() == 24.Hours());
        Assert.IsTrue(1.Days().Equals(24.Hours()));
    }

    [TestMethod]
    public void Mul01()
    {
        {
            var expected = 2.Days();
            var actual = 1.Days() * 2;
            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            var expected = (-2).Days();
            var actual = -2 * 1.Days();
            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            var expected = 2.Days();
            var actual1 = 1.Days() * 2L;
            var actual2 = 2L * 1.Days();
            Assert.AreEqual(expected.Ticks, actual1.Ticks);
            Assert.AreEqual(expected.Ticks, actual2.Ticks);
        }
        {
            var expected = 2.Days();
            var actual1 = 1.Days() * 2.0;
            var actual2 = 2.0 * 1.Days();
            Assert.AreEqual(expected.Ticks, actual1.Ticks);
            Assert.AreEqual(expected.Ticks, actual2.Ticks);
        }
    }

    [TestMethod]
    public void Since01()
    {
        var expected = DateTime.Now.AddDays(1);
        var actual = 1.Days().Since();
        Assert.AreEqual(expected.Ticks, actual.Ticks, 4000);
    }

    [TestMethod]
    public void Sub01()
    {
        {
            var expected = 1.Days();
            var actual = 2.Days() - 1.Days();
            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            var expected = 2.Days();
            var actual = 1.Days() - (-1).Days();
            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
    }

    [TestMethod]
    public void Sub02()
    {
        {
            var expected = 1.Seconds();
            var actual1 = 2.Seconds() - 1;
            var actual2 = 2 - 1.Seconds();
            Assert.AreEqual(expected.Ticks, actual1.Ticks);
            Assert.AreEqual(expected.Ticks, actual2.Ticks);
        }
        {
            var expected = 1.Seconds();
            var actual1 = 2.Seconds() - 1L;
            var actual2 = 2L - 1.Seconds();
            Assert.AreEqual(expected.Ticks, actual1.Ticks);
            Assert.AreEqual(expected.Ticks, actual2.Ticks);
        }
        {
            var expected = 1.Seconds();
            var actual1 = 2.Seconds() - 1.0;
            var actual2 = 2.0 - 1.Seconds();
            Assert.AreEqual(expected.Ticks, actual1.Ticks);
            Assert.AreEqual(expected.Ticks, actual2.Ticks);
        }
    }

    [TestMethod]
    public void Sub03()
    {
        var d = new DateTime(2000, 2, 2);
        Assert.AreEqual(new DateTime(2000, 2, 1), d - 1.Days());
        Assert.AreEqual(new DateTime(2000, 1, 2), d - 1.Months());
        Assert.AreEqual(new DateTime(1999, 2, 2), d - 1.Years());
    }
}
