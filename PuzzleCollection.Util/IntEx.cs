namespace PuzzleCollection.Util;

public static class IntEx
{
    public static IEnumerable<int> ToDigits(this int source, int numBase = 10)
    {
        while (source > 0)
        {
            yield return source % numBase;
            source /= numBase;
        }
    }

    public static IEnumerable<int> ToDigits(this long source, int numBase = 10)
    {
        while (source > 0)
        {
            yield return (int) (source % numBase);
            source /= numBase;
        }
    }

    public static long FromDigits(IEnumerable<int> digits, int numBase = 10)
    {
        long value = 0;
        foreach (var digit in digits.Reverse())
        {
            value = value * numBase + digit;
        }
        
        return value;
    }

    

    public static bool IsPandigital(this long source)
    {
        return source.ToDigits().ToList().IsPandigital();
    }

    public static bool IsPandigital(this List<int> source)
    {
        return source.Count == source.Distinct().Count() && source.Max() == source.Count && source.Min() == 1;
    }


    private static IEnumerable<int> MemoizedFactorials { get; } = NumberSequences.Factorials().Memoize();

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
