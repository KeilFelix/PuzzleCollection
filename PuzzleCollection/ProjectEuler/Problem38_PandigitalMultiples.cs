using PuzzleCollection.Util;

namespace PuzzleCollection.ProjectEuler;

public class Problem38_PandigitalMultiples : IPuzzle
{
    public string GetSolution()
    {
        var pandigitalMultiples = Enumerable.Range(1, 9999)
            .Select(x => GetLargestPandigitalMultipleUnderThreshold(x))
            .ToList();

        var maxPandigitalMultiple = pandigitalMultiples.Max();

        return $"The largest pandigital multiple with 9 digits is {maxPandigitalMultiple}";

        int GetLargestPandigitalMultipleUnderThreshold(int value, int maxDigits = 9)
        {
            var digits = Enumerable.Empty<int>();
            for (int i = 1;  ; i++)
            {
                var nextDigits = (value * i).GetDigits().Reverse().Memoize();
                if(nextDigits.Count() + digits.Count() > maxDigits)
                {
                    break;
                }
                digits = digits.Concat(nextDigits);
            }
            
            digits = digits.Where(d => d != 0).Distinct(); // only 1-9 unique
            return IntEx.FromDigits(digits.Reverse());
        }
    }
}
