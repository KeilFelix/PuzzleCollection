using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleCollection.AdventOfCode.Year2022.Day10_CathodeRayTube
{
    public class Processor
    {
        public BehaviorSubject<int> RegisterX { get; private set; } = new BehaviorSubject<int>(1);
        public BehaviorSubject<int> CpuCycle { get; private set; } = new BehaviorSubject<int>(0);
        public void Execute(ProcessorInstruction instruction)
        {
            for (int i = 0; i < instruction.CpuCycles; i++)
            {
                CpuCycle.OnNext(CpuCycle.Value + 1);
            }

            if (instruction is AddXInstruction addXInstruction)
            {
                RegisterX.OnNext(RegisterX.Value + addXInstruction.ValueToAdd);
            }
        }
    }
}
