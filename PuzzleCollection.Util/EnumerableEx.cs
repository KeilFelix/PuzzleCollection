using System.Collections.ObjectModel;

namespace PuzzleCollection.Util;


public static class EnumerableEx
{
    public static IEnumerable<int> RailFence(int n) => Enumerable.Range(0, n).Concat(Enumerable.Range(1, n - 2).Reverse()).Repeat();

    public static IEnumerable<int> Iterate(int start, int step)
    {
        yield return start;

        while (true)
        {
            start += step;
            yield return start;
        }
    }
    extension(IEnumerable<int> source)
    {
        public int Product() => source.Aggregate(1, (cur, next) => cur * next);
    }

    extension<T>(IEnumerable<T> source)
    {
        public IEnumerable<T> Repeat()
        {
            while (true)
            {
                foreach (var item in source)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<(T? Previous, T Current)> PairWithPrevious()
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

        public IEnumerable<ReadOnlyCollection<T>> PairWithPrevious(int count)
        {

            using (var iterator = source.GetEnumerator())
            {
                List<T> values = new();

                for (int i = 0; i < count; i++)
                {
                    if (!iterator.MoveNext())
                        yield break;
                    values.Add(iterator.Current);

                    yield return values.AsReadOnly();
                }

                while (iterator.MoveNext())
                {
                    values.RemoveAt(0);
                    values.Add(iterator.Current);
                    yield return values.AsReadOnly();
                }
            }
        }


        public Stack<T> ToStack() => new(source);



        public IEnumerable<IEnumerable<T>> SplitBefore(Func<T, bool> predicate)
        {
            using (var enumerator = source.GetEnumerator())
            {
                var chunk = new List<T>();
                bool hasNext = enumerator.MoveNext();
                while (hasNext)
                {
                    chunk.Add(enumerator.Current);
                    hasNext = enumerator.MoveNext();

                    if (!hasNext || predicate(enumerator.Current))
                    {
                        yield return chunk.AsReadOnly();
                        chunk = new List<T>();
                    }
                }
            }
        }

        public IEnumerable<IEnumerable<T>> SplitAfter(Func<T, bool> predicate)
        {
            using (var enumerator = source.GetEnumerator())
            {
                var chunk = new List<T>();
                bool hasNext = enumerator.MoveNext();

                while (hasNext)
                {
                    chunk.Add(enumerator.Current);

                    var isMatch = predicate(enumerator.Current);
                    hasNext = enumerator.MoveNext();
                    if (!hasNext || isMatch)
                    {
                        yield return chunk.AsReadOnly();
                        chunk = new List<T>();
                    }
                }
            }
        }

        public IEnumerable<IEnumerable<T>> SplitBy(int count)
            => source
                .Select((Entry, Index) => (Entry, Index))
                .SplitAfter(t => t.Index % count == count - 1)
                .Select(split => split.Select(t => t.Entry));

        

        public bool IsPalindrome()
        {
            var sourceMem = source.Memoize();
            var halfLength = sourceMem.Count() / 2;
            return sourceMem.Take(halfLength).SequenceEqual(sourceMem.Reverse().Take(halfLength));
        }

    }

    extension<T>(IEnumerable<IEnumerable<T>> sources)
    {
        public IEnumerable<T> IntersectAll()
            => sources.Aggregate((current, next) => current.Intersect(next));
    }

    extension<T>(T source)
    {
        public IEnumerable<T> Yield() { yield return source; }
    }

    extension(object source)
    {
        public T? As<T>() => source is T dest ? dest : default;
    }
}

