namespace PuzzleCollection.AdventOfCode.Year2022.Day7_NoSpaceLeftOnDevice;

public class Puzzle1_GetTotalSizeOfAllDirThatAreBelow100000 : IPuzzle
{
    public string GetSolution()
    {
        FileSystem fileSystem = FileSystem.RecreateFromShellInput(Input.Commands);

        var totalSizeOfAllDirThatAreBelow100000 = fileSystem.Root.DirectoriesDeep
            .Where(dir => dir.Size <= 100000)
            .Select(dir => dir.Size)
            .Sum();

        return $"The total size of all directories with size at most 100000 is {totalSizeOfAllDirThatAreBelow100000}.";
    }
}
