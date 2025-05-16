namespace CarerExtensionTest.Extensions;

[TestClass]
public class IEnumerableExtensionTest
{
    #region Chunk
    [TestMethod]
    public void Chunk01()
    {
        // only arg is start condition.
        int[] arr = [1, 2, 100, 3, 100, 100, 4, 5, 6];
        var chunks = arr.Chunk(a => a < 10);

        Assert.AreEqual(3, chunks.Count());

        {
            var chunk = chunks.ElementAt(0);
            Assert.AreEqual(2, chunk.Count());
            Assert.AreEqual(1, chunk.ElementAt(0));
            Assert.AreEqual(2, chunk.ElementAt(1));
        }
        {
            var chunk = chunks.ElementAt(2);
            Assert.AreEqual(3, chunk.Count());
            Assert.AreEqual(4, chunk.ElementAt(0));
            Assert.AreEqual(6, chunk.ElementAt(2));
        }
    }

    [TestMethod]
    public void Chunk02()
    {
        // only arg is start condition.
        int[] arr = [100, 1, 2, 100, 3, 100, 100, 4, 5, 6, 100];
        var chunks = arr.Chunk(a => a < 10);

        Assert.AreEqual(3, chunks.Count());

        {
            var chunk = chunks.ElementAt(0);
            Assert.AreEqual(2, chunk.Count());
        }
        {
            var chunk = chunks.ElementAt(2);
            Assert.AreEqual(3, chunk.Count());
        }
    }

    [TestMethod]
    public void Chunk03()
    {
        int[] arr = [21, 11, 9, 22, 9, 11, 23, 13, 14];
        var chunks = arr.Chunk(a => a > 20, a => a < 10);

        Assert.AreEqual(3, chunks.Count());

        {
            var chunk = chunks.ElementAt(0);
            Assert.AreEqual(2, chunk.Count());
            Assert.AreEqual(21, chunk.ElementAt(0));
            Assert.AreEqual(11, chunk.ElementAt(1));
        }
        {
            var chunk = chunks.ElementAt(2);
            Assert.AreEqual(3, chunk.Count());
            Assert.AreEqual(23, chunk.ElementAt(0));
            Assert.AreEqual(13, chunk.ElementAt(1));
            Assert.AreEqual(14, chunk.ElementAt(2));
        }
    }

    [TestMethod]
    public void Chunk04()
    {
        int[] arr = [9, 21, 11, 9, 22, 9, 11, 23, 13, 9];
        var chunks = arr.Chunk(a => a > 20, a => a < 10);

        Assert.AreEqual(3, chunks.Count());

        {
            var chunk = chunks.ElementAt(0);
            Assert.AreEqual(2, chunk.Count());
        }
        {
            var chunk = chunks.ElementAt(2);
            Assert.AreEqual(2, chunk.Count());
        }
    }

    [TestMethod]
    public void Chunk05()
    {
        // strange condition.
        {
            int[] arr = [10];
            var chunks = arr.Chunk(a => a < 50, a => a < 10);

            Assert.AreEqual(1, chunks.Count());

            {
                var chunk = chunks.ElementAt(0);
                Assert.AreEqual(1, chunk.Count());
                Assert.AreEqual(10, chunk.ElementAt(0));
            }
        }
        {
            int[] arr = [10, 20];
            var chunks = arr.Chunk(a => a < 50, a => a < 10);

            Assert.AreEqual(1, chunks.Count());

            {
                var chunk = chunks.ElementAt(0);
                Assert.AreEqual(2, chunk.Count());
                Assert.AreEqual(10, chunk.ElementAt(0));
                Assert.AreEqual(20, chunk.ElementAt(1));
            }
        }
        {
            int[] arr = [9, 10, 20];
            var chunks = arr.Chunk(a => a < 50, a => a < 10);

            Assert.AreEqual(1, chunks.Count());

            {
                var chunk = chunks.ElementAt(0);
                Assert.AreEqual(2, chunk.Count());
            }
        }
        {
            int[] arr = [10, 20, 9];
            var chunks = arr.Chunk(a => a < 50, a => a < 10);

            Assert.AreEqual(1, chunks.Count());

            {
                var chunk = chunks.ElementAt(0);
                Assert.AreEqual(2, chunk.Count());
            }
        }
    }

    [TestMethod]
    public void Chunk06()
    {
        // strange condition.
        int[] arr = [10, 20, 9, 11, 21];
        var chunks = arr.Chunk(a => a < 50, a => a < 10);

        Assert.AreEqual(2, chunks.Count());

        {
            var chunk = chunks.ElementAt(0);
            Assert.AreEqual(2, chunk.Count());
            Assert.AreEqual(10, chunk.ElementAt(0));
            Assert.AreEqual(20, chunk.ElementAt(1));
        }
        {
            var chunk = chunks.ElementAt(1);
            Assert.AreEqual(2, chunk.Count());
            Assert.AreEqual(11, chunk.ElementAt(0));
            Assert.AreEqual(21, chunk.ElementAt(1));
        }
    }
    #endregion

    [TestMethod]
    public void Compact01()
    {
        int?[] arr = [null, 1, 2, null, 3, null];
        var results = arr.Compact();

        Assert.AreEqual(3, results.Count());
        Assert.AreEqual(1, results.ElementAt(0));
        Assert.AreEqual(3, results.ElementAt(2));
    }

