// Copyright ©️ 2026 - Felix Keil (Awesomni.Codes)
// Licensed under the MIT License.

using System.Reactive.Linq;
using System.Text;

namespace PuzzleCollection.AdventOfCode.Year2022.Day10_CathodeRayTube
{
    public class Puzzle2_CrtDrawing : IPuzzle
    {
        public string GetSolution()
        {
            var instructions = Input.GetInstructions();

            var processor = new Processor();

            var monitor = new StringBuilder();

            var lastRegisterX = 1;
            instructions.ForEach(instruction =>
            {
                foreach (var process in processor.Execute(instruction))
                {
                    var pixelPosition = ((process.CpuCycle - 1) % 40) + 1;
                    if (pixelPosition >= lastRegisterX && pixelPosition <= lastRegisterX + 2)
                    {
                        monitor.Append("#");
                    }
                    else
                    {
                        monitor.Append(".");
                    }
                    if (pixelPosition == 40)
                    {
                        monitor.Append("\r\n");
                    }
                    lastRegisterX = process.RegisterX;
                }
            }
            );

            return monitor.ToString();
        }
    }
}
