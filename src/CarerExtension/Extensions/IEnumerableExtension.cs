using CarerExtension.Extensions.Chunking;

namespace CarerExtension.Extensions;

public static class IEnumerableExtension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IChunking<T>> Chunk<T>
        (this IEnumerable<T> source,
        Func<T, bool> predicate) =>
        source.Chunk(predicate, item => !predicate(item));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IChunking<T>> Chunk<T>(
        this IEnumerable<T> source,
        Func<T, bool> chunkStartPredicate,
        Func<T, bool> chunkStopPredicate)
    {
        var skipCount = 0;

        var items = source.Select((v, i) => (Value: v, Index: i));
        while (true)
        {
            var targets = items.Skip(skipCount);
            if (targets.None())
            {
                break;
            }

            var chunk = targets
                .SkipWhile(t => !chunkStartPredicate(t.Value) || chunkStopPredicate(t.Value))
                .TakeWhile(t => !chunkStopPredicate(t.Value));
            if (chunk.None())
            {
                break;
            }

            var startIndex = chunk.First().Index;
            var endIndex = chunk.Last().Index;
            yield return Chunking<T>.Create(source, startIndex, endIndex);

            skipCount = endIndex + 1;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Compact<T>(this IEnumerable<T> source) =>
        source.Where(s => s != null);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Cycle<T>(this IEnumerable<T> source, int count)
    {
        for (var i = 0; i < count; i++)
        {
            foreach (var s in source)
            {
                yield return s;
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IEnumerable<T>> EachCons<T>(this IEnumerable<T> source, int count)
    {
        count = Math.Max(1, count);

        var i = 0;
        while (true)
        {
            var startIndex = i * count;
            var current = source.Slice(startIndex, count);
            if (current.Any())
            {
                yield return current;
            }
            else
            {
                yield break;
            }
            i++;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Excluding<T>(this IEnumerable<T> source, T[] elements) =>
        source.Excluding(elements.AsEnumerable());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Excluding<T>(this IEnumerable<T> source, IEnumerable<T> elements)
    {
        foreach (var s in source)
        {
            var hasElement = elements.Any(e =>
            {
                if (e == null)
                {
                    return s == null;
                }
                else
                {
                    return e.Equals(s);
                }
            });
            // only if it does not exist in elements.
            if (!hasElement)
            {
                yield return s;
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Including<T>(this IEnumerable<T> source, params T[] elements) =>
        Including(source, elements.AsEnumerable());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Including<T>(this IEnumerable<T> source, IEnumerable<T> elements)
    {
        foreach (var s in source)
        {
            yield return s;
        }
        foreach (var e in elements)
        {
            yield return e;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Many<T>(this IEnumerable<T> source, int count = 1) =>
        source.Count() > count;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> MultiFirst<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        var isMatchOld = false;

        foreach (var item in source)
        {
            var isMatch = predicate(item);
            if (!isMatchOld && isMatch)
            {
                yield return item;
            }

            isMatchOld = isMatch;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool None<T>(this IEnumerable<T> source) => !source.Any();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool None<T>(this IEnumerable<T> source, Func<T, bool> predicate) =>
        !source.Any(predicate);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<(TOuter, TInner)> Permutate<TOuter, TInner>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner) =>
        outer.Join(inner, _ => true, _ => true, (o, i) => (o, i));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Reject<T>(this IEnumerable<T> source, Func<T, bool> predicate) =>
        source.Where(s => !predicate(s));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> Slice<T>(this IEnumerable<T> source, int startIndex, int count) =>
        source.Skip(startIndex).Take(count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> SliceByIndex<T>(this IEnumerable<T> source, int startIndex, int endIndex)
    {
        var count = endIndex - startIndex + 1;
        return source.Slice(startIndex, count);
    }
}
