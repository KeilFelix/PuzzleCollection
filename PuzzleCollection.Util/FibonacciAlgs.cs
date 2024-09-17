using System.Collections.Concurrent;
using System.Numerics;

namespace PuzzleCollection.Util;

public class FibonacciAlgs
{
    public static void InitCaches()
    {
        //fibonacciMemo = new Dictionary<BigInteger, BigInteger> { { 0, 0 }, { 1, 1 } };
        //lucasMemo = new Dictionary<BigInteger, BigInteger> { { 0, 2 }, { 1, 1 } };
        fibCache = new Dictionary<BigInteger, BigInteger> { { 0, 0 }, { 1, 1 } };
        lucasMemoConcurrent = new ConcurrentDictionary<BigInteger, BigInteger>();
        lucasMemoConcurrent[0] = 2;
        lucasMemoConcurrent[1] = 1;
        //lucasMemoConcurrent[2] = 3;
        //lucasMemoConcurrent[3] = 4;
        fibonacciMemoConcurrent = new ConcurrentDictionary<BigInteger, BigInteger>();
        fibonacciMemoConcurrent[0] = 0;
        fibonacciMemoConcurrent[1] = 1;

        fibMemoCogitoConcurrent = new ConcurrentDictionary<BigInteger, BigInteger>();
        fibMemoCogitoConcurrent[0] = 0;
        fibMemoCogitoConcurrent[1] = 1;
        //fibMemoCogitoConcurrent[2] = 1;
        //fibMemoCogitoConcurrent[3] = 2;
    }
    public static IEnumerable<BigInteger> Lucas
    {
        get
        {
            BigInteger a = 2;
            BigInteger b = 1;
            while (true)
            {
                yield return a;
                BigInteger c = a + b;
                a = b;
                b = c;
            }
        }
    }

    public static IEnumerable<BigInteger> Fibonacci
    {
        get
        {
            BigInteger a = 0;
            BigInteger b = 1;
            while (true)
            {
                yield return a;
                BigInteger c = a + b;
                a = b;
                b = c;
            }
        }
    }

    public static IEnumerable<BigInteger> MemoizedFibonacci { get; } = Fibonacci.Memoize();
    public static IEnumerable<BigInteger> MemoizedLucas { get; } = Lucas.Memoize();


    public static BigInteger MemoizedFibonacciOverLucas(long n)
    {
        if (fibonacciMemoConcurrent.ContainsKey(n))
        {
            return fibonacciMemoConcurrent[n];
        }
        // Uses identity for even n: F(2n) = F(n) * L(n)
        if (n % 2 == 0)
        {
            Console.WriteLine($"F({n/2})");
            Console.WriteLine($"L({n / 2})");
            return fibonacciMemoConcurrent[n] = MemoizedFibonacciOverLucas(n / 2) * MemoizedRippleLucas(n / 2);
        }

        // Uses identity for odd n: F(n) = 2F(n+1) - L(n)
        //return fibonacciMemo[n] = 2 * MemoizedFibonacciOverLucas(n+1) - MemoizedRippleLucas(n);

        // Uses identity for odd n: F(n) = (L(n-1) + L(n+1))/5
        //return fibonacciMemoConcurrent[n] = (MemoizedRippleLucas(n - 1) + MemoizedRippleLucas(n+ 1)) / 5;

        // Uses identity for odd n: F(2n+1) = F(n + 1)^2 + F(n)^2
        Console.WriteLine($"F({n / 2})");
        Console.WriteLine($"F({n / 2 + 1})");
        return fibonacciMemoConcurrent[n] = BigInteger.Pow(MemoizedFibonacciOverLucas(n / 2), 2) + BigInteger.Pow(MemoizedFibonacciOverLucas(n / 2 + 1), 2);
    }


