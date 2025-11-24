using System.Linq;

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
    Forward = 1 << 5,

    // Axis 4 (W)
    Ana = 1 << 6,
    Kata = 1 << 7,

    // Axis 5 (V)
    VPos = 1 << 8,
    VNeg = 1 << 9,

    // Axis 6 (U)
    UPos = 1 << 10,
    UNeg = 1 << 11
}

public static class Directions
{
    public static Direction[] X => [Direction.Left, Direction.Right];
    public static Direction[] Y => [Direction.Down, Direction.Up];
    public static Direction[] Z => [Direction.Backward, Direction.Forward];
    public static Direction[] W => [Direction.Kata, Direction.Ana];
    public static Direction[] V => [Direction.VNeg, Direction.VPos];
    public static Direction[] U => [Direction.UNeg, Direction.UPos];

    public static Direction[][] Dimensions => [X, Y, Z, W, V, U];

    public static IEnumerable<Direction> Orthogonal(int dimensions = 2)
        => Dimensions.Take(dimensions).SelectMany(axis => axis);

    // Diagonal: Movement on exactly 2 axes
    public static IEnumerable<Direction> Diagonal(int dimensions = 2)
    {
        var axes = Dimensions.Take(dimensions).ToList();

        for (int i = 0; i < axes.Count; i++)
        {
            for (int j = i + 1; j < axes.Count; j++)
            {
                foreach (var d1 in axes[i])
                {
                    foreach (var d2 in axes[j])
                    {
                        yield return d1 | d2;
                    }
                }
            }
        }
    }

    public static IEnumerable<Direction> All(int dimensions = 2)
    {
        var activeAxes = Dimensions.Take(dimensions).ToList();

        return GetAllCombinations(activeAxes, 0, Direction.None)
            .Where(d => d != Direction.None);
    }

    private static IEnumerable<Direction> GetAllCombinations(List<Direction[]> axes, int currentIndex, Direction currentDirection)
    {
        if (currentIndex >= axes.Count)
        {
            yield return currentDirection;
            yield break;
        }

        foreach (var result in GetAllCombinations(axes, currentIndex + 1, currentDirection))
        {
            yield return result;
        }

        foreach (var dir in axes[currentIndex])
        {
            foreach (var result in GetAllCombinations(axes, currentIndex + 1, currentDirection | dir))
            {
                yield return result;
            }
        }
    }
}
