// Copyright ©️ 2026 - Felix Keil (Awesomni.Codes)
// Licensed under the MIT License.

namespace PuzzleCollection.AdventOfCode.Year2023.Day2_CubeConundrum;

public class Puzzle2_SumOfThePowersOfTheMinimalCubeSets : IPuzzle
{
    public string GetSolution()
    {
        var games = Input.Games.Memoize();
        var minimalBagCubeSets = games.Select(game => game.MinimalBagCubeSet);
        var minimalBagCubeSetPowers = minimalBagCubeSets.Select(cubeSet => cubeSet.Power);
        var minimalBagCubeSetPowersSum = minimalBagCubeSetPowers.Sum();

        return $"The sum of the powers of all minimal cube sets is {minimalBagCubeSetPowersSum}.";
    }
}
