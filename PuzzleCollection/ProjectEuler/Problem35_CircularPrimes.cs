using PuzzleCollection.Util;
using System.Collections.Immutable;

namespace PuzzleCollection.ProjectEuler;

public class Problem35_CircularPrimes : IPuzzle
{
    public string GetSolution()
    {
        var primesBelowOneMillion = IntEx.Primes().TakeWhile(x => x < 1000000).ToImmutableSortedSet();

        var circularPrimes = primesBelowOneMillion
            .Where(IsCircularPrime)
            .ToList();

        return $"The are {circularPrimes.Count} below one million";


        bool IsCircularPrime(int value)
        {
            var digits = value.GetDigits().ToList();

            return Enumerable.Range(1, digits.Count)
                .Select(x => IntEx.FromDigits(digits.Skip(x).Concat(digits.Take(x))))
                .All(x => primesBelowOneMillion.Contains(x));
        }
    }
}
