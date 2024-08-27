namespace CarerExtensionTest;

[TestClass]
public class AssemblyRoutine
{
    [AssemblyInitialize]
    public static void Initialize(TestContext _)
    {
        // This is a temporary-folder for testing.
        // It is used for temporary-file I/O.
        Directory.CreateDirectory("test");
    }

    [AssemblyCleanup]
    public static void Cleanup()
    {
        Directory.Delete("test", recursive: true);
    }
}
