using System;
using PuzzleCollection.Util.Grids;

namespace PuzzleCollection.AdventOfCode.Year2022.Day8_TreetopTreeHouse;

public static class Input
{
    public static Grid<Tree> GetGrid()
        => File.ReadAllLines("AdventOfCode\\Year2022\\Day8_TreetopTreeHouse\\TreeGrid.txt")
            .Select(line => line.Select(singleChar => Enumerable.Repeat(new Tree(int.Parse(singleChar.ToString())), 1)))
            .ToGrid();
}