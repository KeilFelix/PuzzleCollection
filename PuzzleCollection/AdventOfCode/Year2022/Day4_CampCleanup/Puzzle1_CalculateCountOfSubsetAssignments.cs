namespace PuzzleCollection.AdventOfCode.Year2022.Day4_CampCleanup;

public class Puzzle1_CalculateCountOfSubsetAssignments : IPuzzle
{
    public string GetSolution()
    {
        var countOfAssignmentsWhereAPartIsSubsetOfTheOther = Input
            .GetCampSectionCleanupAssignments()
            .Count(assignment => assignment.AnAssignmentIsSubsetOfOther);

        return $"{countOfAssignmentsWhereAPartIsSubsetOfTheOther} assignment parts are a subset of their counterpart.";
    }
}
