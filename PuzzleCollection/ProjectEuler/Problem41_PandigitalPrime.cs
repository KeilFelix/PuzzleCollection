// Copyright ©️ 2026 - Felix Keil (Awesomni.Codes)
// Licensed under the MIT License.

using PuzzleCollection.Util;

namespace PuzzleCollection.ProjectEuler;

public class Problem41_PandigitalPrime : IPuzzle
{
    public string GetSolution()
    {
        //All pandigital numbers with 9 digits are divisible by 3, so we can skip them
        //All pandigital numbers with 8 digits are divisible by 3, so we can skip them
        var primesToTest = NumberSequences.Primes().TakeWhile(x => x <= 7654321).Reverse().Memoize();

        var largestPandigitalPrime = primesToTest.Where(p => p.IsPandigital()).First();

        return $"The largest pandigital prime is {largestPandigitalPrime}";
    }
}
