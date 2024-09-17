using PuzzleCollection.Util;

namespace PuzzleCollection.CodinGame.ShadowOfTheKnight.Ep1;

public record Building(int Width, int Height)
{
    public int GetDistanceToEnd(Position position, Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return position.Y;
            case Direction.Right:
                return Width - position.X - 1;
            case Direction.Down:
                return Height - position.Y - 1;
            case Direction.Left:
                return position.X;
            default:
                throw new ArgumentException(nameof(direction));
        }
    }
}

public record Position(int X, int Y)
{
    public Position Move(Direction direction, int distance)
    {
        switch (direction)
        {
            case Direction.Up:
                return new Position(X, Y - distance);
            case Direction.Right:
                return new Position(X + distance, Y);
            case Direction.Down:
                return new Position(X, Y + distance);
            case Direction.Left:
                return new Position(X - distance, Y);
            default:
                throw new ArgumentException(nameof(direction));
        }
    }
}

[Flags]
public enum Direction
{
    Up = 1,
    Right = 2,
    Down = 4,
    Left = 8
}

public static class DirectionEx
{
    public static bool IsSameAxis(this Direction source, Direction direction)
    {
        if (source == direction) return true;
        if(source == Direction.Left && direction == Direction.Right) return true;
        if(source == Direction.Down && direction == Direction.Up) return true;
        if (source == Direction.Right && direction == Direction.Left) return true;
        if (source == Direction.Up && direction == Direction.Down) return true;
        return false;
    }
    public static Direction From(string rawDirection)
    {
        switch (rawDirection)
        {
            case "U":
                return Direction.Up;
            case "UR":
                return Direction.Up | Direction.Right;
            case "R":
                return Direction.Right;
            case "DR":
                return Direction.Down | Direction.Right;
            case "D":
                return Direction.Down;
            case "DL":
                return Direction.Down | Direction.Left;
            case "L":
                return Direction.Left;
            case "UL":
                return Direction.Up | Direction.Left;
            default:
                throw new ArgumentException(nameof(rawDirection));
        }
    }
}

public class HeatSignatureDevice
{
    private readonly Func<Direction> _getNextDirection;

    public HeatSignatureDevice(Func<Direction> getNextDirection)
    {
        _getNextDirection = getNextDirection;
    }

    public IEnumerable<Direction> GetNextDirections() => _getNextDirection().GetSetFlags();
}

public class ShadowOfTheKnightBombSearcher
{
    private readonly Building _building;
    private readonly HeatSignatureDevice _heatSignatureDevice;
    private readonly int _maxNumberOfTurns;
    private readonly Action<Position> _notifyMyPosition;

    private Position Position { get; set; }

    public ShadowOfTheKnightBombSearcher(Building building, Position startPosition, HeatSignatureDevice heatSignatureDevice, int maxNumberOfTurns, Action<Position> notifyMyPosition)
    {
        _building = building;
        Position = startPosition;
        _heatSignatureDevice = heatSignatureDevice;
        _maxNumberOfTurns = maxNumberOfTurns;
        _notifyMyPosition = notifyMyPosition;
    }

    public void Search()
    {
        var nextDirectionsToBomb = _heatSignatureDevice.GetNextDirections();

        var directionsWithNextDistancesToBomb = nextDirectionsToBomb
            .Select(d => (Direction: d, Distance: (_building.GetDistanceToEnd(Position, d) + 1) / 2)).ToList();

        while (true)
        {
            foreach (var (curDirection, curDistance) in directionsWithNextDistancesToBomb)
            {
                Position = Position.Move(curDirection, curDistance);
            }
            _notifyMyPosition(Position);

            nextDirectionsToBomb = _heatSignatureDevice.GetNextDirections();
            directionsWithNextDistancesToBomb = nextDirectionsToBomb
                .Select(d => (Direction: d,
                    Distance: (directionsWithNextDistancesToBomb.Single(dwd => dwd.Direction.IsSameAxis(d))
                        .Distance +1)/2)).ToList();
        }
    }
}

class Program
{
    
    static void Main(string[] args)
    {
        //Setup I/O connection
        void SendNextRawPosition(string position) => Console.WriteLine(position);
        string GetNextRawBombDirection() => Console.ReadLine();
        var rawBuilding = Console.ReadLine().Split(' ');
        var maxNumberOfTurnsInput = int.Parse(Console.ReadLine());
        var rawStartPosition = Console.ReadLine().Split(' ');

        //Parse I/O to game
        void SendNextPosition(Position position) => SendNextRawPosition($"{position.X} {position.Y}");
        Direction GetNextBombDirection() => DirectionEx.From(GetNextRawBombDirection());
        var building = new Building(int.Parse(rawBuilding[0]), int.Parse(rawBuilding[1]));
        var startPosition = new Position(int.Parse(rawStartPosition[0]), int.Parse(rawStartPosition[1]));
        var heatSignatureDevice = new HeatSignatureDevice(GetNextBombDirection);


        var shadowOfTheKnightBombSearcher = new ShadowOfTheKnightBombSearcher(
            building,
            startPosition,
            heatSignatureDevice, 
            maxNumberOfTurnsInput, 
            SendNextPosition);


        shadowOfTheKnightBombSearcher.Search();
    }
}
