namespace PuzzleCollection.ProjectEuler;

public class Problem39_IntegerRightTriangles : IPuzzle
{
    public string GetSolution()
    {
        var maxPerimeter = 1000;

        var allRectangularTriangles = Enumerable.Range(3, maxPerimeter)
            .SelectMany(p => Enumerable.Range(1, p - 2).Select(a => (p, a)))
            .SelectMany(x => Enumerable.Range(1, x.p - x.a).Select(b => (x.p, x.a, b, c: x.p - x.a - b)));

        var maxTriangles = allRectangularTriangles
            .Where(x => x.a * x.a + x.b * x.b == x.c * x.c)
            .GroupBy(x => x.p)
            .OrderByDescending(x => x.Count())
            .Memoize();

        return $"The perimeter of the right triangle with the most solutions is {maxTriangles.First().Key}";
    }
}
