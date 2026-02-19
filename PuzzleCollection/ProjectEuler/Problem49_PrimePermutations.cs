// Copyright ©️ 2026 - Felix Keil (Awesomni.Codes)
// Licensed under the MIT License.

using PuzzleCollection.Util;

namespace PuzzleCollection.ProjectEuler;

public class Problem49_PrimePermutations : IPuzzle
{
    public string GetSolution()
    {
        var primes = NumberSequences.Primes()
            .SkipWhile(n => n < 1000)
            .TakeWhile(n => n < 10000)
            .ToList();

        var primePermutations = primes
            .Select(prime => prime
                .ToDigits()
                .Permutations()
                .Select(digits => IntEx.FromDigits(digits))
                .SelectMany(permutation => permutation > 999 && primes.Contains(permutation)
                    ? permutation.Yield()
                    : Enumerable.Empty<long>())
                .Distinct()
                .OrderBy(permutation => permutation)
                .ToList());

        var primeTriplets = primePermutations
            .Where(permutation => permutation.Count > 2)
            .SelectMany(permutations => permutations.Combinations(3));

        var validTriplet = primeTriplets
            .Where(primeTriplet => primeTriplet.Count > 2
                && primeTriplet
                    .PairWithPrevious()
                    .Skip(1)
                    .Select(t => t.Current - t.Previous)
                    .All(difference => difference == 3330)
                && primeTriplet.All(prime => prime != 1487))
            .First();

        var validNumber = IntEx.FromDigits(validTriplet.Select(prime => prime.ToDigits()).Reverse().SelectMany(d => d));
        return $"The sequence is {validNumber}";
    }

}

