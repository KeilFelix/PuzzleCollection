using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleCollection.AdventOfCode.Year2023.Day5_IfYouGiveASeedAFertilizer;

public class Puzzle_FindLowestLocationOfSeedBase
{
    protected string GetSolution(bool seedRanges)
    {
        var almanac = Input.GetAlmanac(seedRanges);

        var currentCategoryNumbers = (Numbers: almanac.SeedRanges, Category: "seed");

        while (currentCategoryNumbers.Category != "location")
        {
            currentCategoryNumbers = almanac.Map(currentCategoryNumbers.Numbers, currentCategoryNumbers.Category);
        }
        var lowestLocationOfSeed = currentCategoryNumbers.Numbers.Select(r => r.Start).Min();
        return $"The lowest location of a seed is {lowestLocationOfSeed}.";
    }
}

public class Puzzle1_FindLowestLocationOfSeed : Puzzle_FindLowestLocationOfSeedBase, IPuzzle
{
    public string GetSolution() => GetSolution(false);
}

public class Puzzle2_FindLowestLocationOfSeed : Puzzle_FindLowestLocationOfSeedBase, IPuzzle
{
    public string GetSolution() => GetSolution(true);
}
