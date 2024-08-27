namespace CarerExtensionTest.Utilities.FileSystem;

[TestClass]
public class PathnameTest
{
    private const string ROOT_DIR = @"test\pathname";

    [ClassInitialize]
    public static void Initialize(TestContext _)
    {
        Directory.CreateDirectory(ROOT_DIR);
    }

    [TestMethod]
    public void Children01()
    {
        var dir = $@"{ROOT_DIR}\child1";

        #region pre-process
        Directory.CreateDirectory($@"{dir}\a01");
        Directory.CreateDirectory($@"{dir}\a02");
        Directory.CreateDirectory($@"{dir}\b01");
        File.WriteAllText($@"{dir}\c01.txt", "c01");
        #endregion

        var path = new Pathname(dir);
        var children = path.Children();

        Assert.AreEqual(4, children.Count());
        Assert.AreEqual("a01", children.ElementAt(0).FileName);
        Assert.AreEqual("c01.txt", children.ElementAt(3).FileName);
    }

    [TestMethod]
    public void Children02()
    {
        var dir = $@"{ROOT_DIR}\child2";

        #region pre-process
        Directory.CreateDirectory($@"{dir}\a01");
        Directory.CreateDirectory($@"{dir}\a02");
        Directory.CreateDirectory($@"{dir}\b01");
        File.WriteAllText($@"{dir}\c01.txt", "c01");
        #endregion

        var path = new Pathname(dir);
        // language=regex
        var children = path.Children("^a");

        Assert.AreEqual(2, children.Count());
        Assert.AreEqual("a01", children.ElementAt(0).FileName);
        Assert.AreEqual("a02", children.ElementAt(1).FileName);
    }

