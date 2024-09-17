namespace PuzzleCollection.AdventOfCode.Year2022.Day4_CampCleanup;

public static class Input
{
    public static IEnumerable<CampSectionCleanupAssignment> GetCampSectionCleanupAssignments()

        => File.ReadAllLines("AdventOfCode\\Year2022\\Day4_CampCleanup\\CampSectionCleanupAssignments.txt")
            .Select(GetCampSectionCleanupAssignmentFromLine);

    private static CampSectionCleanupAssignment GetCampSectionCleanupAssignmentFromLine(string line)
    {
        var assignmentParts = line.Split(',');

        if (assignmentParts.Length != 2) throw new ArgumentException(nameof(line));

        return new CampSectionCleanupAssignment(GetCampSectionCleanupAssignmentPart(assignmentParts[0]), GetCampSectionCleanupAssignmentPart(assignmentParts[1]));
    }

    private static Camp.Section[] GetCampSectionCleanupAssignmentPart(string line)
    {
        var assignedSections = line
            .Split('-')
            .Select(int.Parse)
            .ToArray();

        if (assignedSections.Length != 2) throw new ArgumentException(nameof(line));

        return Enumerable.Range(assignedSections[0], assignedSections[1] - assignedSections[0] + 1) //I know Min&Max would be sufficient but this is more fun.
            .Select(id => new Camp.Section(id))
            .ToArray();
    }
}