using PuzzleCollection.Util;

namespace PuzzleCollection.ProjectEuler;

public class Problem40_ChampernownesConstant : IPuzzle
{
    public string GetSolution()
    {
        var champornownesConstant = IntEx.ChampernownesConstantSeries().Memoize();
        
        var neededDigitIndexes = new List<int> { 1, 10, 100, 1000, 10000, 100000, 1000000 };

        var neededDigits = neededDigitIndexes.Select(d => champornownesConstant.ElementAt(d-1)).Memoize();

        return $"The product of the Champernowne's Constant digits is {neededDigits.Product()}";
    }
}
