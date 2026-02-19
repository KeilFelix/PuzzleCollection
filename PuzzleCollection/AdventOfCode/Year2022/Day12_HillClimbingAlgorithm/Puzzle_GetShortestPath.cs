// Copyright ©️ 2026 - Felix Keil (Awesomni.Codes)
// Licensed under the MIT License.

using Microsoft.CodeAnalysis;
using PuzzleCollection.Util;
using PuzzleCollection.Util.Grids;

namespace PuzzleCollection.AdventOfCode.Year2022.Day12_HillClimbingAlgorithm
{
    public class Puzzle1_GetShortestPathFromStart : PuzzleBase_GetShortestPathFrom
    {
        public override IEnumerable<Grid<Marker>.Position> GetStartingPositions(Grid<Marker> map)
        {
            return map.AllObjects.Single(o => o.Value is Start).Position.Yield();
        }
    }

    public class Puzzle2_GetShortestPathFromAnyBottom : PuzzleBase_GetShortestPathFrom
    {
        public override IEnumerable<Grid<Marker>.Position> GetStartingPositions(Grid<Marker> map)
        {
            return map.AllObjects.Where(o => o.Value is Ground ground && ground.Height == 1).Select(o => o.Position);
        }
    }

    public abstract class PuzzleBase_GetShortestPathFrom : IPuzzle
    {
        public abstract IEnumerable<Grid<Marker>.Position> GetStartingPositions(Grid<Marker> map);

        public string GetSolution()
        {
            var map = Input.GetMap();
            var startPositions = GetStartingPositions(map);

            foreach (var position in startPositions)
            {
                var startingPath = new Grid<Marker>.Object(new Path(new List<Grid<Marker>.Position>()));
                startingPath.MoveTo(position);
            }


            var currentGridPositions = startPositions;

            Option<Grid<Marker>.Object> foundEnd = None;
            while (foundEnd.IsNone)
            {
                var steps = currentGridPositions
                    .SelectMany(previous => previous.GetWalkablePositions()
                        .Select(next => (previous, next)))
                    .DistinctBy(t => t.next)
                    .Select(t => (t.previous, t.next, path: t.next.Objects.Find(o => o.Value is Path path)))
                    .Where(t => t.path.IsNone).ToList();

                foreach (var (previous, next, _) in steps)
                {
                    var oldPath = previous.Objects.Choose(o => o.Value is Path path ? Some(path) : None).Single();
                    var nextPath = new Grid<Marker>.Object(new Path(oldPath.Positions.Append(previous).ToList()));
                    nextPath.MoveTo(next);
                }
                foundEnd = currentGridPositions.Choose(p => p.Objects.Find(o => o.Value is End end)).HeadOrNone();
                currentGridPositions = steps.Select(t => t.next).ToList();

            }

            var end = foundEnd.IfNone(() => throw new InvalidOperationException());
            var endPath = end.Position.Objects.Choose(o => o.Value is Path path ? Some(path) : None).Single();

            return $"The shortest path to the end is {endPath.Positions.Count}.";
        }
    }
}