    public static BigInteger MemoizedRippleLucas(long n)
    {
        if (lucasMemoConcurrent.ContainsKey(n))
        {

            return lucasMemoConcurrent[n];
        }

        var n2 = n / 2;
        var n2p1 = (n + 1) / 2;

        var p = (n2 % 2) == 0 ? 2 : -2;

        // Uses identity for even n: L(2n) = L(n)^2 - 2(-1)^n
        if (n2 == n2p1)
        {
            Console.WriteLine($"L({n2})");
            return lucasMemoConcurrent[n] = BigInteger.Pow(MemoizedRippleLucas(n2), 2) - p;
        }

        // Uses identity for odd n: L(2n) = L(n+1)^2 - L(n)^2 + 4(-1)^n
        Console.WriteLine($"L({n2p1})");
        Console.WriteLine($"L({n2})");
        return lucasMemoConcurrent[n] = BigInteger.Pow(MemoizedRippleLucas(n2p1), 2) - BigInteger.Pow(MemoizedRippleLucas(n2), 2) + (p*2);

        // Uses identity for odd n: L(m + n) = L(m+1)F(n) + L(m)F(n-1) where m+1 is chosen to be the next fitting power of 2
        //var nextM = powerOf2s.First(p => p <= (n / 2));
        //var nextN = n - nextM;
        //return lucasMemoConcurrent[n] = MemoizedRippleLucas(nextM + 1) * MemoizedFibonacciOverLucas(nextN) + MemoizedRippleLucas(nextM) * MemoizedFibonacciOverLucas(nextN - 1);

    }

    //private static Dictionary<BigInteger, BigInteger> lucasMemo = new Dictionary<BigInteger, BigInteger> { { 0, 2 }, { 1, 1 } };
    //private static Dictionary<BigInteger, BigInteger> fibonacciMemo = new Dictionary<BigInteger, BigInteger> { { 0, 0 }, { 1, 1 } };

    private static List<long> powerOf2s;

    static FibonacciAlgs()
    {
        powerOf2s = PowerOf2s.Take(31).Reverse().ToList();
    }
    private static IEnumerable<long> PowerOf2s
    {
        get
        {
            int cur = 1;
            while (true)
            {
                yield return cur;
                cur *= 2;
            }
        }
    }

    private static Dictionary<BigInteger, BigInteger> fibCache = new Dictionary<BigInteger, BigInteger> { { 0, 0 }, { 1, 1 } };

    public static BigInteger MemoizedFibonacciOverSquared(long n)
    {
        if (fibCache.ContainsKey(n)) return fibCache[n];

        if (n % 2 == 0)
        {
            return fibCache[n] = ((2 * MemoizedFibonacciOverSquared(n / 2 - 1)) + MemoizedFibonacciOverSquared(n / 2)) * MemoizedFibonacciOverSquared(n / 2);
        }
        else
        {
            return fibCache[n] = (MemoizedFibonacciOverSquared((n - 1) / 2) * MemoizedFibonacciOverSquared((n - 1) / 2)) + (MemoizedFibonacciOverSquared((n + 1) / 2) * MemoizedFibonacciOverSquared((n + 1) / 2));
        }
    }

    private static ConcurrentDictionary<BigInteger, BigInteger> fibMemoCogitoConcurrent = new ConcurrentDictionary<BigInteger, BigInteger>();

    public static BigInteger MemoizedFibonacciCogitoErgo(long n)
    {
        if (fibMemoCogitoConcurrent.ContainsKey(n)) return fibMemoCogitoConcurrent[n];

        // n is a multiple of 3
        if (n % 3 == 0)
        {
            long third = n / 3;
            BigInteger fibThird = MemoizedFibonacciCogitoErgo(third);
            return fibMemoCogitoConcurrent[n] = 5 * BigInteger.Pow(fibThird, 3) + 3 * BigInteger.Pow(-1, (int)third) * fibThird;
        }

        // even n
        if ((n & 1) == 0)
            return fibMemoCogitoConcurrent[n] = BigInteger.Pow(MemoizedFibonacciCogitoErgo((n >> 1) + 1), 2) - BigInteger.Pow(MemoizedFibonacciCogitoErgo((n >> 1) - 1), 2);

        // for odd n
        return fibMemoCogitoConcurrent[n] = BigInteger.Pow(MemoizedFibonacciCogitoErgo((n >> 1) + 1), 2) + BigInteger.Pow(MemoizedFibonacciCogitoErgo(n >> 1), 2);
    }