    [TestMethod]
    public void Combine01()
    {
        var dir = new Pathname(@"C:\test");
        {
            var file = dir.Combine("test.txt");
            Assert.AreEqual(@"C:\test\test.txt", file.ToString());
        }
        {
            var file = dir.Combine(@"a\", "test.txt");
            Assert.AreEqual(@"C:\test\a\test.txt", file.ToString());
        }
        {
            var file = dir.Combine(["b", "test.txt"]);
            Assert.AreEqual(@"C:\test\b\test.txt", file.ToString());
        }
    }

    [TestMethod]
    public void Combine02()
    {
        var dir = new Pathname(@"C:\test");
        {
            var file = dir + "test.txt";
            Assert.AreEqual(@"C:\test\test.txt", file.ToString());
        }
        {
            var file = dir + new Pathname("test.txt");
            Assert.AreEqual(@"C:\test\test.txt", file.ToString());
        }
    }

    [TestMethod]
    public void CopyTo01()
    {
        #region pre-process
        {
            var dir = Directory.CreateDirectory(@$"{ROOT_DIR}\copy_to");
            File.WriteAllText($@"{dir}\test.txt", "");
        }
        #endregion

        var path1 = new Pathname(ROOT_DIR, "copy_to", "test.txt");
        var path2 = new Pathname(ROOT_DIR, "copy_to", "test2.txt");
        var path3 = $@"{ROOT_DIR}\copy_to\test3.txt";

        Assert.IsFalse(File.Exists(path2.ToString()));
        Assert.IsFalse(File.Exists(path3));

        path1.CopyTo(path2);
        path1.CopyTo(path3);

        Assert.IsTrue(File.Exists(path2.ToString()));
        Assert.IsTrue(File.Exists(path3));
    }

    [TestMethod]
    public void CreateDirectory01()
    {
        var path1 = new Pathname(ROOT_DIR, "create_dir1");
        var path2 = new Pathname(ROOT_DIR, @"create_dir2\");

        Assert.IsFalse(Directory.Exists(path1.ToString()));
        Assert.IsFalse(Directory.Exists(path2.ToString()));

        path1.CreateDirectory();
        path2.CreateDirectory();

        Assert.IsTrue(Directory.Exists(path1.ToString()));
        Assert.IsTrue(Directory.Exists(path2.ToString()));
    }

    [TestMethod]
    public void Delete01()
    {
        var dir = $@"{ROOT_DIR}\delete1";

        #region pre-process
        {
            Directory.CreateDirectory(dir);
        }
        #endregion

        var path = new Pathname(dir);

        Assert.IsTrue(Directory.Exists(path.ToString()));

        path.Delete();

        Assert.IsFalse(Directory.Exists(path.ToString()));
    }

    [TestMethod]
    public void Delete02()
    {
        var file = $@"{ROOT_DIR}\delete2.txt";

        #region pre-process
        File.WriteAllText(file, "");
        #endregion

        var path = new Pathname(file);

        Assert.IsTrue(File.Exists(path.ToString()));

        path.Delete();

        Assert.IsFalse(File.Exists(path.ToString()));
    }

    [TestMethod]
    public void DeleteDirectory01()
    {
        #region pre-process
        Directory.CreateDirectory($@"{ROOT_DIR}\delete_dir1");
        Directory.CreateDirectory($@"{ROOT_DIR}\delete_dir2");
        #endregion

        var path1 = new Pathname(ROOT_DIR, "delete_dir1");
        var path2 = new Pathname(ROOT_DIR, @"delete_dir2\");

        Assert.IsTrue(Directory.Exists(path1.ToString()));
        Assert.IsTrue(Directory.Exists(path2.ToString()));

        path1.DeleteDirectory();
        path2.Delete();

        Assert.IsFalse(Directory.Exists(path1.ToString()));
        Assert.IsFalse(Directory.Exists(path2.ToString()));
    }

    [TestMethod]
    public void DeleteFile01()
    {
        var dir = $@"{ROOT_DIR}\delete_file1";

        #region pre-process
        {
            Directory.CreateDirectory(dir);
            File.WriteAllText($@"{dir}\test1.txt", "");
            File.WriteAllText($@"{dir}\test2.txt", "");
        }
        #endregion

        var path1 = new Pathname(dir, "test1.txt");
        var path2 = new Pathname(dir, "test2.txt");

        Assert.IsTrue(File.Exists(path1.ToString()));
        Assert.IsTrue(File.Exists(path2.ToString()));

        path1.DeleteFile();
        path2.Delete();

        Assert.IsFalse(File.Exists(path1.ToString()));
        Assert.IsFalse(File.Exists(path2.ToString()));
    }

    [TestMethod]
    public void Empty01()
    {
        var baseDir = $@"{ROOT_DIR}\empty1";
        var dir = $@"{baseDir}\directory1";
        var file = $@"{baseDir}\test1.txt";

        #region pre-process
        {
            Directory.CreateDirectory(dir);
            File.WriteAllText(file, "");
        }
        #endregion

        var path = new Pathname(baseDir);

        Assert.IsTrue(Directory.Exists(dir));
        Assert.IsTrue(File.Exists(file));

        path.Empty();

        Assert.IsFalse(Directory.Exists(dir));
        Assert.IsFalse(File.Exists(file));
    }

    [TestMethod]
    public void EnumerateDirectories01()
    {
        var dir = $@"{ROOT_DIR}\enumDir1";

        #region pre-process
        Directory.CreateDirectory($@"{dir}\a01\aa01");
        Directory.CreateDirectory($@"{dir}\a02");
        Directory.CreateDirectory($@"{dir}\b01");
        File.WriteAllText($@"{dir}\c01.txt", "c01");
        #endregion

        var path = new Pathname(dir);

        {
            var dirs = path.EnumerateDirectories(SearchOption.TopDirectoryOnly);
            Assert.AreEqual(3, dirs.Count());
            Assert.AreEqual("a01", dirs.ElementAt(0).FileName);
            Assert.AreEqual("b01", dirs.ElementAt(2).FileName);
        }
        {
            var dirs = path.EnumerateDirectories(SearchOption.AllDirectories);
            Assert.AreEqual(4, dirs.Count());
            Assert.AreEqual("a01", dirs.ElementAt(0).FileName);
            Assert.AreEqual("aa01", dirs.ElementAt(3).FileName);
        }
    }

    [TestMethod]
    public void EnumerateDirectories02()
    {
        var dir = $@"{ROOT_DIR}\enumDir2";

        #region pre-process
        Directory.CreateDirectory($@"{dir}\a01\aa01");
        Directory.CreateDirectory($@"{dir}\a02");
        Directory.CreateDirectory($@"{dir}\b01");
        File.WriteAllText($@"{dir}\c01.txt", "c01");
        #endregion

        var path = new Pathname(dir);
        // language=regex
        var dirs = path.EnumerateDirectories("^a", SearchOption.AllDirectories);

        Assert.AreEqual(3, dirs.Count());
        Assert.AreEqual("a01", dirs.ElementAt(0).FileName);
        Assert.AreEqual("a02", dirs.ElementAt(1).FileName);
    }

    [TestMethod]
    public void EnumerateFiles01()
    {
        var baseDir = $@"{ROOT_DIR}\enumFile1";
        var dir = $@"{baseDir}\a01";

        #region pre-process
        Directory.CreateDirectory(dir);
        File.WriteAllText($@"{baseDir}\a01.txt", "");
        File.WriteAllText($@"{dir}\a02.txt", "");
        File.WriteAllText($@"{dir}\b01.txt", "");
        #endregion

        var path = new Pathname(baseDir);

        {
            var files = path.EnumerateFiles(SearchOption.TopDirectoryOnly);
            Assert.AreEqual(1, files.Count());
            Assert.AreEqual("a01.txt", files.ElementAt(0).FileName);
        }
        {
            var files = path.EnumerateFiles(SearchOption.AllDirectories);
            Assert.AreEqual(3, files.Count());
            Assert.AreEqual("a01.txt", files.ElementAt(0).FileName);
            Assert.AreEqual("b01.txt", files.ElementAt(2).FileName);
        }
    }

    [TestMethod]
    public void EnumerateFiles02()
    {
        var baseDir = $@"{ROOT_DIR}\enumFile2";
        var dir = $@"{baseDir}\a01";

        #region pre-process
        Directory.CreateDirectory(dir);
        File.WriteAllText($@"{baseDir}\a01.txt", "");
        File.WriteAllText($@"{dir}\a02.txt", "");
        File.WriteAllText($@"{dir}\b01.txt", "");
        #endregion

        var path = new Pathname(baseDir);
        // language=regex
        var files = path.EnumerateFiles("^a", SearchOption.AllDirectories);

        Assert.AreEqual(2, files.Count());
        Assert.AreEqual("a01.txt", files.ElementAt(0).FileName);
        Assert.AreEqual("a02.txt", files.ElementAt(1).FileName);
    }

    [TestMethod]
    public void Equals01()
    {
        var path = new Pathname(@"C:\test\test1.txt");
        var path1 = new Pathname(@"C:\test\test1.txt");
        var path2 = new Pathname(@"C:\test\test2.txt");

        Assert.IsTrue(path.Equals(path1));
        Assert.IsTrue(path.Equals(path1.ToString()));

        Assert.IsFalse(path.Equals(path2));
        Assert.IsFalse(path.Equals(path2.ToString()));
        Assert.IsFalse(path.Equals(1));
    }

    [TestMethod]
    public void Equals02()
    {
        var path = new Pathname(@"C:\test\test1.txt");
        var path1 = new Pathname(@"C:\test\test1.txt");
        var path2 = new Pathname(@"C:\test\test2.txt");

        Assert.IsTrue(path == path1);
        Assert.IsTrue(path == path1.ToString());
        Assert.IsFalse(path != path1);
        Assert.IsFalse(path != path1.ToString());

        Assert.IsFalse(path == path2);
        Assert.IsFalse(path == path2.ToString());
        Assert.IsTrue(path != path2);
        Assert.IsTrue(path != path2.ToString());
    }

    [TestMethod]
    public void MoveTo01()
    {
        var dir = @$"{ROOT_DIR}\move_to1";

        #region pre-process
        Directory.CreateDirectory(dir);
        File.WriteAllText($@"{dir}\src.txt", "");
        #endregion

        var src = new Pathname($@"{dir}\src.txt");
        var dest = new Pathname($@"{dir}\dest.txt");

        Assert.IsTrue(File.Exists(src.ToString()));
        Assert.IsFalse(File.Exists(dest.ToString()));

        src.MoveTo(dest);

        Assert.IsFalse(File.Exists(src.ToString()));
        Assert.IsTrue(File.Exists(dest.ToString()));
    }

    [TestMethod]
    public void MoveTo02()
    {
        var dir = @$"{ROOT_DIR}\move_to2";

        #region pre-process
        Directory.CreateDirectory(dir);
        File.WriteAllText($@"{dir}\src.txt", "");
        #endregion

        var src = new Pathname($@"{dir}\src.txt");
        var dest = $@"{dir}\dest.txt";

        Assert.IsTrue(File.Exists(src.ToString()));
        Assert.IsFalse(File.Exists(dest));

        src.MoveTo(dest);

        Assert.IsFalse(File.Exists(src.ToString()));
        Assert.IsTrue(File.Exists(dest));
    }

    [TestMethod]
    public void New01()
    {
        var path1 = new Pathname(@"C:\test");
        var path2 = new Pathname(@"C:\test", "a");
        var path3 = new Pathname(@"C:\test", @"a\", @"b\");

        Assert.AreEqual(@"C:\test", path1.ToString());
        Assert.AreEqual(@"C:\test\a", path2.ToString());
        Assert.AreEqual(@"C:\test\a\b\", path3.ToString());
    }

    [TestMethod]
    public void Parent01()
    {
        {
            var path = new Pathname(@"C:\test\a\b");
            Assert.AreEqual(@"C:\test\a", path.Parent().ToString());
            Assert.AreEqual(@"C:\test", path.Parent().Parent().ToString());
        }
        {
            var path = new Pathname(@"C:\test\a\b\");
            Assert.AreEqual(@"C:\test\a\b", path.Parent().ToString());
        }
    }

    [TestMethod]
    public void Properties01()
    {
        {
            var dir = new Pathname(@"C:\test\a\");
            Assert.AreEqual(@"C:\test\a\", dir.FullPath);
            Assert.AreEqual("", dir.FileName);
            Assert.AreEqual("", dir.Extension);
        }
        {
            var file = new Pathname(@"C:\test\a", "test.txt");
            Assert.AreEqual(@"C:\test\a\test.txt", file.FullPath);
            Assert.AreEqual("test.txt", file.FileName);
            Assert.AreEqual(".txt", file.Extension);
        }
    }

    [TestMethod]
    public void Properties02()
    {
        var dir = $@"{ROOT_DIR}\property02";

        #region pre-process
        Directory.CreateDirectory(dir);
        File.WriteAllText($@"{dir}\test.txt", "");
        #endregion

        {
            var folder = new Pathname(dir);
            Assert.IsTrue(folder.IsDirectory);
            Assert.IsFalse(folder.IsFile);
            Assert.IsTrue(folder.Exists);
        }
        {
            var file = new Pathname(dir, "test.txt");
            Assert.IsFalse(file.IsDirectory);
            Assert.IsTrue(file.IsFile);
            Assert.IsTrue(file.Exists);
        }
    }

    [TestMethod]
    public void Write01()
    {
        var dir = $@"{ROOT_DIR}\write1";

        #region pre-process
        Directory.CreateDirectory(dir);
        #endregion

        {
            var file = new Pathname(dir, "test1.txt");
            file.Write("test");
            Assert.IsTrue(File.Exists(file.ToString()));
        }
        {
            var file = new Pathname(dir, "test1.bin");
            file.Write((IEnumerable<byte>)[1, 2]);
            Assert.IsTrue(File.Exists(file.ToString()));
        }
        {
            var file = new Pathname(dir, "test2.bin");
            file.Write([1, 2]);
            Assert.IsTrue(File.Exists(file.ToString()));
        }
    }
}
