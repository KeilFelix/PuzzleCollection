using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleCollection.AdventOfCode.Year2022.Day10_CathodeRayTube
{
    public class Input
    {
        public static IEnumerable<ProcessorInstruction> GetInstructions()
            => File.ReadAllLines("AdventOfCode/Year2022/Day10_CathodeRayTube/ProcessorInstructions.txt")
            .Select(ParseInstruction);

        private static ProcessorInstruction ParseInstruction(string source)
        {
            if (source == "noop")
            {
                return new NoOpInstruction();
            }
            else if (source.StartsWith("addx "))
            {
                var parts = source.Split(' ');
                if (parts.Length == 2 && int.TryParse(parts[1], out int value))
                {
                    return new AddXInstruction(value);
                }
            }
            throw new InvalidOperationException($"Invalid instruction: {source}");
        }
    }
}
