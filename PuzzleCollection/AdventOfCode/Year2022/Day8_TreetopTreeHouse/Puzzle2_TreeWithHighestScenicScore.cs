using PuzzleCollection.Util.Grids;

namespace PuzzleCollection.AdventOfCode.Year2022.Day8_TreetopTreeHouse;

public class Puzzle2_TreeWithHighestScenicScore : IPuzzle
{
    public string GetSolution()
    {
        var maxScenicScore = Input
            .GetGrid()
            .AllObjects
            .ToList()
            .Select(treeObj => treeObj.Position.GetScenicScore())
            .Max();

        return $"The highest scenic score is {maxScenicScore} from outside.";
    }

    //Temp
    private void TestInspectItem(Coord coord)
    {
        var grid = Input.GetGrid();
        var position = grid.GetPosition(coord);

        Console.WriteLine($"Positions Height:{position.Objects.Single().Value.Height}");
        Console.WriteLine($"Positions Scenic View Score:{position.GetScenicScore()}");

        foreach (var direction in Directions.Orthogonal)
        {
            Console.WriteLine();
            Console.WriteLine($"Direction {Enum.GetName(direction)}");
            Console.WriteLine();
            Console.WriteLine($"View distance:{position.GetViewingDistance(direction)}");

            Console.Write("Visible neighbors:");
            foreach (var visibleNeighbor in position.VisibleNeighbors(direction))
            {
                Console.Write($"{visibleNeighbor.Objects.Single().Value.Height} ");
            }

            Console.Write("\r\n");
        }
    }
}
