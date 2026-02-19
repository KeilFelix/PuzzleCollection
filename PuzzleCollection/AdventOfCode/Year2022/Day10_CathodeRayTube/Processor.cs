// Copyright ©️ 2026 - Felix Keil (Awesomni.Codes)
// Licensed under the MIT License.

using System.Reactive.Subjects;

namespace PuzzleCollection.AdventOfCode.Year2022.Day10_CathodeRayTube
{
    public class Processor
    {
        public BehaviorSubject<int> RegisterX { get; private set; } = new BehaviorSubject<int>(1);
        public BehaviorSubject<int> CpuCycle { get; private set; } = new BehaviorSubject<int>(0);
        public IEnumerable<(int CpuCycle, int RegisterX)> Execute(ProcessorInstruction instruction)
        {
            for (int i = 0; i < instruction.CpuCycles; i++)
            {
                CpuCycle.OnNext(CpuCycle.Value + 1);

                if (i == instruction.CpuCycles - 1 && instruction is AddXInstruction addXInstruction)
                {
                    RegisterX.OnNext(RegisterX.Value + addXInstruction.ValueToAdd);
                }

                yield return (CpuCycle.Value, RegisterX.Value);
            }


        }
    }
}
