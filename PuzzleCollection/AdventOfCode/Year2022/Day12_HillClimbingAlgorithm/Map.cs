// Copyright ©️ 2026 - Felix Keil (Awesomni.Codes)
// Licensed under the MIT License.

using PuzzleCollection.Util.Grids;

namespace PuzzleCollection.AdventOfCode.Year2022.Day12_HillClimbingAlgorithm
{
    public record Marker() { }
    public record Start() : Ground(0) { }
    public record End() : Ground(26) { }
    public record Ground(int Height) : Marker { }
    public record Path(List<Grid<Marker>.Position> Positions) : Marker { }
    public record Walker(Path Path) { }
}
