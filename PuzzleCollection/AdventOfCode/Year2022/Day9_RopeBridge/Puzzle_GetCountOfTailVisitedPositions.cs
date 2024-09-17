using System.Data;
using PuzzleCollection.Util.Grids;

namespace PuzzleCollection.AdventOfCode.Year2022.Day9_RopeBridge;

public abstract class PuzzleBase_GetCountOfTailVisitedPositions : IPuzzle
{
    protected abstract int RopeLength { get; }

    public string GetSolution()
    {
        var ropeHeadMoves = Input.GetRopeHeadMoves();

        var rope = new Rope(new Coord(0,0), RopeLength);

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
