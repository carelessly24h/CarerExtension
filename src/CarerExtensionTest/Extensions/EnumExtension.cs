// different from Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute;
using EnumDescription = System.ComponentModel.DescriptionAttribute;

namespace CarerExtensionTest.Extensions;

[TestClass]
public class EnumExtension
{
    enum TestEnum
    {
        [EnumDescription("Description of Value1")]
        Value1,
        [EnumDescription("Description of Value2")]
        Value2,
        [EnumDescription("Description of Value3")]
        Value3,
    }

    [TestMethod]
    public void GetDescription01()
    {
        {
            var d = TestEnum.Value1.GetDescription();
            Assert.AreEqual("Description of Value1", d);
        }
        {
            var d = TestEnum.Value1.GetDescription();
            Assert.AreEqual("Description of Value1", d);
        }
    }
}