    public static async Task<BigInteger> MemoizedFibonacciOverLucasParallel(long n)
    {
        if (fibonacciMemoConcurrent.ContainsKey(n)) return fibonacciMemoConcurrent[n];
        if (n < 131072) return MemoizedFibonacciOverLucas(n);

        // Uses identity for even n: F(2n) = F(n) * L(n)
        if (n % 2 == 0)
        {
            var resultsEven = await Task.WhenAll(
                Task.Run(async () => await MemoizedFibonacciOverLucasParallel(n / 2)),
                Task.Run(async () => await MemoizedRippleLucasParallel(n / 2)));

            return fibonacciMemoConcurrent[n] = resultsEven[0] * resultsEven[1];
        }

        // Uses identity for odd n: F(n) = 2F(n+1) - L(n)
        //var resultsOdd = await Task.WhenAll(
        //    Task.Run(async () => 2 * await MemoizedFibonacciOverLucasParallel(n + 1)), 
        //    Task.Run(async () => await MemoizedFibonacciOverLucasParallel(n)));
        //return fibonacciMemo[n] = resultsOdd[0] - resultsOdd[1];

        // Uses identity for odd n: F(2n+1) = F(n + 1)^2 + F(n)^2
        var resultsOdd = await Task.WhenAll(
            Task.Run(async () => BigInteger.Pow(await MemoizedFibonacciOverLucasParallel(n / 2), 2)),
            Task.Run(async () => BigInteger.Pow(await MemoizedFibonacciOverLucasParallel(n / 2 + 1), 2)));
        return fibonacciMemoConcurrent[n] = resultsOdd[0] + resultsOdd[1];
    }

    public static async Task<BigInteger> MemoizedRippleLucasParallel(long n)
    {
        if (lucasMemoConcurrent.ContainsKey(n)) return lucasMemoConcurrent[n];
        if (n <= 131072) return MemoizedRippleLucas(n);

        var n2 = n / 2;
        var n2p1 = (n + 1) / 2;

        var p = (n2 % 2) == 0 ? 2 : -2;

        // Uses identity for even n: L(2n) = L(n)^2 - 2(-1)^n
        if (n2 == n2p1) return lucasMemoConcurrent[n] = BigInteger.Pow(await MemoizedRippleLucasParallel(n2), 2) - p;

        // Uses identity for odd n: L(2n) = L(n+1)^2 - L(n)^2 + 4(-1)^n
        var resultsOdd = await Task.WhenAll(
            Task.Run(async () => BigInteger.Pow(await MemoizedRippleLucasParallel(n2p1), 2) + (p * 2)),
            Task.Run(async () => BigInteger.Pow(await MemoizedRippleLucasParallel(n2), 2)));
        return lucasMemoConcurrent[n] = resultsOdd[0] - resultsOdd[1];

        // Uses identity for odd n: L(m + n) = L(m+1)F(n) + L(m)F(n-1) where m+1 is chosen to be the next fitting power of 2
        //var nextM = powerOf2s.First(p => p <= (n / 2));
        //var nextN = n - nextM;
        //var resultsOdd = await Task.WhenAll(
        //    Task.Run(async () => await MemoizedRippleLucasParallel(nextM + 1)),
        //    Task.Run(async () => await MemoizedFibonacciOverLucasParallel(nextN)),
        //    Task.Run(async () => await MemoizedRippleLucasParallel(nextM)),
        //    Task.Run(async () => await MemoizedFibonacciOverLucasParallel(nextN - 1))
        //    );
        //return lucasMemoConcurrent[n] = resultsOdd[0] * resultsOdd[1] + resultsOdd[2] * resultsOdd[3];
    }

    private static ConcurrentDictionary<BigInteger, BigInteger> lucasMemoConcurrent = new ConcurrentDictionary<BigInteger, BigInteger> ();
    private static ConcurrentDictionary<BigInteger, BigInteger> fibonacciMemoConcurrent = new ConcurrentDictionary<BigInteger, BigInteger>();

}
