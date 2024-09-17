namespace PuzzleCollection.AdventOfCode.Year2023.Day2_CubeConundrum;

public class Puzzle1_SumOfPossibleGameIds : IPuzzle
{
    public string GetSolution()
    {
        var games = Input.Games.Memoize();
        var bagCubeSet = new CubeSet(new List<Cubes> { new Cubes(CubeColor.Red, 12), new Cubes(CubeColor.Green, 13), new Cubes(CubeColor.Blue, 14) });

        var possibleGames = games
            .Where(game => game.RevealedCubeSets.All(cubeSet => cubeSet.IsPossibleToTakeFrom(bagCubeSet)));

        var sumOfPossibleGameIds = possibleGames.Select(game => game.Id)
            .Sum();

        return $"The sum of all possible game ids is {sumOfPossibleGameIds}.";
    }
}
