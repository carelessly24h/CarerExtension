using System.Threading;
using System.Threading.Tasks;

namespace CarerExtensionTest.Utilities.Threading;

[TestClass]
public class TasksTest
{
    private const string RootDir = @"test\tasks";

    public TestContext TestContext { get; set; }

    [ClassInitialize]
    public static void Initialize(TestContext _)
    {
        Directory.CreateDirectory(RootDir);
    }

    [TestMethod]
    public void CancelAllTasks01()
    {
        var path1 = Path.Combine(RootDir, "cancel_all_tasks1.txt");
        var path2 = Path.Combine(RootDir, "cancel_all_tasks2.txt");

        using var processor = new ParallelProcessor();
        processor.AddTask((cts => FileDumpTask(path1, "test1", cts)));
        processor.AddTask((cts => FileDumpTask(path2, "test2", cts)));

        processor.CancelAllTasks();
        processor.Start(1);

        // wait for tasks to complete.
        Task.Delay(500, TestContext.CancellationToken).Wait(TestContext.CancellationToken);

        Assert.AreEqual(1, processor.WorkersCount);
        Assert.IsFalse(File.Exists(path1));
        Assert.IsFalse(File.Exists(path2));
    }

    [TestMethod]
    public void CancelTask01()
    {
        var path1 = Path.Combine(RootDir, "cancel_task1.txt");
        var path2 = Path.Combine(RootDir, "cancel_task2.txt");

        using var processor = new ParallelProcessor();
        var id1 = processor.AddTask((cts => FileDumpTask(path1, "test1", cts)));
        var id2 = processor.AddTask((cts => FileDumpTask(path2, "test2", cts)));

        processor.CancelTask(id1);
        processor.CancelTask(id2);
        processor.Start(1);

        // wait for tasks to complete.
        Task.Delay(500, TestContext.CancellationToken).Wait(TestContext.CancellationToken);

        Assert.AreEqual(1, processor.WorkersCount);
        Assert.IsFalse(File.Exists(path1));
        Assert.IsFalse(File.Exists(path2));
    }

    [TestMethod]
    public void StartMultiWorker()
    {
        var path1 = Path.Combine(RootDir, "multi_worker1.txt");
        var path2 = Path.Combine(RootDir, "multi_worker2.txt");
        var path3 = Path.Combine(RootDir, "multi_worker3.txt");

        using var processor = new ParallelProcessor();
        processor.AddTask((cts => FileDumpTask(path1, "test1", cts)));
        processor.AddTask((cts => FileDumpTask(path2, "test2", cts)));
        processor.Start(2);

        // wait for tasks to complete.
        Task.Delay(500, TestContext.CancellationToken).Wait(TestContext.CancellationToken);

        Assert.AreEqual(2, processor.WorkersCount);
        {
            var file = File.ReadAllText(path1);
            Assert.AreEqual("test1", file);
        }
        {
            var file = File.ReadAllText(path2);
            Assert.AreEqual("test2", file);
        }
    }

    [TestMethod]
    public void StartSingleWorker()
    {
        var path1 = Path.Combine(RootDir, "single_worker1.txt");
        var path2 = Path.Combine(RootDir, "single_worker2.txt");

        using var processor = new ParallelProcessor();
        processor.AddTask((cts => FileDumpTask(path1, "test1", cts)));
        processor.AddTask((cts => FileDumpTask(path2, "test2", cts)));
        processor.Start(1);

        // wait for tasks to complete.
        Task.Delay(500, TestContext.CancellationToken).Wait(TestContext.CancellationToken);

        Assert.AreEqual(1, processor.WorkersCount);
        {
            var file = File.ReadAllText(path1);
            Assert.AreEqual("test1", file);
        }
        {
            var file = File.ReadAllText(path2);
            Assert.AreEqual("test2", file);
        }
    }

    [TestMethod]
    public void TaskCancelEvent01()
    {
        var path1 = Path.Combine(RootDir, "cancel_event1.txt");

        using var processor = new ParallelProcessor();
        var taskId = processor.AddTask((cts => FileDumpTask(path1, "", cts)));
        processor.TaskCanceled += (s, e) => CancelEvent(s, e, taskId);

        processor.CancelAllTasks();
        processor.Start(1);
    }

    [TestMethod]
    public void TaskCompleteEvent01()
    {
        var path1 = Path.Combine(RootDir, "complete_event1.txt");

        using var processor = new ParallelProcessor();
        var taskId = processor.AddTask((cts => FileDumpTask(path1, "", cts)));
        processor.TaskCompleted += (s, e) => CompleteEvent(s, e, taskId);

        processor.Start(1);
    }

    private static void FileDumpTask(string path, string contents, CancellationTokenSource cts)
    {
        cts.Token.ThrowIfCancellationRequested();
        File.WriteAllText(path, contents);
    }

    private static void CompleteEvent(object? sender, TaskEventArgs e, Guid taskId)
    {
        Assert.AreEqual(taskId, e.TaskId);
    }

    private static void CancelEvent(object? sender, TaskEventArgs e, Guid taskId)
    {
        Assert.AreEqual(taskId, e.TaskId);
    }
}
