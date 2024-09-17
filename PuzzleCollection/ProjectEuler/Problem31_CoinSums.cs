namespace PuzzleCollection.ProjectEuler;

public class Problem31_CoinSums : IPuzzle
{
    public record Coin(int value);

    public int WaysToMakeChange(int amount, IEnumerable<Coin> coins)
    {
        if (amount == 0)
        {
            return 1;
        }
        if (amount < 0 || !coins.Any())
        {
            return 0;
        }

        var coin = coins.First();

        return WaysToMakeChange(amount - coin.value, coins) + WaysToMakeChange(amount, coins.Skip(1));
    }

    public string GetSolution()
    {
        int targetAmount = 200;
        var coins = (new[] { 1, 2, 5, 10, 20, 50, 100, 200 }).Select(value => new Coin(value));

        int ways = WaysToMakeChange(targetAmount, coins);
        

        return $"Es gibt {ways} Möglichkeiten, {targetAmount} Pence mit den Münzen {string.Join(", ", coins.Select(c => c.value))} zu bilden.";
    }
}
