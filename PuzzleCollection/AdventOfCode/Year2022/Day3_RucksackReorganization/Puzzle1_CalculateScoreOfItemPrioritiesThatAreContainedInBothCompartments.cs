namespace PuzzleCollection.AdventOfCode.Year2022.Day3_RucksackReorganization;

public class Puzzle1_CalculateScoreOfItemPrioritiesThatAreContainedInBothCompartments : IPuzzle
{
    public string GetSolution()
    {
        var itemPrioritySumOfItemsContainedInBothCompartments = Input
            .GetRucksacks()
            .Select(rucksack => rucksack.First.Items.First(item => rucksack.Second.Items.Any(secondItem => secondItem == item)).Priority)
            .Sum();

        return $"The sum of all item priorities that are contained in both compartments is {itemPrioritySumOfItemsContainedInBothCompartments}.";
    }
}
