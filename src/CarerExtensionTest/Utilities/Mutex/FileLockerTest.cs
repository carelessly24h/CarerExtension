namespace CarerExtensionTest.Utilities.Mutex;

[TestClass]
public class FileLockerTest
{
    private const string ROOT_DIR = @"test\mutex_test";

    [ClassInitialize]
    public static void Initialize(TestContext _)
    {
        Directory.CreateDirectory(ROOT_DIR);
    }

    [TestMethod]
    public void Dispose01()
    {
        var dir = $@"{ROOT_DIR}\dispose1";
        var lockFile = $@"{dir}\lockFile";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        using (var mutex = FileLocker.Lock(lockFile))
        {
            Assert.IsTrue(File.Exists(lockFile));
        }
        Assert.IsFalse(File.Exists(lockFile));
    }

    [TestMethod]
    public void Dispose02()
    {
        var dir = $@"{ROOT_DIR}\dispose2";
        var lockFile = $@"{dir}\lockFile";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        var mutex = FileLocker.Lock(lockFile);
        try
        {
            Assert.IsTrue(File.Exists(lockFile));
        }
        finally
        {
            mutex.Dispose();
            Assert.IsFalse(File.Exists(lockFile));
        }
    }

    [TestMethod]
    public void Lock01()
    {
        var dir = $@"{ROOT_DIR}\lock1";
        var lockFile = $@"{dir}\lockFile";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        using var mutex = FileLocker.Lock(lockFile);
        Assert.ThrowsExactly<FileLockException>(() => FileLocker.Lock(lockFile));
    }
}
