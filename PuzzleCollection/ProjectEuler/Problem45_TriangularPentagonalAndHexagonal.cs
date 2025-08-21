using PuzzleCollection.Util;
using System;

namespace PuzzleCollection.ProjectEuler;

public class Problem45_TriangularPentagonalAndHexagonal : IPuzzle
{
    public string GetSolution()
    {
            
        var triangleNumbers = NumberSequences.TriangleNumbers().Skip(285).Memoize();
        var pentagonalNumbers = NumberSequences.PentagonalNumbers().Skip(165).Memoize();
        var hexagonalNumbers = NumberSequences.HexagonalNumbers().Skip(143).Memoize();


        var equals = GetEquals(triangleNumbers, pentagonalNumbers, hexagonalNumbers);


        return $"The next is {equals.First()}";
    }

    public static IEnumerable<long> GetEquals(params IEnumerable<long>[] orderedSequences)
    {

        var enumerators = orderedSequences.Select(s => s.GetEnumerator()).Do(e => e.MoveNext()).ToList();

        while (true)
        {
            if (enumerators.DistinctBy(e => e.Current).Count() == 1)
            {
                yield return enumerators.First().Current;
            }

            var min = enumerators.MinBy(e => e.Current);

            min.MoveNext();
        }
    }

}

