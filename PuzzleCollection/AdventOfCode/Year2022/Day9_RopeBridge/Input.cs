using PuzzleCollection.Util.Grids;

namespace PuzzleCollection.AdventOfCode.Year2022.Day9_RopeBridge;

public static class Input
{
    public static IEnumerable<Move> GetRopeHeadMoves()
        => File.ReadAllLines("AdventOfCode\\Year2022\\Day9_RopeBridge\\RopeHeadMoves.txt")
            .Select(MoveFromLine);

    private static Move MoveFromLine(string line)
    {
        var split = line.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);

        return new Move(DirectionFromString(split[0]), int.Parse(split[1]));
    }

    private static Direction DirectionFromString(string str)
    {
        switch (str)
        {
            case "U":
                return Direction.Up;
            case "R":
                return Direction.Right;
            case "D":
                return Direction.Down;
            case "L":
                return Direction.Left;
            default:
                throw new ArgumentException(nameof(str));
        }
    }
}
