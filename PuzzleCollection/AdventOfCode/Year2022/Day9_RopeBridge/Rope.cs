// Copyright ©️ 2026 - Felix Keil (Awesomni.Codes)
// Licensed under the MIT License.

using PuzzleCollection.Util;
using PuzzleCollection.Util.Grids;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace PuzzleCollection.AdventOfCode.Year2022.Day9_RopeBridge;

public record Marker() { }
public record Knot(int Number) : Marker { }
public record Trail(Marker Of) : Marker { }
public record Start() : Marker { }

public class Rope : IDisposable
{
    private readonly CompositeDisposable _knotSubscriptions = new();

    public Rope(Coord startCoord, int ropeLength)
    {
        var startPosition = Grid.GetPosition(startCoord);
        Start = new Grid<Marker>.Object(new Start());
        Start.MoveTo(startPosition);

        Knots = Enumerable.Range(0, ropeLength).Select(i => new Grid<Marker>.Object(new Knot(i))).ToList();

        Head = Knots.First();
        Tail = Knots.Last();

        Knots.ForEach(knot => knot.MoveTo(startPosition));

        var followingSubscriptions = Knots.PairWithPrevious().Select(knotPair =>
        {
            var (leadingKnot, followingKnot) = knotPair;
            if (leadingKnot is null)
                return Disposable.Empty;

            return leadingKnot.PositionObservable
                .Subscribe(position =>
                {
                    var followTailCoord = GetFollowVector(followingKnot.Position!.Coord, position!.Coord);
                    followingKnot.Move(followTailCoord);
                });
        }).ToList();

        var trailSubscriptions = Tail.PositionObservable
            .Subscribe(position =>
            {
                new Grid<Marker>.Object(new Trail(Tail.Value)).MoveTo(position);
            });

        _knotSubscriptions = new CompositeDisposable(followingSubscriptions) { trailSubscriptions };

        var allSubscriptions = Disposable.Create(() =>
        {
            foreach (var subscription in followingSubscriptions)
            {
                subscription.Dispose();
            }
            trailSubscriptions.Dispose();
        });
    }

    public Grid<Marker>.Object Start { get; }

    public Grid<Marker>.Object Head { get; }

    public Grid<Marker>.Object Tail { get; }

    public List<Grid<Marker>.Object> Knots { get; }


    public Grid<Marker> Grid { get; } = new();

    public void MoveRope(Direction direction) => Head.Move(direction.ToMove());

    private Coord GetFollowVector(Coord followCoord, Coord leadingCoord)
    {
        var distanceCoord = leadingCoord - followCoord;

        var followX = 0;
        var followY = 0;
        if (distanceCoord.X > 1)
        {
            followX = 1;
            if (distanceCoord.Y > 0)
            {
                followY = 1;
            }
            else if (distanceCoord.Y < 0)
            {
                followY = -1;
            }
        }
        else if (distanceCoord.X < -1)
        {
            followX = -1;
            if (distanceCoord.Y > 0)
            {
                followY = 1;
            }
            else if (distanceCoord.Y < 0)
            {
                followY = -1;
            }
        }
        else if (distanceCoord.Y > 1)
        {
            followY = 1;
            if (distanceCoord.X > 0)
            {
                followX = 1;
            }
            else if (distanceCoord.X < 0)
            {
                followX = -1;
            }
        }
        else if (distanceCoord.Y < -1)
        {
            followY = -1;
            if (distanceCoord.X > 0)
            {
                followX = 1;
            }
            else if (distanceCoord.X < 0)
            {
                followX = -1;
            }
        }
        return new(followX, followY);
    }

    public void Dispose() => _knotSubscriptions.Dispose();
}
