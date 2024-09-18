namespace PuzzleCollection.AdventOfCode.Year2022.Day2_RockPaperScissors;

public static class Input
{
    public enum Interpretation
    {
        SecondSignAsChoice,
        SecondSignAsResult
    }
    public static IEnumerable<RockPaperScissors.Game> GetGames(Interpretation interpretation)
        => File.ReadAllLines("AdventOfCode/Year2022/Day2_RockPaperScissors/RockPaperScissorsGamesStrategyGuide.txt")
            .Select(line => GetGameFromLine(line, interpretation));

    private static RockPaperScissors.Game GetGameFromLine(string line, Interpretation interpretation)
    {
        var splittedLine = line.Split(' ');

        var opponentChoice = ChoiceFromRaw(splittedLine[0]);
        var mySelfChoice = interpretation == Interpretation.SecondSignAsChoice
            ? ChoiceFromRaw(splittedLine[1])
            : RockPaperScissors.Choice.ToResult(opponentChoice, ResultFromRaw(splittedLine[1]));
        return new RockPaperScissors.Game(opponentChoice, mySelfChoice);
    }


    private static RockPaperScissors.Choice ChoiceFromRaw(string rawChoice)
    {
        if (rawChoice is "A" or "X")
        {
            return RockPaperScissors.Choice.Rock;
        }

        if (rawChoice is "B" or "Y")
        {
            return RockPaperScissors.Choice.Paper;
        }

        if (rawChoice is "C" or "Z")
        {
            return RockPaperScissors.Choice.Scissors;
        }

        throw new ArgumentException(nameof(rawChoice));
    }

    private static RockPaperScissors.Result ResultFromRaw(string rawResult)
    {
        if (rawResult is "X")
        {
            return RockPaperScissors.Result.Lose;
        }

        if (rawResult is "Y")
        {
            return RockPaperScissors.Result.Tie;
        }

        if (rawResult is "Z")
        {
            return RockPaperScissors.Result.Win;
        }

        throw new ArgumentException(nameof(rawResult));
    }
}