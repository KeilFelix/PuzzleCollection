namespace PuzzleCollection.AdventOfCode.Year2022.Day8_TreetopTreeHouse;

public class Puzzle1_GetCountOfVisibleTrees : IPuzzle
{
    public string GetSolution()
    {
        var trees = Input
            .GetGrid()
            .AllObjects.ToList();

        return $"In the tree grid you can see {trees.Count(tree => tree.Position.IsVisibleFromAny())} from outside.";
    }
}
