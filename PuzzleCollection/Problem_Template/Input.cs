namespace PuzzleCollection.Problem_Template;

public static class Input
{
    public static IEnumerable<SomeThing> GetSomeThings()
        => File.ReadAllLines("Problem_Template/SomeInput.txt")
            .Select(UnitFromLine);

    private static SomeThing UnitFromLine(string line)
        => new SomeThing();
}
