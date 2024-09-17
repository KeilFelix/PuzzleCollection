using PuzzleCollection.Util;

namespace PuzzleCollection.AdventOfCode.Year2022.Day1_CalorieCounting;

public static class Input
{
    public static IEnumerable<Elf> GetElvesCaloryInventory()
        => File.ReadAllLines("AdventOfCode\\Year2022\\Day1_CalorieCounting\\ElvesCaloryInventory.txt")
            .SplitAfter(string.IsNullOrWhiteSpace)
            .Select((rawChunk, index) => new Elf(index, GetCaloriesFromChunk(rawChunk)));

    private static int GetCaloriesFromChunk(IEnumerable<string> chunk)
        => chunk
            .SkipLast(1)
            .Select(line => int.Parse(line))
            .Sum();
}