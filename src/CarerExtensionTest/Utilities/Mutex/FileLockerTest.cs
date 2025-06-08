using CsvHelper.Configuration;

namespace CarerExtensionTest.Utilities.Mutex;

[TestClass]
public class FileLockerTest
{
    private const string RootDir = @"test\mutex_test";

    [ClassInitialize]
    public static void Initialize(TestContext _)
    {
        Directory.CreateDirectory(RootDir);
    }

    [TestMethod]
    public void Dispose01()
    {
        var dir = $@"{RootDir}\dispose1";
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
        var dir = $@"{RootDir}\dispose2";
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
        var dir = $@"{RootDir}\lock1";
        var lockFile = $@"{dir}\lockFile";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        using var mutex = FileLocker.Lock(lockFile);
        Assert.ThrowsExactly<FileLockException>(() => FileLocker.Lock(lockFile));
    }

    [TestMethod]
    public void Lock02()
    {
        var dir = $@"{RootDir}\lock2";
        var lockFile = $@"{dir}\lockFile";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        File.WriteAllText(lockFile, "");
        using var mutex = FileLocker.Lock(lockFile);
    }
}
