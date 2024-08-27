namespace CarerExtensionTest.Extensions;

[TestClass]
public class DateTimeExtensionTest
{
    #region Advance
    [TestMethod]
    public void Advance01()
    {
        // add DateTime.
        var d = new DateTime(2001, 2, 3, 10, 11, 12, 13);

        {
            var expected = d.AddYears(1);
            var actual = d.Advance(years: 1);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            var expected = d.AddMonths(2);
            var actual = d.Advance(months: 2);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            var expected = d.AddDays(14);
            var actual = d.Advance(weeks: 2);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            var expected = d.AddDays(3);
            var actual = d.Advance(days: 3);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            var expected = d.AddHours(4);
            var actual = d.Advance(hours: 4);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            var expected = d.AddMinutes(5);
            var actual = d.Advance(minutes: 5);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            var expected = d.AddSeconds(6);
            var actual = d.Advance(seconds: 6);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            var expected = d.AddMilliseconds(7);
            var actual = d.Advance(milliseconds: 7);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
    }

    [TestMethod]
    public void Advance02()
    {
        // subtract DateTime.
        var d = new DateTime(2001, 2, 3, 10, 11, 12, 13);

        {
            var expected = d.AddYears(-1);
            var actual = d.Advance(years: -1);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            var expected = d.AddMonths(-2);
            var actual = d.Advance(months: -2);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            var expected = d.AddDays(-14);
            var actual = d.Advance(weeks: -2);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            var expected = d.AddDays(-3);
            var actual = d.Advance(days: -3);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            var expected = d.AddHours(-4);
            var actual = d.Advance(hours: -4);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            var expected = d.AddMinutes(-5);
            var actual = d.Advance(minutes: -5);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            var expected = d.AddSeconds(-6);
            var actual = d.Advance(seconds: -6);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            var expected = d.AddMilliseconds(-7);
            var actual = d.Advance(milliseconds: -7);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
    }
    #endregion

    [TestMethod]
    public void Ago01()
    {
        var d = new DateTime(2001, 2, 3, 11, 12, 13, 14);

        var expected = d.AddSeconds(-14);
        var actual = d.Ago(14);

        Assert.AreEqual(expected.Ticks, actual.Ticks);
    }

    [TestMethod]
    public void BeginningOfDay01()
    {
        var d = new DateTime(2001, 2, 3, 11, 12, 13, 14);

        // 2001/2/3 11:12 -> 2001/2/3 00:00
        var expected = d.BeginningOfDay();
        var actual = new DateTime(2001, 2, 3);

        Assert.AreEqual(expected.Ticks, actual.Ticks);
    }

    [TestMethod]
    public void BeginningOfHour01()
    {
        var d = new DateTime(2001, 2, 3, 11, 12, 13, 14);

        // 2001/2/3 11:12 -> 2001/2/3 11:00
        var expected = d.BeginningOfHour();
        var actual = new DateTime(2001, 2, 3, 11, 0, 0);

        Assert.AreEqual(expected.Ticks, actual.Ticks);
    }

    [TestMethod]
    public void BeginningOfMonth01()
    {
        var d = new DateTime(2001, 2, 3, 11, 12, 13, 14);

        // 2001/2/3 -> 2001/2/1
        var expected = d.BeginningOfMonth();
        var actual = new DateTime(2001, 2, 1);

        Assert.AreEqual(expected.Ticks, actual.Ticks);
    }

    [TestMethod]
    public void BeginningOfQuarter01()
    {
        {
            var d = new DateTime(2001, 3, 31, 11, 12, 13, 14);

            // 2001/3/31 -> 2001/1/1
            var expected = d.BeginningOfQuarter();
            var actual = new DateTime(2001, 1, 1);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            var d = new DateTime(2001, 12, 31, 11, 12, 13, 14);

            // 2001/12/31 -> 2001/9/1
            var expected = d.BeginningOfQuarter();
            var actual = new DateTime(2001, 10, 1);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
    }

    #region BeginningOfWeek
    [TestMethod]
    public void BeginningOfWeek01()
    {
        {
            // Monday
            var d = new DateTime(2001, 2, 5, 11, 12, 13, 14);

            // 2001/2/5 11:12 -> 2001/2/5
            var expected = d.BeginningOfWeek(DayOfWeek.Monday);
            var actual = new DateTime(2001, 2, 5);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            // Sunday
            var d = new DateTime(2001, 2, 11, 11, 12, 13, 14);

            // 2001/2/11 -> 2001/2/5
            var expected = d.BeginningOfWeek(DayOfWeek.Monday);
            var actual = new DateTime(2001, 2, 5);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
    }

    [TestMethod]
    public void BeginningOfWeek02()
    {
        {
            // Sunday
            var d = new DateTime(2001, 2, 4, 11, 12, 13, 14);

            // 2001/2/4 11:12 -> 2001/2/4
            var expected = d.BeginningOfWeek(DayOfWeek.Sunday);
            var actual = new DateTime(2001, 2, 4);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            // Saturday
            var d = new DateTime(2001, 2, 10, 11, 12, 13, 14);

            // 2001/2/10 -> 2001/2/4
            var expected = d.BeginningOfWeek(DayOfWeek.Sunday);
            var actual = new DateTime(2001, 2, 4);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
    }
    #endregion

    [TestMethod]
    public void BeginningOfYear01()
    {
        var d = new DateTime(2001, 2, 3, 11, 12, 13, 14);

        // 2001/2/3 -> 2001/2/1
        var expected = d.BeginningOfYear();
        var actual = new DateTime(2001, 1, 1);

        Assert.AreEqual(expected.Ticks, actual.Ticks);
    }

    [TestMethod]
    public void Change01()
    {
        var d = new DateTime(2001, 2, 3);

        var expected = d.Change(year: 2002, month: 3, day: 4, hour: 11, minute: 12, second: 13, millisecond: 14);
        var actual = new DateTime(2002, 3, 4, 11, 12, 13, 14);

        Assert.AreEqual(expected.Ticks, actual.Ticks);
    }

    [TestMethod]
    public void DaysInMonth01()
    {
        {
            var d = new DateTime(2001, 2, 1);
            Assert.AreEqual(d.DaysInMonth(), 28);
        }
        {
            // leap year.
            var d = new DateTime(2004, 2, 1);
            Assert.AreEqual(d.DaysInMonth(), 29);
        }
    }

    [TestMethod]
    public void Deconstruct01()
    {
        var d = new DateTime(2001, 2, 3, 10, 11, 12, 13);

        {
            var (year, month, day, hour, minute, second) = d;
            Assert.AreEqual(2001, year);
            Assert.AreEqual(2, month);
            Assert.AreEqual(3, day);
            Assert.AreEqual(10, hour);
            Assert.AreEqual(11, minute);
            Assert.AreEqual(12, second);
        }
        {
            var (_, _, _, _, _, _, millisecond) = d;
            Assert.AreEqual(13, millisecond);
        }
    }

    [TestMethod]
    public void EndOfDay01()
    {
        var d = new DateTime(2001, 2, 3);

        var expected = d.EndOfDay();
        var actual = new DateTime(2001, 2, 3, 23, 59, 59, 999);

        Assert.AreEqual(expected.Ticks, actual.Ticks);
    }

    [TestMethod]
    public void EndOfHour01()
    {
        var d = new DateTime(2001, 2, 3, 11, 0, 0);

        var expected = d.EndOfHour();
        var actual = new DateTime(2001, 2, 3, 11, 59, 59, 999);

        Assert.AreEqual(expected.Ticks, actual.Ticks);
    }

    [TestMethod]
    public void EndOfMinute01()
    {
        var d = new DateTime(2001, 2, 3, 11, 12, 0);

        var expected = d.EndOfMinute();
        var actual = new DateTime(2001, 2, 3, 11, 12, 59, 999);

        Assert.AreEqual(expected.Ticks, actual.Ticks);
    }

    [TestMethod]
    public void EndOfMonth01()
    {
        {
            var d = new DateTime(2001, 2, 3);

            var expected = d.EndOfMonth();
            var actual = new DateTime(2001, 2, 28, 23, 59, 59, 999);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            // leap year.
            var d = new DateTime(2004, 2, 3);

            var expected = d.EndOfMonth();
            var actual = new DateTime(2004, 2, 29, 23, 59, 59, 999);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
    }

    [TestMethod]
    public void EndOfQuarter01()
    {
        {
            var d = new DateTime(2001, 1, 1);

            var expected = d.EndOfQuarter();
            var actual = new DateTime(2001, 3, 31, 23, 59, 59, 999);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            var d = new DateTime(2001, 10, 1);

            var expected = d.EndOfQuarter();
            var actual = new DateTime(2001, 12, 31, 23, 59, 59, 999);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
    }

    #region EndOfWeek
    [TestMethod]
    public void EndOfWeek01()
    {
        {
            // Monday
            var d = new DateTime(2001, 2, 5);

            // 2001/2/5 -> 2001/2/11 23:59
            var expected = d.EndOfWeek(DayOfWeek.Monday);
            var actual = new DateTime(2001, 2, 11, 23, 59, 59, 999);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            // Sunday
            var d = new DateTime(2001, 2, 11);

            // 2001/2/11 -> 2001/2/11 23:59
            var expected = d.EndOfWeek(DayOfWeek.Monday);
            var actual = new DateTime(2001, 2, 11, 23, 59, 59, 999);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
    }

    [TestMethod]
    public void EndOfWeek02()
    {
        {
            // Sunday
            var d = new DateTime(2001, 2, 4);

            // 2001/2/4 -> 2001/2/10 23:59
            var expected = d.EndOfWeek(DayOfWeek.Sunday);
            var actual = new DateTime(2001, 2, 10, 23, 59, 59, 999);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
        {
            // Saturday
            var d = new DateTime(2001, 2, 10);

            // 2001/2/10 -> 2001/2/10 23:59
            var expected = d.EndOfWeek(DayOfWeek.Sunday);
            var actual = new DateTime(2001, 2, 10, 23, 59, 59, 999);

            Assert.AreEqual(expected.Ticks, actual.Ticks);
        }
    }
    #endregion

    [TestMethod]
    public void EndOfYear01()
    {
        var d = new DateTime(2001, 2, 3);

        var expected = d.EndOfYear();
        var actual = new DateTime(2001, 12, 31, 23, 59, 59, 999);

        Assert.AreEqual(expected.Ticks, actual.Ticks);
    }

    [TestMethod]
    public void MonthsAgo01()
    {
        var d = new DateTime(2001, 2, 3, 11, 12, 13, 14);

        var expected = d.MonthsAgo(2);
        var actual = new DateTime(2000, 12, 3, 11, 12, 13, 14);

        Assert.AreEqual(expected.Ticks, actual.Ticks);
    }

    [TestMethod]
    public void MonthsSince01()
    {
        var d = new DateTime(2000, 12, 3, 11, 12, 13, 14);

        var expected = d.MonthsSince(2);
        var actual = new DateTime(2001, 2, 3, 11, 12, 13, 14);

        Assert.AreEqual(expected.Ticks, actual.Ticks);
    }

    [TestMethod]
    public void NextDay01()
    {
        var d = new DateTime(2001, 1, 2, 11, 12, 13, 14);

        var expected = d.AddDays(3);
        var actual = d.NextDay(3);

        Assert.AreEqual(expected.Ticks, actual.Ticks);
    }

    [TestMethod]
    public void PreviousDay01()
    {
        var d = new DateTime(2001, 1, 2, 11, 12, 13, 14);

        var expected = d.AddDays(-3);
        var actual = d.PreviousDay(3);

        Assert.AreEqual(expected.Ticks, actual.Ticks);
    }

    [TestMethod]
    public void Since01()
    {
        var d = new DateTime(2001, 2, 3, 11, 12, 13, 14);

        var expected = d.AddSeconds(5);
        var actual = d.Since(5);

        Assert.AreEqual(expected.Ticks, actual.Ticks);
    }

    [TestMethod]
    public void WeeksAgo01()
    {
        var d = new DateTime(2001, 2, 3, 11, 12, 13, 14);

        var expected = d.AddDays(-14);
        var actual = d.WeeksAgo(2);

        Assert.AreEqual(expected.Ticks, actual.Ticks);
    }

    [TestMethod]
    public void WeeksSince01()
    {
        var d = new DateTime(2001, 2, 3, 11, 12, 13, 14);

        var expected = d.AddDays(14);
        var actual = d.WeeksSince(2);

        Assert.AreEqual(expected.Ticks, actual.Ticks);
    }

    [TestMethod]
    public void YearsAgo01()
    {
        var d = new DateTime(2001, 2, 3, 11, 12, 13, 14);

        var expected = d.AddYears(-2);
        var actual = d.YearsAgo(2);

        Assert.AreEqual(expected.Ticks, actual.Ticks);
    }

    [TestMethod]
    public void YearsSince01()
    {
        var d = new DateTime(2001, 2, 3, 11, 12, 13, 14);

        var expected = d.AddYears(2);
        var actual = d.YearsSince(2);

        Assert.AreEqual(expected.Ticks, actual.Ticks);
    }
}
