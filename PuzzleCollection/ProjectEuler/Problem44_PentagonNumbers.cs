using PuzzleCollection.Util;

namespace PuzzleCollection.ProjectEuler;

public class Problem44_PentagonNumbers : IPuzzle
{
    public string GetSolution()
    {
            
        var pentagonNumbers = NumberSequences.PentagonalNumbers().Take(10000).ToList();
        for (int i = 0; i < pentagonNumbers.Count; i++)
        {
            for (int j = i; j < pentagonNumbers.Count; j++)
            {
                var p1 = pentagonNumbers[i];
                var p2 = pentagonNumbers[j];
                var sum = p1 + p2;
                if (pentagonNumbers.Contains(sum) && pentagonNumbers.Contains(p2 - p1))
                {
                    return $"The difference is {p2 - p1}";
                }
            }
        }

        return "No solution found";
    }

}

