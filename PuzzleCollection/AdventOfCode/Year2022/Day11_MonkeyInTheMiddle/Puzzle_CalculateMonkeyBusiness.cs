// Copyright ©️ 2026 - Felix Keil (Awesomni.Codes)
// Licensed under the MIT License.

using System.Numerics;

namespace PuzzleCollection.AdventOfCode.Year2022.Day11_MonkeyInTheMiddle
{
    public abstract class Puzzle_CalculateMonkeyBusiness : IPuzzle
    {
        public abstract int NumberOfRounds { get; }
        public abstract Func<BigInteger, BigInteger>? WorryLevelAdjustment { get; }
        public string GetSolution()
        {
            var monkeys = Input.GetMonkeys(WorryLevelAdjustment);

            for (int round = 0; round < NumberOfRounds; round++)
            {
                foreach (var monkey in monkeys)
                {
                    monkey.Turn();
                }
            }

            var orderedMonkeys = monkeys.OrderByDescending(m => m.InspectionCount);

            var highestInspectionCounts = orderedMonkeys.Take(2).ToList();
            long monkeyBusiness = highestInspectionCounts[0].InspectionCount * highestInspectionCounts[1].InspectionCount;
            return $"The monkey business is {monkeyBusiness}.";
        }
    }

    public class Puzzle1_CalculateMonkeyBusiness : Puzzle_CalculateMonkeyBusiness
    {
        public override Func<BigInteger, BigInteger>? WorryLevelAdjustment => worryLevel => worryLevel / 3;

        public override int NumberOfRounds => 20;
    }

    public class Puzzle2_CalculateMonkeyBusiness : Puzzle_CalculateMonkeyBusiness
    {
        public const int LeastCommonMultipleOfDivisors = 2 * 3 * 5 * 7 * 11 * 13 * 17 * 19;
        public override Func<BigInteger, BigInteger>? WorryLevelAdjustment => worryLevel => worryLevel % LeastCommonMultipleOfDivisors;

        public override int NumberOfRounds => 10000;
    }
}
