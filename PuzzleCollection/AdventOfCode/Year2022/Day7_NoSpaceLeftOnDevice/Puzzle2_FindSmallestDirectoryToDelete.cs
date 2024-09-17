namespace PuzzleCollection.AdventOfCode.Year2022.Day7_NoSpaceLeftOnDevice;

public class Puzzle2_FindSmallestDirectoryToDelete : IPuzzle
{
    private record Update(int Size);
    public string GetSolution()
    {
        FileSystem fileSystem = FileSystem.RecreateFromShellInput(Input.Commands);
        var update = new FileSystem.FileInfo("Update", 30000000);

        var unusedSpace = FileSystem.DiskSpace - fileSystem.Root.Size;

        var neededSpace = update.Size - unusedSpace;
        var dirsWithSize = fileSystem.Root.DirectoriesDeep
            .Select(d => (d.Name, d.Size))
            .ToList();

        var dirsOrdered = dirsWithSize.OrderBy(dir => dir.Size).ToList();



        var dirToDelete = fileSystem.Root.DirectoriesDeep
            .OrderBy(dir => dir.Size)
            .First(dir => dir.Size >= neededSpace);

        return $"The directory to be deleted should be {dirToDelete.Name} with a size of {dirToDelete.Size}.";
    }
}
