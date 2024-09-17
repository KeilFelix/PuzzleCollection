using PuzzleCollection.Util;
using System.Text.RegularExpressions;

namespace PuzzleCollection.AdventOfCode.Year2022.Day7_NoSpaceLeftOnDevice;


public static class Input
{
    public static IEnumerable<IShellCommand> Commands
        => File.ReadAllLines("AdventOfCode\\Year2022\\Day7_NoSpaceLeftOnDevice\\CommandShellInput.txt")
        .SplitBefore(str => str.StartsWith('$'))
        .Select(commandWithOutput => Parse(commandWithOutput.First(), commandWithOutput.Skip(1)));


    public static IShellCommand Parse(string command, IEnumerable<string> output)
    {
        if (command == "$ ls")
        {
            var lsSplittedOutput = output
                .Select(str => str.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries));

            var subdirectories = lsSplittedOutput
                .Where(split => split[0] == "dir")
                .Select(split => split[1])
                .ToArray();

            var fileInfos = lsSplittedOutput
                .SelectMany(split =>
                    int.TryParse(split[0], out int size)
                        ? new[] { new FileSystem.FileInfo(split[1], size) }
                        : Enumerable.Empty<FileSystem.FileInfo>())
                .ToArray();

            return new LsCommand(subdirectories, fileInfos);
        }

        var cdCommandMatch = new Regex(@"\$\scd\s(?<Destination>\S+)").Match(command);

        if (cdCommandMatch.Success)
        {
            return new CdCommand(cdCommandMatch.Groups["Destination"].Value);
        }

        throw new ArgumentException(nameof(command));
    }
}
