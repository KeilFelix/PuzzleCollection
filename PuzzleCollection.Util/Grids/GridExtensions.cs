namespace PuzzleCollection.Util.Grids;

public static class GridExtensions
{
    public static IEnumerable<Grid<TValue>.Position> WalkWhile<TValue>(this Grid<TValue>.Position position, Move move, Func<Grid<TValue>.Position, bool> predicate)
        => position.Walk(move).TakeWhile(predicate);

    public static IEnumerable<Grid<TValue>.Position> WalkValues<TValue>(this Grid<TValue>.Position position, Move move)
        => position.WalkWhile(move, pos => pos.Objects.Any());

    public static Grid<TSource> ToGrid<TSource>(this IEnumerable<IEnumerable<IEnumerable<TSource>>> source)
        => new Grid<TSource>(source);
}