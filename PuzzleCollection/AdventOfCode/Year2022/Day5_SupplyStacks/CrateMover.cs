using PuzzleCollection.Util;

namespace PuzzleCollection.AdventOfCode.Year2022.Day5_SupplyStacks;

public abstract class CrateMover
{
    public void Rearrange(List<Stack<Crate>> Stacks, IEnumerable<MoveInstruction> moveInstructions)
    {
        foreach (var moveInstruction in moveInstructions)
        {
            Rearrange(Stacks, moveInstruction);
        }
    }
    public abstract void Rearrange(List<Stack<Crate>> Stacks, MoveInstruction moveInstruction);
}

public class CrateMover9000 : CrateMover
{
    public override void Rearrange(List<Stack<Crate>> Stacks, MoveInstruction moveInstruction)
    {
        for (int i = 0; i < moveInstruction.Count; i++)
        {
            Stacks[moveInstruction.To - 1].Push(Stacks[moveInstruction.From - 1].Pop());
        }
    }
}

public class CrateMover9001 : CrateMover
{
    public override void Rearrange(List<Stack<Crate>> Stacks, MoveInstruction moveInstruction)
    {
        var pickedUpCrates = Stacks[moveInstruction.From - 1].Pop(moveInstruction.Count)
            .Reverse();

        foreach (var crate in pickedUpCrates)
        {
            Stacks[moveInstruction.To - 1].Push(crate);
        }
    }


}
