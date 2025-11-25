using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

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
            instructions.ForEach(instruction => {
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
