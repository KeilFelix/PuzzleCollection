namespace PuzzleCollection.AdventOfCode.Year2023.Day4_Scratchcards;

public static class Input
{
    public static IEnumerable<Scratchcard> Scratchcards
        => File.ReadAllLines("AdventOfCode\\Year2023\\Day4_Scratchcards\\ScratchcardInput.txt")
        .Select(ParseCard);


    public static Scratchcard ParseCard(string cardLine)
    {
        var cardLineSplitted = cardLine.Split(new string[] { "Card ", ": ", " | " }, StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList();

        if(cardLineSplitted.Count != 3)
        {
            throw new ArgumentException(nameof(cardLine));
        }

        if(!int.TryParse(cardLineSplitted[0], out var cardNumber))
        {
            throw new ArgumentException(nameof(cardLine));
        }

        var winningNumbers = cardLineSplitted[1].Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).Select(int.Parse).ToList().AsReadOnly(); //Tryparse and proper exception
        var scratchNumbers = cardLineSplitted[2].Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).Select(int.Parse).ToList().AsReadOnly();
        
        return new Scratchcard(cardNumber, winningNumbers, scratchNumbers);
    }
}
