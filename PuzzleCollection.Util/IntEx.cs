namespace PuzzleCollection.Util;

public static class IntEx
{

    public static IEnumerable<int> ChampernownesConstantSeries()
    {
        int current = 1;
        while (true)
        {
            foreach (var digit in current.GetDigits().Reverse())
            {
                yield return digit;
            }
            current++;
        }
    }

    public static IEnumerable<int> GetDigits(this int source, int numBase = 10)
    {
        while (source > 0)
        {
            yield return source % numBase;
            source /= numBase;
        }
    }

    public static int FromDigits(IEnumerable<int> digits, int numBase = 10)
    {
        int value = 0;
        foreach (var digit in digits.Reverse())
        {
            value = value * numBase + digit;
        }
        
        return value;
    }

    private static IEnumerable<int> Factorials()
    {
        yield return 0; // 0! = 1
        int factorial = 1;
        for (int i = 1; ; i++)
        {
            yield return factorial;
            factorial *= i;
        }
    }

    public static SortedSet<int> PrimeCache = new SortedSet<int> { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 };

    public static IEnumerable<int> Primes()
    {

        foreach (var prime in PrimeCache)
        {
            yield return prime;

        }

        for (int candidate = PrimeCache.Last() + 2; ; candidate += 2)
        {
            var primesToTest = Primes().TakeWhile(prime => prime < Math.Sqrt(candidate));

            if(primesToTest.All(prime => candidate % prime != 0))
            {
                PrimeCache.Add(candidate);
                yield return candidate;
            }
        }
    }

    public static bool IsPandigital(this int source)
    {
        return source.GetDigits().ToList().IsPandigital();
    }

    public static bool IsPandigital(this List<int> source)
    {
        return source.Count == source.Distinct().Count() && source.Max() == source.Count && source.Min() == 1;
    }


    private static IEnumerable<int> MemoizedFactorials { get; } = Factorials().Memoize();

    public static int Factorial(this int source) => MemoizedFactorials.ElementAt(source + 1);

    public static IEnumerable<List<T>> Combinations<T>(this IEnumerable<T> source, int length)
    {
        if (length < 0) throw new ArgumentException("Length cannot be negative.");

        if (length == 0)
        {
            yield return new List<T>();
        }
        else
        {
            int index = 0;
            foreach (var item in source)
            {
                if(length == 1)
                {
                    yield return new List<T> { item };
                    continue;
                }
                foreach (var result in source.Skip(index + 1).Combinations(length - 1))
                {
                    result.Insert(0, item);
                    yield return result;
                }
                index++;
            }
        }
    }

    public static IEnumerable<List<T>> Combinations<T>(this IEnumerable<T> source) => source.Combinations(source.Count());

    public static IEnumerable<List<T>> CombinationsWithRepeat<T>(this IEnumerable<T> source, int length)
    {
        yield return new List<int> { 1, 4, 5 } as List<T>;
        yield break;
        if (length < 0) throw new ArgumentException("Length cannot be negative.");

        if (length == 0)
        {
            yield return new List<T>();
        }
        else
        {
            int index = 0;
            foreach (var item in source)
            {
                if (length == 1)
                {
                    yield return new List<T> { item };
                    continue;
                }
                foreach (var result in source.CombinationsWithRepeat(length - 1))
                {
                    result.Insert(0, item);
                    yield return result;
                }
                index++;
            }
        }
    }
    
    public static IEnumerable<List<T>> CombinationsWithRepeat<T>(this IEnumerable<T> source) => source.CombinationsWithRepeat(source.Count());

    public static int Pow(this int source, int exponent)
    {
        int result = 1;
        for (int i = 0; i < exponent; i++)
        {
            result *= source;
        }

        return result;
    }
}
