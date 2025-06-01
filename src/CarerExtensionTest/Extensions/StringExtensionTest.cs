namespace CarerExtensionTest.Extensions;

[TestClass]
public class StringExtensionTest
{
    [TestMethod]
    public void At01()
    {
        Assert.AreEqual("a", "abc".At(0));
        Assert.AreEqual("c", "abc".At(2));
    }

    #region EndsWith
    [TestMethod]
    public void EndsWith01()
    {
        Assert.IsTrue("abc".EndsWith("b", "bc"));
        Assert.IsFalse("zzz".EndsWith("b", "bc"));
    }

    [TestMethod]
    public void EndsWith02()
    {
        Span<string> s = ["b", "bc"];
        Assert.IsTrue("abc".EndsWith(s));
        Assert.IsFalse("zzz".EndsWith(s));
    }
    #endregion

    [TestMethod]
    public void First01()
    {
        Assert.AreEqual("", "abc".First(0));
        Assert.AreEqual("a", "abc".First(1));
        Assert.AreEqual("abc", "abc".First(3));
    }

    [TestMethod]
    public void From01()
    {
        Assert.AreEqual("abc", "abc".From(0));
        Assert.AreEqual("bc", "abc".From(1));
        Assert.AreEqual("c", "abc".From(2));
        Assert.AreEqual("", "abc".From(3));

        Assert.AreEqual("c", "abc".From(^1));
        Assert.AreEqual("bc", "abc".From(^2));
        Assert.AreEqual("abc", "abc".From(^3));
    }

    #region Index
    [TestMethod]
    public void Index01()
    {
        var test = @"
aaa

  bbb";
        var expected = @"
  aaa

    bbb";
        Assert.AreEqual(expected, test.Indent(2));
    }

    [TestMethod]
    public void Index02()
    {
        var test = @"
aaa

  bbb";
        var expected = @"  
  aaa
  
    bbb";
        Assert.AreEqual(expected, test.Indent(2, indentEmptyLines: true));
    }
    #endregion

    [TestMethod]
    public void IsMatch01()
    {
        Assert.IsTrue("abc".IsMatch("^a"));
        Assert.IsFalse("zzz".IsMatch("^a"));
    }

    [TestMethod]
    public void IsNullOrEmpty01()
    {
        string? s = null;
        Assert.IsTrue(s.IsNullOrEmpty());
        Assert.IsTrue("".IsNullOrEmpty());
        Assert.IsFalse("a".IsNullOrEmpty());
    }

    [TestMethod]
    public void IsNullOrWhiteSpace01()
    {
        string? s = null;
        Assert.IsTrue(s.IsNullOrWhiteSpace());
        Assert.IsTrue("".IsNullOrWhiteSpace());
        Assert.IsTrue(" ".IsNullOrWhiteSpace());
        Assert.IsFalse("a".IsNullOrWhiteSpace());
    }

    [TestMethod]
    public void IsPresent01()
    {
        string? s = null;
        Assert.IsFalse(s.IsPresent());
        Assert.IsFalse("".IsPresent());
        Assert.IsTrue("a".IsPresent());
    }

    [TestMethod]
    public void Last01()
    {
        Assert.AreEqual("c", "abc".Last(1));
        Assert.AreEqual("abc", "abc".Last(3));
    }

    [TestMethod]
    public void Presence01()
    {
        string? s = null;
        Assert.IsNull(s.Presence());
        Assert.IsNull("".Presence());
        Assert.IsNotNull("a".Presence());
    }

    [TestMethod]
    public void Remove01()
    {
        Assert.AreEqual("bc", "abc".Remove("^a"));
    }

    #region SafetySubstring
    [TestMethod]
    public void SafetySubstring01()
    {
        Assert.AreEqual("bc", "abc".SafetySubstring(1));
        Assert.AreEqual("", "abc".SafetySubstring(3));

        // over array size.
        Assert.IsNull("abc".SafetySubstring(4));
    }

    [TestMethod]
    public void SafetySubstring02()
    {
        Assert.AreEqual("bc", "abc".SafetySubstring(1, 2));

        // over array size.
        Assert.AreEqual("bc", "abc".SafetySubstring(1, 3));
        Assert.AreEqual("", "abc".SafetySubstring(3, 1));
        Assert.IsNull("abc".SafetySubstring(4, 1));
    }
    #endregion

    [TestMethod]
    public void Squish01()
    {
        Assert.AreEqual("a", "   a".Squish());
        Assert.AreEqual("b", "b   ".Squish());
        Assert.AreEqual("a b", "a   b".Squish());
    }

    #region StartsWith
    [TestMethod]
    public void StartsWith01()
    {
        Assert.IsTrue("abc".StartsWith("ab", "b"));
        Assert.IsFalse("zzz".StartsWith("ab", "b"));
    }

    [TestMethod]
    public void StartsWith02()
    {
        Span<string> s = ["ab", "b"];
        Assert.IsTrue("abc".StartsWith(s));
        Assert.IsFalse("zzz".StartsWith(s));
    }
    #endregion

    #region StripHeredoc
    [TestMethod]
    public void StripHeredoc01()
    {
        var test = @"
      a
    bb
  
      ccc
        
";
        var expected = @"
  a
bb

  ccc
    
";
        Assert.AreEqual(expected, test.StripHeredoc());
    }

    [TestMethod]
    public void StripHeredoc02()
    {
        var test = @"
            a
        bb
    
            ccc
                
";
        var expected = @"
    a
bb

    ccc
        
";
        Assert.AreEqual(expected, test.StripHeredoc());
    }
    #endregion

    [TestMethod]
    public void To01()
    {
        Assert.AreEqual("a", "abc".To(0));
        Assert.AreEqual("ab", "abc".To(1));
        Assert.AreEqual("abc", "abc".To(2));

        Assert.AreEqual("abc", "abc".To(^1));
        Assert.AreEqual("ab", "abc".To(^2));
        Assert.AreEqual("a", "abc".To(^3));
    }

    [TestMethod]
    public void Trim01()
    {
        Assert.AreEqual(" a ", "  a  ".Trim(1));
        Assert.AreEqual("a", "  a  ".Trim(3));
    }

    [TestMethod]
    public void TrimEnd01()
    {
        Assert.AreEqual("  a ", "  a  ".TrimEnd(1));
        Assert.AreEqual("  a", "  a  ".TrimEnd(3));
    }

    [TestMethod]
    public void TrimStart01()
    {
        Assert.AreEqual(" a  ", "  a  ".TrimStart(1));
        Assert.AreEqual("a  ", "  a  ".TrimStart(3));
    }
}
