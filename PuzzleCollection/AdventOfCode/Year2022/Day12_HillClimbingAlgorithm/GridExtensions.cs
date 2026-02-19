// Copyright ©️ 2026 - Felix Keil (Awesomni.Codes)
// Licensed under the MIT License.

using PuzzleCollection.Util.Grids;

namespace PuzzleCollection.AdventOfCode.Year2022.Day12_HillClimbingAlgorithm;

public static class GridExtensions
{
    extension(Grid<Marker>.Position position)
    {
        public IEnumerable<Grid<Marker>.Position> GetNeighbors()
            => Directions.Orthogonal(2)
                .Select(direction => position.Move(new Move(direction, 1)))
                .Where(p => p.Objects.Any());

        public IEnumerable<Grid<Marker>.Position> GetWalkablePositions()
        {
            var currentGround = position.Objects.Find(o => o.Value is Ground).IfNone(() => throw new InvalidOperationException());
            var height = ((Ground)currentGround.Value).Height;
            return Directions.Orthogonal(2)
                .Select(direction => position.Move(new Move(direction, 1)))
                .Where(neighbor => neighbor.Objects.Any(o => o.Value is Ground ground && ground.Height - 1 <= height));
        }

        //public IEnumerable<Grid<Marker>.Position> GetNextPositionsAndSetPath()
        //{
        //    var pathObject = position.Objects.Single(o => o.Value is Path);
        //    var newPath = new Path(((Path)pathObject.Value).Positions.Append(position).ToList());
        //    var nextPositions = position.GetWalkablePositions().ToList();

        //    foreach (var nextPosition in nextPositions)
        //    {

        //        var nextPathObject = nextPosition.Objects.SingleOrDefault(o => o.Value is Path);

        //        if (nextPathObject?.Value.As<Path>()?.Positions.Count)
        //        {

        //        }

        //        var nextPath = (Path)nextPathObject.Value;
        //        if (nextPath.Positions.Count == 0 || nextPath.Positions.Count > newPath.Positions.Count)
        //        {
        //            nextPathObject.MoveTo(null);
        //            nextPathObject.MoveTo(nextPosition);
        //            nextPosition.Objects = nextPosition.Objects.Except(new[] { nextPathObject })
        //                .Append(new Grid<Marker>.Object(newPath, nextPosition))
        //                .ToList();
        //        }
        //    }
        //}
    }
}
