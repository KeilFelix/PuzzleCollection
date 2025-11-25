using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleCollection.AdventOfCode.Year2022.Day10_CathodeRayTube
{
    public class Puzzle1_SumOfFirstSixSignalStrengths : IPuzzle
    {
        public string GetSolution()
        {
            var instructions = Input.GetInstructions();

            var processor = new Processor();

            int? sumOfFirstSixSignalStrengths = null;
            var processingCompleted = new Subject<Unit>();
            var processorSubscription = processor.CpuCycle.CombineLatest(processor.RegisterX, (cycle, registerX) => (cycle, registerX))
                .Where(t => (t.cycle - 20) % 40 == 0)
                .TakeUntil(processingCompleted)
                .ToList()
                .Subscribe(registerValuesByCycle =>
                {
                    var signalStrengths  = registerValuesByCycle.GroupBy(t => t.cycle).Select(grp => grp.First()).Select(t => t.registerX * t.cycle);
                    sumOfFirstSixSignalStrengths = signalStrengths.Sum();
                });

            instructions.ForEach(instruction => processor.Execute(instruction));
            processingCompleted.OnNext(new Unit());

            processorSubscription.Dispose();

            return $"Sum of the six signal strengths: {sumOfFirstSixSignalStrengths?.ToString() ?? "No solution"}";
        }
    }
}
