namespace PuzzleCollection.AdventOfCode.Year2023.Day4_Scratchcards;

public class Puzzle1_CalculateTotalScratchcardPoints : IPuzzle
{
    public string GetSolution()
    {
        var scratchcards = Input.Scratchcards.Select(sc => new ScratchcardWithPoints(sc)).Memoize();

        var totalNumberOfPoints = scratchcards.Select(sc => sc.Points)
            .Sum();
        return $"The total number of points is {totalNumberOfPoints}.";
    }
}
