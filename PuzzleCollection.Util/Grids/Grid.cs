namespace PuzzleCollection.Util.Grids;

public static class DirectionExtensions
{
    public static Move ToMove(this Direction direction, int length = 1) => new Move(direction, length);
}
public record Move(Direction Direction, int Length)
{
    public Coord Vector
    {
        get
        {
            switch (Direction)
            {
                case Direction.Up:
                    return new Coord(0, 0 + Length);
                case Direction.UpRight:
                    return new Coord(0 + Length, 0 + Length);
                case Direction.Right:
                    return new Coord(0 + Length, 0);
                case Direction.DownRight:
                    return new Coord(0 + Length, 0 - Length);
                case Direction.Down:
                    return new Coord(0, 0 - Length);
                case Direction.DownLeft:
                    return new Coord(0 - Length, 0 - Length);
                case Direction.Left:
                    return new Coord(0 - Length, 0);
                case Direction.UpLeft:
                    return new Coord(0 - Length, 0 + Length);
                default:
                    throw new ArgumentException(nameof(Direction));
            }
        }
    }
}

public record Coord(int X, int Y)
{
    public Coord Move(Move move) => this + move.Vector;

    public IEnumerable<Coord> Walk(Move move)
    {
        Coord currentCoord = Move(move);
        while (true)
        {
            yield return currentCoord;
            currentCoord = currentCoord.Move(move);
        }
    }

    public static Coord operator +(Coord a, Coord b) => new Coord(a.X + b.X, a.Y + b.Y);
    public static Coord operator -(Coord a, Coord b) => new Coord(a.X - b.X, a.Y - b.Y);

}

public class Grid<TValue>
{
    private Dictionary<Coord, Position> _positions { get; }

    public Grid() : this(Enumerable.Empty<IEnumerable<IEnumerable<TValue>>>()) { }

    public Grid(IEnumerable<IEnumerable<IEnumerable<TValue>>> values)
    {
        _positions = new();

        var objectsToAdd =
            values
            .SelectMany((row, y) =>
                row.Select((values, x) => (Coord: new Coord(x, y), Values: values)));

        foreach ((var coord, var coordValues) in objectsToAdd)
        {
            var position = GetPosition(coord);
            position.Objects.AddRange(coordValues.Select(val => new Object(val, position)));
        }
    }

    public Position GetPosition(Coord coord)
    {
        if (!_positions.TryGetValue(coord, out var position))
        {
            position = new(this, coord);
            _positions.Add(coord, position);
        }
        return position;
    }

   

    public IEnumerable<Object> AllObjects => _positions.SelectMany(kvp => kvp.Value.Objects);

    public class Position
    {
        public Grid<TValue> Grid { get; }

        public Coord Coord { get; }

        public List<Object> Objects { get; } = new();

        public Position Move(Move move) => Grid.GetPosition(Coord + move.Vector);

        public Position Move(Coord coord) => Grid.GetPosition(Coord + coord);

        public IEnumerable<Position> Walk(Move move)
        {
            return Coord.Walk(move).Select(Grid.GetPosition);
        }

        public Position(Grid<TValue> grid, Coord coord)
        {
            Grid = grid;
            Coord = coord;
        }
    }

    public class Object
    {
        private Position? _position;
        public TValue Value { get; }

        public void Move(Move move)
        {
            if(Position == null) throw new InvalidOperationException("Object is not placed on a grid");

            var newPosition = Position.Move(move);

            MoveTo(newPosition);
        }

        public void Move(Coord coord)
        {
            if(Position == null) throw new InvalidOperationException("Object is not placed on a grid");

            var newPosition = Position.Move(coord);

            MoveTo(newPosition);
        }

        public void MoveTo(Position? position)
        {
            Position?.Objects.Remove(this);
            _position = position;
            Position?.Objects.Add(this);
        }

        public Object(TValue value, Position? position = null)
        {
            
            _position = position;
            Value = value;
        }
        public Position? Position
        {
            get => _position;
        }

    }
}
