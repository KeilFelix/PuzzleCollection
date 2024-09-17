namespace PuzzleCollection.AdventOfCode.Year2022.Day7_NoSpaceLeftOnDevice;

public interface IShellCommand { }
public record CdCommand(string Destination) : IShellCommand;
public record LsCommand(string[] Subdirectories, FileSystem.FileInfo[] Files) : IShellCommand;
