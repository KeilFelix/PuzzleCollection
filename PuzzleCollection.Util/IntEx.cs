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

    public static IEnumerable<int> GetDigits(this long source, int numBase = 10)
    {
        while (source > 0)
        {
            yield return (int) (source % numBase);
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

    public static SortedSet<long> GetDefaultPrimeCache() => new SortedSet<long> { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 };
    public static IEnumerable<long> Primes(SortedSet<long> primeCache = null)
    {
        if(primeCache == null)
        {
            primeCache = GetDefaultPrimeCache();
        }

        foreach (var prime in primeCache)
        {
            yield return prime;

        }

        for (long candidate = primeCache.Last() + 2; ; candidate += 2)
        {
            var primesToTest = Primes(primeCache).TakeWhile(prime => prime < Math.Sqrt(candidate));

            if(primesToTest.All(prime => candidate % prime != 0))
            {
                primeCache.Add(candidate);
                yield return candidate;
            }
        }
    }

    public static bool IsPandigital(this long source)
    {
        return source.GetDigits().ToList().IsPandigital();
    }

    public static bool IsPandigital(this List<int> source)
    {
        return source.Count == source.Distinct().Count() && source.Max() == source.Count && source.Min() == 1;
    }


    private static IEnumerable<int> MemoizedFactorials { get; } = Factorials().Memoize();

    public static int Factorial(this int source) => MemoizedFactorials.ElementAt(source + 1);

    public static int Pow(this int source, int exponent)
    {
        int result = 1;
        for (int i = 0; i < exponent; i++)
        {
            result *= source;
        }

        return result;
    }


    public static long Pow(this long source, int exponent)
    {
        long result = 1;
        for (int i = 0; i < exponent; i++)
        {
            result *= source;
        }

        return result;
    }

    public static IEnumerable<long> HexagonalNumbers()
    {
        int n = 1;
        int increment = 1;
        while (true)
        {
            yield return n;
            increment += 4;
            n += increment;
        }
    }

    public static IEnumerable<long> PentagonalNumbers()
    {
        int n = 1;
        int increment = 1;
        while (true)
        {
            yield return n;
            increment += 3;
            n += increment;
        }
    }

    public static IEnumerable<long> TriangleNumbers()
    {

        int n = 0;
        int increment = 0;
        while (true)
        {
            increment++;
            n += increment;
            yield return n;
        }
    }

    public static IEnumerable<long> MultiplesOf(int basis)
    {
        int n = basis;
        while (true)
        {
            yield return n;
            n+=basis;
        }
    }

    public static IEnumerable<long> NaturalNumbers() => MultiplesOf(1);

    public static IEnumerable<long> Squares() => NaturalNumbers().Select(n => n * n);

    public static List<long> GetPrimeFactors(long number)
    {
        var factors = new List<long>();

        while (number % 2 == 0)
        {
            factors.Add(2);
            number /= 2;
        }

        for (long i = 3; i * i <= number; i += 2)
        {
            while (number % i == 0)
            {
                factors.Add(i);
                number /= i;
            }
        }

        if (number > 1)
            factors.Add(number);

        return factors;
    }

}
