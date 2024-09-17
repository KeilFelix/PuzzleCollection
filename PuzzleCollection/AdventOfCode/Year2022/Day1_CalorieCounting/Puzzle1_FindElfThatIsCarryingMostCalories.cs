namespace PuzzleCollection.AdventOfCode.Year2022.Day1_CalorieCounting;

public class Puzzle1_FindElfThatIsCarryingMostCalories : IPuzzle
{
    public string GetSolution()
    {
        var elfWithMaxLoad =
            Input.GetElvesCaloryInventory()
            .OrderByDescending(elf => elf.LoadedCalories)
            .First();

        return $"The elf {elfWithMaxLoad.Index} has most calories loaded with {elfWithMaxLoad.LoadedCalories}.";
    }
}