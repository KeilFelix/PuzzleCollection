namespace PuzzleCollection.Util.Grids;

public enum Direction
{
    Up,
    UpRight,
    Right,
    DownRight,
    Down,
    DownLeft,
    Left,
    UpLeft
}

public static class Directions
{
    public static IEnumerable<Direction> All => Enum.GetValues<Direction>();
    public static IEnumerable<Direction> Orthogonal => new[] { Direction.Up, Direction.Right, Direction.Down, Direction.Left, };
    public static IEnumerable<Direction> Diagonal => new[] { Direction.UpRight, Direction.DownRight, Direction.DownLeft, Direction.UpLeft };
}
