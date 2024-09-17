namespace PuzzleCollection;
public static class Puzzles
{
    public static IEnumerable<IPuzzle> All
    {
        get
        {
            return typeof(IPuzzle)
                .Assembly
                .GetTypes()
                .Where(t => typeof(IPuzzle).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract && !t.FullName.Contains("DayX_Template"))
                .OrderByDescending(t => t.FullName)
                .Select(t => Activator.CreateInstance(t))
                .Cast<IPuzzle>();
        }
    }
}
