using PuzzleCollection.Util;
using PuzzleCollection.Util.Grids;

namespace PuzzleCollection.AdventOfCode.Year2022.Day9_RopeBridge;

public record Marker() { }
public record Knot(int Number) : Marker { }
public record Trail(Marker Of) : Marker { }
public record Start() : Marker { }

public class Rope
{
    public Rope(Coord startCoord, int ropeLength)
    {
        var startPosition = Grid.GetPosition(startCoord);
        Start = new Grid<Marker>.Object(new Start());
        Start.MoveTo(startPosition);

        for (int i = 0; i < ropeLength; i++)
        {
            var knot = new Knot(i);
            new Grid<Marker>.Object(knot).MoveTo(startPosition);
            new Grid<Marker>.Object(new Trail(knot)).MoveTo(startPosition);
        }


        Knots = Grid.AllObjects
        .Where(obj => obj.Value is Knot)
        .OrderBy(obj => obj.Value is Knot knot ? knot.Number : 0)
        .ToList();

        Head = Knots.First();
        Tail = Knots.Last();
    }

    public Grid<Marker>.Object Start { get; }

    public Grid<Marker>.Object Head { get; }

    public Grid<Marker>.Object Tail { get; }

    public List<Grid<Marker>.Object> Knots { get; }
   

    public Grid<Marker> Grid { get; } = new();

    public void MoveRope(Direction direction)
    {
        MoveWithTrail(Head, direction.ToMove().Vector);

        foreach ((var leadingKnot, var followingKnot) in Knots.PairWithPrevious().Skip(1))
        {
            var followTailCoord = GetFollowVector(followingKnot.Position!.Coord, leadingKnot.Position!.Coord);

            MoveWithTrail(followingKnot, followTailCoord);
        }
    }

    private Coord GetFollowVector(Coord followCoord, Coord leadingCoord)
    {
        var distanceCoord = leadingCoord - followCoord;

        var followX = 0;
        var followY = 0;
        if(distanceCoord.X > 1)
        {
            followX = 1;
            if(distanceCoord.Y > 0)
            {
                followY = 1;
            }
            else if(distanceCoord.Y < 0)
            {
                followY = -1;
            }
        }
        else if(distanceCoord.X < -1)
        {
            followX = -1;
            if(distanceCoord.Y > 0)
            {
                followY = 1;
            }
            else if(distanceCoord.Y < 0)
            {
                followY = -1;
            }
        }
        else if(distanceCoord.Y > 1)
        {
            followY = 1;
            if(distanceCoord.X > 0)
            {
                followX = 1;
            }
            else if(distanceCoord.X < 0)
            {
                followX = -1;
            }
        }
        else if(distanceCoord.Y < -1)
        {
            followY = -1;
            if(distanceCoord.X > 0)
            {
                followX = 1;
            }
            else if(distanceCoord.X < 0)
            {
                followX = -1;
            }
        }
        return new(followX, followY);
    }

    private void MoveWithTrail(Grid<Marker>.Object toMove, Coord coord)
    {
        toMove.Move(coord);
        var trail = new Grid<Marker>.Object(new Trail(toMove.Value));
        trail.MoveTo(toMove.Position);
    }
}
