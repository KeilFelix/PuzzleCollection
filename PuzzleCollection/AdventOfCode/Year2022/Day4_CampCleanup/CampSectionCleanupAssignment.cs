namespace PuzzleCollection.AdventOfCode.Year2022.Day4_CampCleanup;

public record CampSectionCleanupAssignment(Camp.Section[] First, Camp.Section[] Second)
{
    public bool AnAssignmentIsSubsetOfOther
    {
        get
        {
            var intersect = First.Intersect(Second).ToArray();

            return intersect.SequenceEqual(First) || intersect.SequenceEqual(Second);
        }
    }

    public bool AssignmentPartsAreOverlapping => First.Intersect(Second).Any();
}