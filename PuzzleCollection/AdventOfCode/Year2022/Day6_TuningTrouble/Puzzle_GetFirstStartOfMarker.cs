namespace PuzzleCollection.AdventOfCode.Year2022.Day6_TuningTrouble;

public abstract class Puzzle_GetFirstStartOfMarker : IPuzzle
{
    private readonly int _markerSize;

    protected Puzzle_GetFirstStartOfMarker(int markerSize)
    {
        _markerSize = markerSize;
    }
    public string GetSolution()
    {
        var markers = Input.DataStream().ParseAsMarkers(_markerSize);

        var firstStarterPacketEndPosition = markers
            .Select((Marker, Index) => (Marker, Index))
            .Skip(_markerSize - 1) //Dirty :/
            .First(t => t.Marker.IsStarterPacket)
            .Index + 1;

        return $"The position of the last character of the first start-of-packet marker is  {firstStarterPacketEndPosition}.";
    }
}

public class Puzzle1_GetFirstStartOfPacketMarker : Puzzle_GetFirstStartOfMarker
{
    public Puzzle1_GetFirstStartOfPacketMarker() : base(4) { }
}

public class Puzzle2_GetFirstStartOfMessageMarker : Puzzle_GetFirstStartOfMarker
{
    public Puzzle2_GetFirstStartOfMessageMarker() : base(14) { }
}
