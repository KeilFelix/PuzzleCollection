// Copyright ©️ 2026 - Felix Keil (Awesomni.Codes)
// Licensed under the MIT License.

using PuzzleCollection.Util;
using PuzzleCollection.Util.Grids;

namespace PuzzleCollection.AdventOfCode.Year2022.Day12_HillClimbingAlgorithm;

public static class Input
{
    public static Grid<Marker> GetMap() => File.ReadAllLines("AdventOfCode/Year2022/Day12_HillClimbingAlgorithm/Map.txt")
            .Select(line => line.Trim().Select(c => (c == 'S' ? (Marker)new Start() : c == 'E' ? new End() : new Ground(c - 'a' + 1)).Yield()))
            .ToGrid();
}
