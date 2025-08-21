using PuzzleCollection.Util;

namespace PuzzleCollection.ProjectEuler;

public class Problem37_TruncatablePrimes : IPuzzle
{
    public string GetSolution()
    {
        var primeCache = NumberSequences.GetDefaultPrimeCache();
        var truncatablePrimes = NumberSequences.Primes(primeCache)
            .SkipWhile(x => x < 10) // 2, 3, 5, 7 are not considered truncatable
            .Where(IsTruncatablePrime)
            .Take(11)
            .ToList();

        var sum = truncatablePrimes.Sum();

        return $"The sum of the eleven truncatable primes is {sum}";

        bool IsTruncatablePrime(long candidate)
        {
            var digits = candidate.ToDigits().ToList();

            var leftDigits = new List<int>();
            var rightDigits = new List<int>();

            for (int i = 0; i < digits.Count - 1; i++)
            {
                rightDigits.Add(digits[i]);
                leftDigits.Insert(0, digits[digits.Count - 1 - i]);
                if(!primeCache.Contains(IntEx.FromDigits(leftDigits)) ||
                 !primeCache.Contains(IntEx.FromDigits(rightDigits)))
                {
                    return false;
                }
            }
            
            return true;
        }

    }

    
}
