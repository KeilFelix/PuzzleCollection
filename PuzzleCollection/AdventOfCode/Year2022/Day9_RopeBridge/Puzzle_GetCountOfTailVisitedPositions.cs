// Copyright ©️ 2026 - Felix Keil (Awesomni.Codes)
// Licensed under the MIT License.

using PuzzleCollection.Util.Grids;
using System.Data;

namespace PuzzleCollection.AdventOfCode.Year2022.Day9_RopeBridge;

public abstract class PuzzleBase_GetCountOfTailVisitedPositions : IPuzzle
{
    protected abstract int RopeLength { get; }

    public string GetSolution()
    {
        var ropeHeadMoves = Input.GetRopeHeadMoves();

        using var rope = new Rope(new Coord(0, 0), RopeLength);

        foreach (var ropeHeadMove in ropeHeadMoves)
        {
            for (int i = 0; i < ropeHeadMove.Length; i++)
            {
                rope.MoveRope(ropeHeadMove.Direction);
            }
        }

        var visitedPositions = rope.Grid.AllObjects
            .Where(obj => obj.Value is Trail trail && trail.Of == rope.Tail.Value)
            .GroupBy(obj => obj.Position)
            .Select(group => group.Key);

        return $"The number of places that the tail visited is {visitedPositions.Count()}.";
    }
}

public class Puzzle1_GetCountOfTailVisitedPositions : PuzzleBase_GetCountOfTailVisitedPositions
{
    protected override int RopeLength => 2;
}

public class Puzzle2_GetCountOfTailVisitedPositions : PuzzleBase_GetCountOfTailVisitedPositions
{
    protected override int RopeLength => 10;
}
