namespace PuzzleCollection.AdventOfCode.Year2022.Day2_RockPaperScissors;

public abstract class Puzzle_CalculateScoreFromStrategyGuide : IPuzzle
{
    protected abstract Input.Interpretation Interpretation { get; }

    public string GetSolution()
    {
        var totalScore = Input
            .GetGames(Interpretation)
            .Select(game => game.Score)
            .Sum();

        return $"The RockPaperScissors strategy guide's total score is {totalScore}.";
    }
}

public class Puzzle1_CalculateScoreFromStrategyGuideSecondSignAsChoice : Puzzle_CalculateScoreFromStrategyGuide
{
    protected override Input.Interpretation Interpretation => Input.Interpretation.SecondSignAsChoice;
}

public class Puzzle2_CalculateScoreFromStrategyGuideSecondSignAsResult : Puzzle_CalculateScoreFromStrategyGuide
{
    protected override Input.Interpretation Interpretation => Input.Interpretation.SecondSignAsResult;
}

