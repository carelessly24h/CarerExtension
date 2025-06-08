namespace CarerExtensionTest.Utilities.Compression.Zip;

[TestClass]
public class MemoryArchiverTest
{
    private const string TestFile = @"IO\TestFiles\TestZipFile.zip";
    private const string RootDir = @"test\compression\zip";

    private const string EmptyFolderPath1 = $@"{RootDir}\add_empty_folder1";
    private const string TextFilePath1 = $@"{RootDir}\add_text_file1.txt";
    private const string TextFilePath2 = $@"{RootDir}\add_text_file2.txt";

    [ClassInitialize]
    public static void Initialize(TestContext _)
    {
        Directory.CreateDirectory(RootDir);

        Directory.CreateDirectory(EmptyFolderPath1);
        File.WriteAllText(TextFilePath1, "Text File no.1");
        File.WriteAllText(TextFilePath2, "Text File no.2");
    }

    [TestMethod]
    public void AddEntries01()
    {
        var dir = $@"{RootDir}\add_entries1";
        var writeFile = $@"{dir}\write.zip";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        using var archiver = new MemoryArchiver(TestFile);
        archiver.AddEntry(TextFilePath1, Path.GetFileName(TextFilePath1));

        var bin = archiver.GetBytes();
        File.WriteAllBytes(writeFile, bin);

        #region validate
        ZipFile.ExtractToDirectory(writeFile, dir);
        Assert.IsTrue(File.Exists($@"{dir}\text_file1.txt"));
        Assert.IsTrue(File.Exists($@"{dir}\zip_folder\text_file2.txt"));
        Assert.IsTrue(File.Exists($@"{dir}\zip_folder\text_file3"));
        Assert.IsTrue(Directory.Exists($@"{dir}\empty_folder"));

        Assert.IsTrue(File.Exists($@"{dir}\add_text_file1.txt"));
        #endregion
    }

    [TestMethod]
    public void AddEntries02()
    {
        var dir = $@"{RootDir}\add_entries2";
        var writeFile = $@"{dir}\write.zip";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        using var archiver = new MemoryArchiver(TestFile);
        archiver.AddEntry(TextFilePath1, Path.GetFileName(TextFilePath1));
        archiver.AddEntry(EmptyFolderPath1, $"{Path.GetFileName(EmptyFolderPath1)}/");

        var bin = archiver.GetBytes();
        File.WriteAllBytes(writeFile, bin);

        #region validate
        ZipFile.ExtractToDirectory(writeFile, dir);
        Assert.IsTrue(File.Exists($@"{dir}\add_text_file1.txt"));
        Assert.IsTrue(Directory.Exists($@"{dir}\add_empty_folder1"));
        #endregion
    }

    [TestMethod]
    public void ChangeComment01()
    {
        using var archiver = new MemoryArchiver(TestFile);
        var oldComment = archiver.GetComment();

        archiver.ChangeComment("changed_comment");
        var newComment = archiver.GetComment();

        Assert.AreNotEqual(oldComment, newComment);
        Assert.AreEqual("changed_comment", newComment);
    }

    [TestMethod]
    public void Compress01()
    {
        var dir = $@"{RootDir}\compress1";
        var writeFile = $@"{dir}\write.zip";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        var buffer = MemoryArchiver.Compress(TextFilePath1);
        File.WriteAllBytes(writeFile, buffer);

        #region validate
        ZipFile.ExtractToDirectory(writeFile, dir);
        Assert.IsTrue(File.Exists($@"{dir}\add_text_file1.txt"));
        #endregion
    }

    [TestMethod]
    public void Compress02()
    {
        var dir = $@"{RootDir}\compress2";
        var writeFile = $@"{dir}\write.zip";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        string[] files = [
            TextFilePath1,
            EmptyFolderPath1,
        ];
        var buffer = MemoryArchiver.Compress(files);
        File.WriteAllBytes(writeFile, buffer);

        #region validate
        ZipFile.ExtractToDirectory(writeFile, dir);
        Assert.IsTrue(File.Exists($@"{dir}\add_text_file1.txt"));
        Assert.IsTrue(Directory.Exists($@"{dir}\add_empty_folder1"));
        #endregion
    }

    [TestMethod]
    public void Decompress01()
    {
        var dir = $@"{RootDir}\decompress1";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        var files = MemoryArchiver.Decompress(TestFile);
        foreach (var file in files)
        {
            if (file.IsDirectory)
            {
                Directory.CreateDirectory(@$"{dir}\{file.EntryPath}");
            }
            else
            {
                File.WriteAllBytes(@$"{dir}\{file.EntryPath}", file.ReadFromCache());
            }
        }

        #region validate
        Assert.IsTrue(File.Exists($@"{dir}\text_file1.txt"));
        Assert.IsTrue(File.Exists($@"{dir}\zip_folder\text_file2.txt"));
        Assert.IsTrue(File.Exists($@"{dir}\zip_folder\text_file3"));
        Assert.IsTrue(Directory.Exists($@"{dir}\empty_folder"));
        #endregion
    }

    [TestMethod]
    public void RemoveEntries01()
    {
        var dir = $@"{RootDir}\remove_entries1";
        var writeFile = $@"{dir}\write.zip";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        using var archiver = new MemoryArchiver(TestFile);
        archiver.RemoveEntry("zip_folder/text_file2.txt");

        var bin = archiver.GetBytes();
        File.WriteAllBytes(writeFile, bin);

        #region validate
        ZipFile.ExtractToDirectory(writeFile, dir);
        Assert.IsTrue(File.Exists($@"{dir}\text_file1.txt"));
        Assert.IsTrue(File.Exists($@"{dir}\zip_folder\text_file3"));
        Assert.IsTrue(Directory.Exists($@"{dir}\empty_folder"));

        Assert.IsFalse(File.Exists($@"{dir}\zip_folder\text_file2.txt"));
        #endregion
    }

    [TestMethod]
    public void RemoveEntries02()
    {
        var dir = $@"{RootDir}\remove_entries2";
        var writeFile = $@"{dir}\write.zip";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        using var archiver = new MemoryArchiver(TestFile);
        archiver.RemoveEntry("zip_folder/text_file2.txt");
        archiver.RemoveEntry("empty_folder/");

        var bin = archiver.GetBytes();
        File.WriteAllBytes(writeFile, bin);

        #region validate
        ZipFile.ExtractToDirectory(writeFile, dir);
        Assert.IsTrue(File.Exists($@"{dir}\text_file1.txt"));
        Assert.IsTrue(File.Exists($@"{dir}\zip_folder\text_file3"));

        Assert.IsFalse(File.Exists($@"{dir}\zip_folder\text_file2.txt"));
        Assert.IsFalse(Directory.Exists($@"{dir}\empty_folder"));
        #endregion
    }
}