    [TestMethod]
    public void CrossJoin01()
    {
        {
            int[] arr = [1, 2];
            var results = arr.CrossJoin([3, 4]);

            Assert.AreEqual(4, results.Count());
            Assert.AreEqual((1, 3), results.ElementAt(0));
            Assert.AreEqual((2, 4), results.ElementAt(3));
        }
        {
            // empty + not empty.
            int[] arr = [];
            var results = arr.CrossJoin([3, 4]);

            Assert.AreEqual(0, results.Count());
        }
        {
            // not empty + empty.
            int[] arr = [1, 2];
            var results = arr.CrossJoin(Array.Empty<int>());

            Assert.AreEqual(0, results.Count());
        }
    }

    [TestMethod]
    public void Cycle01()
    {
        int[] arr = [1, 2, 3];
        var results = arr.Cycle(3);

        Assert.AreEqual(9, results.Count());
        Assert.AreEqual(1, results.ElementAt(0));
        Assert.AreEqual(3, results.ElementAt(8));
    }

    #region EachCons
    [TestMethod]
    public void EachCons01()
    {
        int[] arr = [1, 2, 3, 4, 5, 6];
        var results = arr.EachCons(3);

        Assert.AreEqual(4, results.Count());
        {
            var result = results.ElementAt(0);
            Assert.AreEqual(3, result.Count());

            Assert.AreEqual(1, result.ElementAt(0));
            Assert.AreEqual(3, result.ElementAt(2));
        }
        {
            var result = results.ElementAt(3);
            Assert.AreEqual(3, result.Count());

            Assert.AreEqual(4, result.ElementAt(0));
            Assert.AreEqual(6, result.ElementAt(2));
        }
    }

    [TestMethod]
    public void EachCons02()
    {
        int[] arr = [1, 2];
        var results = arr.EachCons(3);

        Assert.AreEqual(0, results.Count());
    }
    #endregion

    #region Excluding
    [TestMethod]
    public void Excluding01()
    {
        {
            int[] arr = [1, 2, 3];
            var results = arr.Excluding([3, 4]);

            Assert.AreEqual(2, results.Count());
            Assert.AreEqual(1, results.ElementAt(0));
            Assert.AreEqual(2, results.ElementAt(1));
        }
        {
            int[] arr = [1, 2];
            var results = arr.Excluding([1, 2]);

            Assert.AreEqual(0, results.Count());
        }
    }

    [TestMethod]
    public void Excluding02()
    {
        int?[] arr = [1, null, 3];
        var results = arr.Excluding([null]);

        Assert.AreEqual(2, results.Count());
        Assert.AreEqual(1, results.ElementAt(0));
        Assert.AreEqual(3, results.ElementAt(1));
    }
    #endregion

    [TestMethod]
    public void Including01()
    {
        int[] arr = [1, 2];
        var results = arr.Including([3, 4]);

        Assert.AreEqual(4, results.Count());
        Assert.AreEqual(1, results.ElementAt(0));
        Assert.AreEqual(4, results.ElementAt(3));
    }

    [TestMethod]
    public void Many01()
    {
        {
            int[] arr = [1, 2];
            Assert.IsTrue(arr.Many());
        }
        {
            int[] arr = [1];
            Assert.IsFalse(arr.Many());
        }
    }

    #region None
    [TestMethod]
    public void None01()
    {
        {
            int[] arr = [];
            Assert.IsTrue(arr.None());
        }
        {
            int[] arr = [1];
            Assert.IsFalse(arr.None());
        }
    }

    [TestMethod]
    public void None02()
    {
        {
            int[] arr = [];
            Assert.IsTrue(arr.None(a => a > 10));
        }
        {
            int[] arr = [1, 2];
            Assert.IsTrue(arr.None(a => a > 10));
        }
        {
            int[] arr = [1, 11];
            Assert.IsFalse(arr.None(a => a > 10));
        }
    }
    #endregion

    [TestMethod]
    public void Reject01()
    {
        int[] arr = [1, 10, 2, 11];
        var results = arr.Reject(a => a < 10);

        Assert.AreEqual(2, results.Count());
        Assert.AreEqual(10, results.ElementAt(0));
        Assert.AreEqual(11, results.ElementAt(1));
    }

    [TestMethod]
    public void Slice01()
    {
        int[] arr = [1, 2, 3, 4, 5];
        var results = arr.Slice(0, 3);

        Assert.AreEqual(3, results.Count());
        Assert.AreEqual(1, results.ElementAt(0));
        Assert.AreEqual(3, results.ElementAt(2));
    }

    [TestMethod]
    public void Slice02()
    {
        int[] arr = [1];
        var results = arr.Slice(1, 2);

        Assert.AreEqual(0, results.Count());
    }

    [TestMethod]
    public void SliceByIndex01()
    {
        int[] arr = [1, 2, 3, 4, 5];
        var results = arr.SliceByIndex(0, 3);

        Assert.AreEqual(4, results.Count());
        Assert.AreEqual(1, results.ElementAt(0));
        Assert.AreEqual(4, results.ElementAt(3));
    }

    [TestMethod]
    public void SliceByIndex02()
    {
        int[] arr = [1];
        var results = arr.SliceByIndex(1, 2);

        Assert.AreEqual(0, results.Count());
    }
}
