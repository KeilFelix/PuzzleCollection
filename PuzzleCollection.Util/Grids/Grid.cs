using System.Reactive.Linq;
using System.Reactive.Subjects;
using DynamicData;
using PuzzleCollection.Util;

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
            int highestBit = EnumEx.HighestSetBit(Direction);
            int axisCount = highestBit < 0 ? 2 : (highestBit / 2) + 1;
            axisCount = Math.Max(2, Math.Min(axisCount, Directions.Dimensions.Length));

            long[] values = new long[axisCount];
            ulong bits = Convert.ToUInt64(Direction);
            long length = Length;

            for (int axis = 0; axis < axisCount; axis++)
            {
                int negativeBit = axis * 2;
                int positiveBit = negativeBit + 1;

                if (((bits >> negativeBit) & 1UL) == 1UL)
                    values[axis] -= length;

                if (((bits >> positiveBit) & 1UL) == 1UL)
                    values[axis] += length;
            }

            return new Coord(values);
        }
    }
}

public readonly struct Coord : IEquatable<Coord>
{
    private readonly long[] _values;

    public Coord(params long[] values)
    {
        _values = values.ToArray();
    }

    public long this[int index] => _values != null && index < _values.Length ? _values[index] : 0;

    public int Dimension => _values?.Length ?? 0;

    public long X => this[0];
    public long Y => this[1];
    public long Z => this[2];

    public void Deconstruct(out long x, out long y)
    {
        x = X;
        y = Y;
    }

    public void Deconstruct(out long x, out long y, out long z)
    {
        x = X;
        y = Y;
        z = Z;
    }

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

    public static Coord operator +(Coord a, Coord b)
    {
        int dim = Math.Max(a.Dimension, b.Dimension);
        long[] newValues = new long[dim];
        for (int i = 0; i < dim; i++)
        {
            newValues[i] = a[i] + b[i];
        }
        return new Coord(newValues);
    }

    public static Coord operator -(Coord a, Coord b)
    {
        int dim = Math.Max(a.Dimension, b.Dimension);
        long[] newValues = new long[dim];
        for (int i = 0; i < dim; i++)
        {
            newValues[i] = a[i] - b[i];
        }
        return new Coord(newValues);
    }

    public override bool Equals(object? obj) => obj is Coord other && Equals(other);
    public bool Equals(Coord other)
    {
        if (_values == other._values) return true;

        var len = Math.Max(Dimension, other.Dimension);

        for (int i = 0; i < len; i++)
        {
            if (this[i] != other[i]) return false;
        }
        return true;
    }

    public override int GetHashCode()
    {
        if (_values == null) return 0;
        HashCode hash = new();

        int maxIndex = Dimension - 1;
        while (maxIndex >= 0 && this[maxIndex] == 0) maxIndex--;

        for (int i = 0; i <= maxIndex; i++)
        {
            hash.Add(this[i]);
        }

        return hash.ToHashCode();
    }

    public override string ToString() => $"({string.Join(", ", _values ?? Array.Empty<long>())})";

    public static bool operator ==(Coord left, Coord right) => left.Equals(right);
    public static bool operator !=(Coord left, Coord right) => !left.Equals(right);
}

public class Grid<TValue> : IDisposable
{
    private readonly SourceCache<Position, Coord> _positions = new(p => p.Coord);

    public IObservable<IChangeSet<Position, Coord>> ConnectPositions() => _positions.Connect();

    public Grid() : this(Enumerable.Empty<IEnumerable<IEnumerable<TValue>>>()) { }

    public Grid(IEnumerable<IEnumerable<IEnumerable<TValue>>> values)
    {
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
        var lookup = _positions.Lookup(coord);
        if (lookup.HasValue)
            return lookup.Value;

        var position = new Position(this, coord);
        _positions.AddOrUpdate(position);
        return position;
    }

    public IEnumerable<Object> AllObjects => _positions.Items.SelectMany(p => p.Objects);

    public void Dispose() => _positions.Dispose();

    public class Position
    {
        public Grid<TValue> Grid { get; }

        public Coord Coord { get; }

        private readonly SourceList<Object> _objects = new();
        public IEnumerable<Object> Objects => _objects.Items;
        public IObservable<IChangeSet<Object>> ObjectsChanges => _objects.Connect();

        internal void AddObject(Object obj) => _objects.Add(obj);
        internal void RemoveObject(Object obj) => _objects.Remove(obj);

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
        private readonly BehaviorSubject<Position?> _positionSubject = new(null);
        public TValue Value { get; }

        public IObservable<Position?> PositionObservable => _positionSubject.AsObservable();

        public void Move(Move move)
        {
            if (Position == null) throw new InvalidOperationException("Object is not placed on a grid");

            var newPosition = Position.Move(move);

            MoveTo(newPosition);
        }

        public void Move(Coord coord)
        {
            if (Position == null) throw new InvalidOperationException("Object is not placed on a grid");

            var newPosition = Position.Move(coord);

            MoveTo(newPosition);
        }

        public void MoveTo(Position? position)
        {
            if (_positionSubject.Value == position)
                return;

            var previousPosition = _positionSubject.Value;
            previousPosition?.RemoveObject(this);

            if (position != null)
            {
                position.AddObject(this);
            }

            _positionSubject.OnNext(position);
        }

        public Position? Position => _positionSubject.Value;

        public Object(TValue value)
        {
            Value = value;
        }
    }
}
