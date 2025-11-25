using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
