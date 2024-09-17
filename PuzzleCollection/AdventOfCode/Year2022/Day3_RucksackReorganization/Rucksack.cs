namespace PuzzleCollection.AdventOfCode.Year2022.Day3_RucksackReorganization;

public record Rucksack(Rucksack.Compartment First, Rucksack.Compartment Second)
{
    public record Compartment(Compartment.Item[] Items)
    {
        public record Item(int Priority);
    }

    public IEnumerable<Compartment.Item> Items => First.Items.Concat(Second.Items);
}
