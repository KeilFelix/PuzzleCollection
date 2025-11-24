using PuzzleCollection.Util;
using PuzzleCollection.Util.Grids;

namespace PuzzleCollection.AdventOfCode.Year2022.Day8_TreetopTreeHouse;

public record Tree(int Height);

public static class GridPositionTreeExtensions
{
    public static bool IsVisibleFrom(this Grid<Tree>.Position position, Direction direction)
        => position.WalkValues(direction.ToMove()).All(neighbor => neighbor.Objects.Single().Value.Height < position.Objects.Single().Value.Height);

    public static bool IsVisibleFromAny(this Grid<Tree>.Position position)
        => Directions.All2D.Where(d => !d.IsDiagonal()) // Only 2D orthogonal for Day 8
            .Any(position.IsVisibleFrom);

    public static IEnumerable<Grid<Tree>.Position> VisibleNeighbors(this Grid<Tree>.Position position,
        Direction direction)
    {
        using var enumerator = position.WalkValues(direction.ToMove()).GetEnumerator();
        while (enumerator.MoveNext())
        {
            yield return enumerator.Current;

            if (enumerator.Current.Objects.Single().Value.Height >= position.Objects.Single().Value.Height) break;
        }
    }
    public static int GetViewingDistance(this Grid<Tree>.Position position, Direction direction)
        => position.VisibleNeighbors(direction).Count();

    public static int GetScenicScore(this Grid<Tree>.Position position)
        => Directions.All2D.Where(d => !d.IsDiagonal()) // Only 2D orthogonal for Day 8
            .Select(position.GetViewingDistance)
            .Product();
}

public static class DirectionHelperExtensions
{
    public static bool IsDiagonal(this Direction direction)
    {
        int count = 0;
        if (direction.HasFlag(Direction.Left)) count++;
        if (direction.HasFlag(Direction.Right)) count++;
        if (direction.HasFlag(Direction.Up)) count++;
        if (direction.HasFlag(Direction.Down)) count++;
        if (direction.HasFlag(Direction.Forward)) count++;
        if (direction.HasFlag(Direction.Backward)) count++;
        return count > 1;
    }
}
