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
            return Direction switch
            {
                Direction.Up => new Coord(0, Length),
                Direction.UpRight => new Coord(Length, Length),
                Direction.Right => new Coord(Length, 0),
                Direction.DownRight => new Coord(Length, -Length),
                Direction.Down => new Coord(0, -Length),
                Direction.DownLeft => new Coord(-Length, -Length),
                Direction.Left => new Coord(-Length, 0),
                Direction.UpLeft => new Coord(-Length, Length),
                _ => throw new ArgumentException($"Unknown direction: {Direction}", nameof(Direction))
            };
        }
    }
}

public record Coord(int X, int Y)
{
        public Coord Move(Move move) => this + move.Vector;

        /// <summary>
        /// Returns an infinite sequence of coordinates walking in the specified direction.
        /// Use with TakeWhile or similar to limit the sequence.
        /// </summary>
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
            foreach (var val in coordValues)
            {
                new Object(val).MoveTo(position);
            }
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
            // Avoid unnecessary remove/add if already at the target position
            if (_position == position)
                return;

            _position?.Objects.Remove(this);
            _position = position;
            _position?.Objects.Add(this);
        }

        public Object(TValue value)
        {
            Value = value;
        }
        public Position? Position
        {
            get => _position;
        }

    }
}
