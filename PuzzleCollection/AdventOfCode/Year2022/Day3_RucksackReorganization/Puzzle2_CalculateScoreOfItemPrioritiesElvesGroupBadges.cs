using PuzzleCollection.Util;

namespace PuzzleCollection.AdventOfCode.Year2022.Day3_RucksackReorganization;

public class Puzzle2_CalculateScoreOfItemPrioritiesElvesGroupBadges : IPuzzle
{
    public string GetSolution()
    {
        var itemsByElvesGroups = Input
            .GetRucksacks()
            .Select((Rucksack, Index) => (Rucksack, Index))
            .SplitBy(3)
            .Select(rGrp => rGrp.Select(t => t.Rucksack.Items));

        var itemBadgesFromElvesGroups = itemsByElvesGroups
            .Select(itemsByElvesGroup =>
                itemsByElvesGroup.IntersectAll().Single());

        var sumOfItemBadgePriorities = itemBadgesFromElvesGroups.Select(item => item.Priority).Sum();
        return $"The sum of all item badge priorities from the elves groups is {sumOfItemBadgePriorities}.";
    }
}
