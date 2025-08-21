using PuzzleCollection.Util;
using System;
using System.Collections.ObjectModel;

namespace PuzzleCollection.ProjectEuler;

public class Problem47_DistinctPrimesFactors : IPuzzle
{
    public string GetSolution()
    {
        var setsToProve = NumberSequences.NaturalNumbers()
            .PairWithPrevious(4).Skip(40)
            .First(HasDistinctPrimeFactors);

        return $"The first number of the set is {setsToProve[0]}";
    }

    private bool HasDistinctPrimeFactors(ReadOnlyCollection<long> set)
    {
        var primeFactorsMultiplied = set
            .Select(n =>
                IntEx.GetPrimeFactors(n)
                .GroupBy(n => n)
                .Select(group => group.Key.Pow(group.Count()))).ToList();

        if(primeFactorsMultiplied.Any(pf => pf.Count() != 4))
        {
            return false ;
        }

        var allPrimeFactors = primeFactorsMultiplied.SelectMany(primeFactors => primeFactors).ToList();

        return allPrimeFactors.Count() == allPrimeFactors.Distinct().Count();
    }
}

