// Copyright ©️ 2026 - Felix Keil (Awesomni.Codes)
// Licensed under the MIT License.

namespace PuzzleCollection.Util
{
    public static class NumberSequences
    {
        public static IEnumerable<int> ChampernownesConstantSeries()
        {
            int current = 1;
            while (true)
            {
                foreach (var digit in current.ToDigits().Reverse())
                {
                    yield return digit;
                }
                current++;
            }
        }

        public static IEnumerable<int> Factorials()
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
            if (primeCache == null)
            {
                primeCache = GetDefaultPrimeCache();
            }

            foreach (var prime in primeCache)
            {
                yield return prime;

            }

            for (long candidate = primeCache.Last() + 2; ; candidate += 2)
            {
                var primesToTest = Primes(primeCache).TakeWhile(prime => prime <= Math.Sqrt(candidate));

                if (primesToTest.All(prime => candidate % prime != 0))
                {
                    primeCache.Add(candidate);
                    yield return candidate;
                }
            }
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
                n += basis;
            }
        }

        public static IEnumerable<long> NaturalNumbers() => MultiplesOf(1);

        public static IEnumerable<long> Squares() => NaturalNumbers().Select(n => n * n);

    }
}
