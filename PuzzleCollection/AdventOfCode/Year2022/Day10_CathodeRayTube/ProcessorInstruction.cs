// Copyright ©️ 2026 - Felix Keil (Awesomni.Codes)
// Licensed under the MIT License.

namespace PuzzleCollection.AdventOfCode.Year2022.Day10_CathodeRayTube
{
    public abstract class ProcessorInstruction
    {
        public abstract int CpuCycles { get; }
    }

    public class NoOpInstruction : ProcessorInstruction
    {
        public override int CpuCycles => 1;
    }

    public class AddXInstruction : ProcessorInstruction
    {
        public override int CpuCycles => 2;
        public int ValueToAdd { get; }
        public AddXInstruction(int valueToAdd)
        {
            ValueToAdd = valueToAdd;
        }
    }

}
