using PuzzleCollection.Util;

namespace PuzzleCollection.ProjectEuler;

public class Problem34_DigitFactorials : IPuzzle
{
    public string GetSolution()
    {
        var maxValue = Enumerable.Range(0, 9).Select(x => x.Factorial()).Sum();
        var values = Enumerable.Range(3, maxValue);

        var digitFactorials = values
            .Where(IsDigitFactorial)
            .Memoize();

        var sum = digitFactorials.Sum();

        return $"The sum of all digit factorials is {sum}.";


        bool IsDigitFactorial(int value)
        {
            var digits = value.GetDigits().ToList();

            var factorialSum = digits.Select(x => x.Factorial()).Sum();
            return value == factorialSum;
        }
    }
}
