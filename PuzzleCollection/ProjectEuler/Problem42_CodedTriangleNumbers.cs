using PuzzleCollection.Util;

namespace PuzzleCollection.ProjectEuler;

public class Problem42_CodedTriangleNumbers : IPuzzle
{
    public IEnumerable<string> Words =>
        File.ReadAllText("ProjectEuler/0042_words.txt").Split(',')
            .Select(word => word.Trim('"'));
    public string GetSolution()
    {
        return $"There are {Words.Where(IsTriangleWord).Count()} triangle words";
    }

    public static IEnumerable<long> TriangleNumbersMemoized => IntEx.TriangleNumbers().Memoize();
    public static bool IsTriangleWord(string word)
    {
        var wordValue = word.Sum(c => c.AlphabeticalPosition());

        return TriangleNumbersMemoized.TakeWhile(tn => tn <= wordValue).LastOrDefault() == wordValue;
    }
}

