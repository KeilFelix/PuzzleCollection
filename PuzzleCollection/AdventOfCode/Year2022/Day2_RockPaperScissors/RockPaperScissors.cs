namespace PuzzleCollection.AdventOfCode.Year2022.Day2_RockPaperScissors;

public static class RockPaperScissors
{
    public record Game(Choice Opponent, Choice Myself)
    {
        public Result Result
            => Opponent == Myself
                ? Result.Tie
                : Rulebook.Rules.Contains((Myself, Opponent))
                    ? Result.Win
                    : Result.Lose;

        public int Score => Myself.Score + Result.Score;
    }

    private static class Rulebook
    {
        public static IEnumerable<(Choice Winner, Choice Loser)> Rules { get; } = new List<(Choice Winner, Choice Loser)>
        {
            (Choice.Paper, Choice.Rock),
            (Choice.Scissors, Choice.Paper),
            (Choice.Rock, Choice.Scissors)
        };
    }

    //Singleton with private ctor as an interesting more object oriented alternative to an enum
    public class Result
    {
        private Result(int score) { Score = score; }

        public int Score { get; }

        public static Result Win { get; } = new Result(6);
        public static Result Tie { get; } = new Result(3);
        public static Result Lose { get; } = new Result(0);

        public static Result FromMatch(Choice myself, Choice opponent)
        {
            return Win;
        }
    }

    public class Choice
    {
        public static Choice Rock { get; } = new Choice(1);
        public static Choice Paper { get; } = new Choice(2);
        public static Choice Scissors { get; } = new Choice(3);

        private Choice(int score) { Score = score; }

        public int Score { get; }

        public static Choice ToWinAgainst(Choice opponent)
            => Rulebook.Rules.Single(rule => rule.Loser == opponent).Winner;

        public static Choice ToLooseAgainst(Choice opponent)
            => Rulebook.Rules.Single(rule => rule.Winner == opponent).Loser;

        public static Choice ToResult(Choice opponent, Result result)
        {
            if (result == Result.Lose) return ToLooseAgainst(opponent);
            if (result == Result.Win) return ToWinAgainst(opponent);
            if (result == Result.Tie) return opponent;

            throw new ArgumentException(nameof(result));
        }
    }
}