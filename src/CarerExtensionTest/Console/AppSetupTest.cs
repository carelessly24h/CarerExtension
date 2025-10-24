namespace CarerExtensionTest.Console;

[TestClass]
public class AppSetupTest
{
    private static AppSettings appSettings = null!;

    private static CarerDbContext dbContext = null!;

    #region Initialize/Cleanup
    [ClassInitialize]
    public static void ClassInit(TestContext context)
    {
        // set Development mode.
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Development");

        var jsonPath = @"Configs\appsettings.Development.json";
        appSettings = AppSettings.Read(jsonPath);
        dbContext = new CarerDbContext(appSettings);
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
    }
    #endregion

    [TestMethod]
    public void RunTest()
    {
        var path = Assembly.GetExecutingAssembly().Location;
        AppSetup.Run([]);

        var corporations = dbContext.Corporations;
        Assert.AreEqual(3, corporations.Count());

        {
            var (id, name) = corporations.ElementAt(0);
            Assert.AreEqual(1, id);
            Assert.AreEqual("CORP1", name);
        }

        var users = dbContext.Users;
        Assert.AreEqual(3, users.Count());

        {
            var (id, corporationId, name, age) = users.ElementAt(0);
            Assert.AreEqual(1, id);
            Assert.AreEqual(1, corporationId);
            Assert.AreEqual("USER1", name);
            Assert.AreEqual(10, age);
        }
    }
}
