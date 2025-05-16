﻿namespace CarerExtensionTest.Extensions;

[TestClass]
public class RegexExtensionTest
{
    [TestMethod]
    [SuppressMessage("Performance", "SYSLIB1045:'GeneratedRegexAttribute' に変換します。", Justification = "<保留中>")]
    public void IsMultiLine01()
    {
        {
            var r = new Regex(".", RegexOptions.Multiline);
            Assert.IsTrue(r.IsMultiLine());
        }
        {
            var r = new Regex(".");
            Assert.IsFalse(r.IsMultiLine());
        }
    }
}
