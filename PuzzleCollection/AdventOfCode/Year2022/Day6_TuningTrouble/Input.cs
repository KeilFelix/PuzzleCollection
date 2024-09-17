namespace PuzzleCollection.AdventOfCode.Year2022.Day6_TuningTrouble;

public static class Input
{
    public static IEnumerable<char> DataStream()
    {
        using (StreamReader sr = new StreamReader("AdventOfCode\\Year2022\\Day6_TuningTrouble\\DataStream.txt"))
        {
            char current = (char)sr.Read();
            while (current >= 0)
            {
                yield return current;
                current = (char)sr.Read();
            }
        }
    }
}
