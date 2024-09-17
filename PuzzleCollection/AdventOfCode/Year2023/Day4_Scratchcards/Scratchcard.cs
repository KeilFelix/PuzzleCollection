using PuzzleCollection.Util;

namespace PuzzleCollection.AdventOfCode.Year2023.Day4_Scratchcards;

public record Scratchcard(int Number, IReadOnlyCollection<int> WinningNumbers, IReadOnlyCollection<int> ScratchNumbers)
{
    private int? _points;
    private int? _matchingNumberCount;


    public int MatchingNumberCount => _matchingNumberCount ??= WinningNumbers.Intersect(ScratchNumbers).Count();
}

public record ScratchcardWithPoints : Scratchcard
{
    public ScratchcardWithPoints(Scratchcard original) : base(original)
    {
    }

    public int Points => MatchingNumberCount == 0 ? 0 : 2.Pow(MatchingNumberCount - 1);
}

public record ScratchcardWithWonCopies : Scratchcard
{
    protected ScratchcardWithWonCopies(Scratchcard original, List<ScratchcardWithWonCopies> Copies) : base(original)
    {
        this.Copies = Copies;
    }

    public List<ScratchcardWithWonCopies> Copies { init; get; }


    //List of Scratchcards should be refactored to hold this cache, this design is broken if Scratchcards are created from different sources
    private static Dictionary<int,ScratchcardWithWonCopies> dirtyScratchCardCache = new();

    public static ScratchcardWithWonCopies CreateFromScratchcards(Scratchcard scratchcard, List<Scratchcard> scratchcards)
    {
        var indexOfScratchcard = scratchcards.IndexOf(scratchcard);
        if(dirtyScratchCardCache.TryGetValue(indexOfScratchcard, out var cachedCard))
        {
            return cachedCard;
        }

        int countOfNextScratchcards = scratchcard.MatchingNumberCount;
        if (indexOfScratchcard + 1 + countOfNextScratchcards > scratchcards.Count)
        {
            countOfNextScratchcards = scratchcards.Count - indexOfScratchcard - 1;

        }

        var wonCopies = scratchcards.GetRange(indexOfScratchcard + 1, countOfNextScratchcards);

        var scratchcardWithWonCopies = new ScratchcardWithWonCopies(scratchcard, wonCopies.Select(sc => CreateFromScratchcards(sc, scratchcards)).ToList());
        dirtyScratchCardCache.Add(indexOfScratchcard, scratchcardWithWonCopies);

        return scratchcardWithWonCopies;
    }

    public int TotalCardCount => 1 + Copies.Sum(sc => sc.TotalCardCount);
}
