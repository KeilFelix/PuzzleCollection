using PuzzleCollection.Util;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace PuzzleCollection.ProjectEuler;

public class Problem50_ConsecutivePrimeSum : IPuzzle
{
    public string GetSolution()
    {
        var maxPrimeSum = 1000000;
        var primes = NumberSequences.Primes()
            .TakeWhile(prime => prime < maxPrimeSum)
            .ToList();

        bool IsPrime(long number) => primes.Contains(number);


        int highestPossibleConsecutivePrimesCount = 556;


        var highestConsecutivePrimesAscending = Enumerable.Range(1, highestPossibleConsecutivePrimesCount)
            .Reverse()
            .SelectMany(i =>
                primes.Skip(i).Scan(
                    (Sum: primes.Take(i).Sum(), List: primes.Take(i).ToList()),
                    (current, nextPrime) => (Sum: current.Sum - current.List[0] + nextPrime, List: current.List.Skip(1).Append(nextPrime).ToList()))
                .Prepend((Sum: primes.Take(i).Sum(), List: primes.Take(i).ToList()))
                .TakeWhile(p => p.Sum < maxPrimeSum)
                .Where(p => IsPrime(p.Sum)))
            .First();


        //for (int i = 556; i >= 1; i--)
        //{
        //    var initialSumOfConsecutivePrimes = primes.Take(i).Sum();
        //    if (IsPrime(initialSumOfConsecutivePrimes))
        //    {
        //        primeBuildFromHighestConsecutivePrimesSum = initialSumOfConsecutivePrimes;
        //        break;
        //    }
        //    primes.Scan(0L, (current, prime) => current + prime)
        //        .TakeWhile(sum => sum < 1000000)
        //        .Where(sum => sum == initialSumOfConsecutivePrimes)
        //        .Select(sum => primes.TakeWhile(p => p <= sum).Count())
        //        .Max();
        //}
        //var highestSumNum = primes.Scan(0L, (current, prime) => current + prime).TakeWhile(sum => sum < 1000000).Count();

        return $"The prime with the highest consecutive prime sum is {highestConsecutivePrimesAscending.Sum}";
    }

}

