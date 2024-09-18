using System.Text;

namespace PuzzleCollection.AdventOfCode.Year2022.Day3_RucksackReorganization;

public static class Input
{
    public static IEnumerable<Rucksack> GetRucksacks()

        => File.ReadAllLines("AdventOfCode/Year2022/Day3_RucksackReorganization/Rucksacks.txt")
            .Select(GetRucksackFromLine);

    private static Rucksack GetRucksackFromLine(string line)
    {
        int AsciiValueToItemPriority(byte asciiValue)
        {
            if (asciiValue is >= 97 and <= 122)
            {
                return asciiValue - 96;
            }

            if (asciiValue is >= 65 and <= 90)
            {
                return asciiValue - 38;
            }

            throw new ArgumentException(nameof(asciiValue));
        }
        var itemPriorities = Encoding.ASCII.GetBytes(line)
            .Select(AsciiValueToItemPriority)
            .ToArray();

        var compartmentSize = itemPriorities.Length / 2;

        return new Rucksack(
            new Rucksack.Compartment(itemPriorities.Take(compartmentSize).Select(prio => new Rucksack.Compartment.Item(prio))
                .ToArray()),
            new Rucksack.Compartment(itemPriorities.Skip(compartmentSize).Select(prio => new Rucksack.Compartment.Item(prio))
                .ToArray())
        );
    }
}