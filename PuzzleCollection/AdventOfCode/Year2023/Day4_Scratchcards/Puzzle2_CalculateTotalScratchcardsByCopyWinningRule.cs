namespace PuzzleCollection.AdventOfCode.Year2023.Day4_Scratchcards;

public class Puzzle2_CalculateTotalScratchcardsByCopyWinningRule : IPuzzle
{
    record ScratchcardWithCopies(Scratchcard scratchcard, List<ScratchcardWithCopies> copies) { }
    public string GetSolution()
    {
        
        var scratchcards = Input.Scratchcards.ToList();

        var scratchcardsWithCopies = scratchcards.Select(sc => ScratchcardWithWonCopies.CreateFromScratchcards(sc, scratchcards)).ToList();
        
        var totalCardCount = scratchcardsWithCopies.Select(sc => sc.TotalCardCount).Sum();

        return $"The total number of cards with all copies is {totalCardCount}.";


    }
}
