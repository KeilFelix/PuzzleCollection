using PuzzleCollection.Util;
using System;
using System.Collections.ObjectModel;

namespace PuzzleCollection.ProjectEuler;

public class Problem48_SelfPowers : IPuzzle
{
    public string GetSolution()
    {
        var modSum = NumberSequences.NaturalNumbers()
            .Take(1000)
            .Select(i => Enumerable.Repeat(i, (int)i).Aggregate((a, b) => (a*b) % 10000000000))
            .Aggregate((a, b) => (a + b) % 10000000000);


        return $"The digits are {modSum}";
    }

}

