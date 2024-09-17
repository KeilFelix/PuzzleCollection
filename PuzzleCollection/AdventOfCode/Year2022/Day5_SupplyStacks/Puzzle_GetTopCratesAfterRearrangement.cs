namespace PuzzleCollection.AdventOfCode.Year2022.Day5_SupplyStacks;

public abstract class Puzzle_ProcessDataStream<TCrateMover> : IPuzzle where TCrateMover : CrateMover, new()
{
    public string GetSolution()
    {
        var stacks = Input.GetStacks();
        var moveInstructions = Input.GetMoveInstructions();
        var crane = new TCrateMover();

        crane.Rearrange(stacks, moveInstructions);

        var topCrates = stacks
            .Select(stack => stack.Peek().Identifier).ToArray();

        return $"The top crates are {new string(topCrates)}.";
    }
}

public class Puzzle1_GetTopCratesAfterRearrangementWithCrateMover9000 : Puzzle_ProcessDataStream<CrateMover9000> { }
public class Puzzle2_GetTopCratesAfterRearrangementWithCrateMover9001 : Puzzle_ProcessDataStream<CrateMover9001> { }
