namespace PuzzleCollection.AdventOfCode.Year2022.Day1_CalorieCounting;

public class Puzzle2_FindTopThreeElvesWithMostCalories : IPuzzle
{
    public string GetSolution()
    {
        var topThreeElvesCalorieTotal =
            Input.GetElvesCaloryInventory()
                .OrderByDescending(elf => elf.LoadedCalories)
                .Take(3)
                .Select(elf => elf.LoadedCalories)
                .Sum();

        return $"The top three elves have {topThreeElvesCalorieTotal} calories in total.";
    }
}