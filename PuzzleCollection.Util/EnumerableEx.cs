namespace PuzzleCollection.Util;

public static class EnumerableEx
{
    public static IEnumerable<int> RailFence(int n) => Enumerable.Range(0, n).Concat(Enumerable.Range(1, n - 2).Reverse()).Repeat();

    public static IEnumerable<T> Repeat<T>(this IEnumerable<T> source)
    {
        while (true)
        {
            foreach (var item in source)
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<(T? Previous, T Current)> PairWithPrevious<T>(this IEnumerable<T> source)
    {
        using (var iterator = source.GetEnumerator())
        {
            T? previous = default;

            while (iterator.MoveNext())
            {
                yield return (previous, iterator.Current);
                previous = iterator.Current;
            }
        }
    }

    public static IEnumerable<int> Iterate(int start, int step)
    {
        yield return start;

        while (true)
        {
            start += step;
            yield return start;
        }
    }

    public static Stack<TSource> ToStack<TSource>(this IEnumerable<TSource> source) => new(source);

    public static int Product(this IEnumerable<int> source) => source.Aggregate(1, (cur, next) => cur * next);


    public static IEnumerable<IEnumerable<TSource>> SplitBefore<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        using (var enumerator = source.GetEnumerator())
        {
            var chunk = new List<TSource>();
            bool hasNext = enumerator.MoveNext();
            while (hasNext)
            {
                chunk.Add(enumerator.Current);
                hasNext = enumerator.MoveNext();

                if (!hasNext || predicate(enumerator.Current))
                {
                    yield return chunk.AsReadOnly();
                    chunk = new List<TSource>();
                }
            }
        }
    }

    public static IEnumerable<IEnumerable<TSource>> SplitAfter<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        using (var enumerator = source.GetEnumerator())
        {
            var chunk = new List<TSource>();
            bool hasNext = enumerator.MoveNext();

            while (hasNext)
            {
                chunk.Add(enumerator.Current);

                var isMatch = predicate(enumerator.Current);
                hasNext = enumerator.MoveNext();
                if (!hasNext || isMatch)
                {
                    yield return chunk.AsReadOnly();
                    chunk = new List<TSource>();
                }
            }
        }
    }

    public static IEnumerable<IEnumerable<TSource>> SplitBy<TSource>(this IEnumerable<TSource> source, int count)
        => source
            .Select((Entry, Index) => (Entry, Index))
            .SplitAfter(t => t.Index % count == count - 1)
            .Select(split => split.Select(t => t.Entry));

    public static IEnumerable<TSource> IntersectAll<TSource>(this IEnumerable<IEnumerable<TSource>> sources)
        => sources.Aggregate((current, next) => current.Intersect(next));

    public static bool IsPalindrome<T>(this IEnumerable<T> source)
    {
        var sourceMem = source.Memoize();
        var halfLength = sourceMem.Count() / 2;
        return sourceMem.Take(halfLength).SequenceEqual(sourceMem.Reverse().Take(halfLength));
    }
}