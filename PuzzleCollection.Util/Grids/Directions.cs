namespace PuzzleCollection.Util.Grids;

[Flags]
public enum Direction
{
    None = 0,

    // Axis X
    Left = 1 << 0,
    Right = 1 << 1,

    // Axis Y
    Down = 1 << 2,
    Up = 1 << 3,

    // Axis Z
    Backward = 1 << 4,
    Forward = 1 << 5
}

public static class Directions
{
    // Orthogonal: Single axis movement
    public static IEnumerable<Direction> Orthogonal => new[]
    {
        Direction.Up, Direction.Down,
        Direction.Left, Direction.Right,
        Direction.Forward, Direction.Backward
    };

    // Diagonal: Movement on exactly 2 axes
    public static IEnumerable<Direction> Diagonal
    {
        get
        {
            var x = new[] { Direction.Left, Direction.Right };
            var y = new[] { Direction.Up, Direction.Down };
            var z = new[] { Direction.Forward, Direction.Backward };

            foreach (var dX in x)
                foreach (var dY in y)
                    yield return dX | dY;

            foreach (var dX in x)
                foreach (var dZ in z)
                    yield return dX | dZ;

            foreach (var dY in y)
                foreach (var dZ in z)
                    yield return dY | dZ;
        }
    }

    // All 2D (X/Y plane only) for backward compatibility logic
    public static IEnumerable<Direction> All2D
    {
        get
        {
            var x = new[] { Direction.Left, Direction.Right, Direction.None };
            var y = new[] { Direction.Up, Direction.Down, Direction.None };

            foreach (var dX in x)
                foreach (var dY in y)
                {
                    if (dX == Direction.None && dY == Direction.None) continue;
                    if (dX != Direction.None && dY != Direction.None) yield return dX | dY; // Diagonals
                    else yield return dX | dY; // Orthogonals
                }
        }
    }

    // All valid directions (Orthogonal + Diagonal + 3D diagonals)
    // Note: This includes corner cases like Forward | Right | Up
    public static IEnumerable<Direction> All
    {
        get
        {
            var x = new[] { Direction.Left, Direction.Right, Direction.None };
            var y = new[] { Direction.Up, Direction.Down, Direction.None };
            var z = new[] { Direction.Forward, Direction.Backward, Direction.None };

            foreach (var dX in x)
                foreach (var dY in y)
                    foreach (var dZ in z)
                    {
                        var d = dX | dY | dZ;
                        if (d != Direction.None) yield return d;
                    }
        }
    }
}
