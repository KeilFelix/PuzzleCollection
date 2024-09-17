using PuzzleCollection.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
namespace PuzzleCollection.AdventOfCode.Year2023.Day2_CubeConundrum;

public record Game(int Id, List<CubeSet> RevealedCubeSets)
{
    public CubeSet MinimalBagCubeSet => new CubeSet(RevealedCubeSets
        .SelectMany(cs => cs.Cubes)
        .GroupBy(cg => cg.Color).Select(colorGroup => colorGroup.MaxBy(cg => cg.Count)!).ToList());
}

public record CubeSet(List<Cubes> Cubes)
{
    public bool IsPossibleToTakeFrom(CubeSet other)
    {
        return Cubes.Join(other.Cubes, cg => cg.Color, cg => cg.Color, (revealedCubes, takenFromCubes) => revealedCubes.Count > takenFromCubes.Count).All(hasMoreCubesRevealed => !hasMoreCubesRevealed);
    }

    public int Power => Cubes.Select(cube => cube.Count).Product();
}

public record Cubes(CubeColor Color, int Count) { }

public enum CubeColor
{
    Red,
    Green,
    Blue,
}
