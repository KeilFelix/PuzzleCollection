using PuzzleCollection.Util;
using PuzzleCollection.Util.Grids;

namespace PuzzleCollection.AdventOfCode.Year2022.Day8_TreetopTreeHouse;

public record Tree(int Height);

public static class GridPositionTreeExtensions
{
    public static bool IsVisibleFrom(this Grid<Tree>.Position position, Direction direction)
        => position.WalkValues(direction.ToMove()).All(neighbor => neighbor.Objects.Single().Value.Height < position.Objects.Single().Value.Height);

    public static bool IsVisibleFromAny(this Grid<Tree>.Position position)
        => Directions.Orthogonal
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
        => Directions.Orthogonal
            .Select(position.GetViewingDistance)
            .Product();
}
