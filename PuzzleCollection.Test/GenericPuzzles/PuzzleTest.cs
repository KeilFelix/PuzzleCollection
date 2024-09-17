namespace PuzzleCollection.Test.GenericPuzzles;

[TestFixtureSource(typeof(Puzzles), nameof(Puzzles.All))]
public class PuzzleTest
{
    private IPuzzle _puzzle;

    public PuzzleTest(IPuzzle puzzle)
    {
        _puzzle = puzzle;
    }
    public static IDictionary<Type, string> Answers { get; } = new Dictionary<Type, string>()
    {
        {typeof(Problem_Template.Puzzle1_DoSomething),  "This is a Unit SomeThing { }" },
        {typeof(AdventOfCode.Year2022.Day1_CalorieCounting.Puzzle1_FindElfThatIsCarryingMostCalories),  "The elf 79 has most calories loaded with 69836." },
        {typeof(AdventOfCode.Year2022.Day1_CalorieCounting.Puzzle2_FindTopThreeElvesWithMostCalories),  "The top three elves have 207968 calories in total." },
        {typeof(AdventOfCode.Year2022.Day2_RockPaperScissors.Puzzle1_CalculateScoreFromStrategyGuideSecondSignAsChoice),  "The RockPaperScissors strategy guide's total score is 13052." },
        {typeof(AdventOfCode.Year2022.Day2_RockPaperScissors.Puzzle2_CalculateScoreFromStrategyGuideSecondSignAsResult),  "The RockPaperScissors strategy guide's total score is 13693." },
        {typeof(AdventOfCode.Year2022.Day3_RucksackReorganization.Puzzle1_CalculateScoreOfItemPrioritiesThatAreContainedInBothCompartments),  "The sum of all item priorities that are contained in both compartments is 8298." },
        {typeof(AdventOfCode.Year2022.Day3_RucksackReorganization.Puzzle2_CalculateScoreOfItemPrioritiesElvesGroupBadges),  "The sum of all item badge priorities from the elves groups is 2708." },
        {typeof(AdventOfCode.Year2022.Day4_CampCleanup.Puzzle1_CalculateCountOfSubsetAssignments),  "542 assignment parts are a subset of their counterpart." },
        {typeof(AdventOfCode.Year2022.Day4_CampCleanup.Puzzle2_CalculateCountOfOverlappingAssignments),  "900 assignments have overlapping parts." },
        {typeof(AdventOfCode.Year2022.Day5_SupplyStacks.Puzzle1_GetTopCratesAfterRearrangementWithCrateMover9000),  "The top crates are FCVRLMVQP." },
        {typeof(AdventOfCode.Year2022.Day5_SupplyStacks.Puzzle2_GetTopCratesAfterRearrangementWithCrateMover9001),  "The top crates are RWLWGJGFD." },
        {typeof(AdventOfCode.Year2022.Day6_TuningTrouble.Puzzle1_GetFirstStartOfPacketMarker),  "The position of the last character of the first start-of-packet marker is  1802." },
        {typeof(AdventOfCode.Year2022.Day6_TuningTrouble.Puzzle2_GetFirstStartOfMessageMarker),  "The position of the last character of the first start-of-packet marker is  3551." },
        {typeof(AdventOfCode.Year2022.Day7_NoSpaceLeftOnDevice.Puzzle1_GetTotalSizeOfAllDirThatAreBelow100000),  "The total size of all directories with size at most 100000 is 1644735." },
        {typeof(AdventOfCode.Year2022.Day7_NoSpaceLeftOnDevice.Puzzle2_FindSmallestDirectoryToDelete),  "The directory to be deleted should be bcnvw with a size of 1300850." },
        {typeof(AdventOfCode.Year2022.Day8_TreetopTreeHouse.Puzzle1_GetCountOfVisibleTrees),  "In the tree grid you can see 1832 from outside." },
        {typeof(AdventOfCode.Year2022.Day8_TreetopTreeHouse.Puzzle2_TreeWithHighestScenicScore),  "The highest scenic score is 157320 from outside." },
        {typeof(AdventOfCode.Year2022.Day9_RopeBridge.Puzzle1_GetCountOfTailVisitedPositions),  "The number of places that the tail visited is 6271." },
        {typeof(AdventOfCode.Year2022.Day9_RopeBridge.Puzzle2_GetCountOfTailVisitedPositions),  "The number of places that the tail visited is 2458." },
        {typeof(AdventOfCode.Year2023.Day1_Trebuchet.Puzzle1_ParseCalibrationValueFromFirstAndLastDigit),  "The sum of the calibration values is 55447." },
        {typeof(AdventOfCode.Year2023.Day1_Trebuchet.Puzzle2_ParseCalibrationValueFromFirstAndLastDigitWithWordNumbers),  "The sum of the calibration values is 54706." },
        {typeof(AdventOfCode.Year2023.Day2_CubeConundrum.Puzzle1_SumOfPossibleGameIds),  "The sum of all possible game ids is 2776." },
        {typeof(AdventOfCode.Year2023.Day2_CubeConundrum.Puzzle2_SumOfThePowersOfTheMinimalCubeSets),  "The sum of the powers of all minimal cube sets is 68638." },
        {typeof(AdventOfCode.Year2023.Day4_Scratchcards.Puzzle1_CalculateTotalScratchcardPoints),  "The total number of points is 28750." },
        {typeof(AdventOfCode.Year2023.Day4_Scratchcards.Puzzle2_CalculateTotalScratchcardsByCopyWinningRule),  "The total number of cards with all copies is 10212704." },
        {typeof(ProjectEuler.Problem26_ReciprocalCycles),  "Die längste wiederkehrende Dezimalzahl hat 982 Stellen und tritt bei 1/983 auf." },
        {typeof(ProjectEuler.Problem31_CoinSums),  "Es gibt 73682 Möglichkeiten, 200 Pence mit den Münzen 1, 2, 5, 10, 20, 50, 100, 200 zu bilden." },
        {typeof(ProjectEuler.Problem32_PandigitalProducts),  "Die Summe ist 45228." },
        {typeof(ProjectEuler.Problem33_DigitCancellingFractions),  "The denominator is 100." },
        {typeof(ProjectEuler.Problem34_DigitFactorials),  "The sum of all digit factorials is 40730." },
        {typeof(ProjectEuler.Problem35_CircularPrimes),  "The are 55 below one million" },
        {typeof(ProjectEuler.Problem36_DoubleBasePalindromes),  "The sum of all double base palindromes is 872187" },
        {typeof(ProjectEuler.Problem37_TruncatablePrimes),  "The sum of the eleven truncatable primes is 748317" },
        {typeof(ProjectEuler.Problem38_PandigitalMultiples),  "The largest pandigital multiple with 9 digits is 932718654" },
        {typeof(ProjectEuler.Problem39_IntegerRightTriangles),  "The perimeter of the right triangle with the most solutions is 840" },
        {typeof(ProjectEuler.Problem40_ChampernownesConstant),  "The product of the Champernowne's Constant digits is 210" },
        {typeof(ProjectEuler.Problem41_PandigitalPrime),  "The largest pandigital prime is 7652413" },
    };

    [Test]
    public void CheckPuzzle()
    {
        var solution = _puzzle.GetSolution();
        Console.WriteLine($"{_puzzle.GetType().FullName.Replace("PuzzleCollection.", string.Empty)}\nSolution: {solution}\n");

        Assert.That(solution, Is.EqualTo(Answers[_puzzle.GetType()]));
    }
}