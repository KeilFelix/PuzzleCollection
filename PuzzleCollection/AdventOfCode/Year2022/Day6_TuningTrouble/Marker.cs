namespace PuzzleCollection.AdventOfCode.Year2022.Day6_TuningTrouble;

public record Marker(IEnumerable<char> Characters)
{
    public bool IsStarterPacket => Characters.Distinct().Count() == Characters.Count();
}

public static class StreamExtensions
{
    public static IEnumerable<Marker> ParseAsMarkers(this IEnumerable<char> stream, int markerSize)
    {
        Marker NextMarker(Marker old, char newChar)
        {
            return new Marker(old.Characters.Skip(1).Append(newChar));
        }
        var currentMarker = new Marker(new char[markerSize]);

        foreach (var character in stream)
        {
            currentMarker = NextMarker(currentMarker, character);
            yield return currentMarker;
        }
    }
}
