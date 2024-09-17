namespace PuzzleCollection.AdventOfCode.Year2022.Day4_CampCleanup;

public class Puzzle2_CalculateCountOfOverlappingAssignments : IPuzzle
{
    public string GetSolution()
    {
        var countOfAssignmentsWhereAPartIsSubsetOfTheOther = Input
            .GetCampSectionCleanupAssignments()
            .Count(assignment => assignment.AssignmentPartsAreOverlapping);

        return $"{countOfAssignmentsWhereAPartIsSubsetOfTheOther} assignments have overlapping parts.";
    }
}
