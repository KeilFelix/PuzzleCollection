namespace PuzzleCollection.Problem_Template;

public class Puzzle1_DoSomething : IPuzzle
{
    public string GetSolution()
    {
        var someThings = Input.GetSomeThings();

        return $"This is a Unit {someThings.First()}";
    }
}
