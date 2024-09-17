namespace PuzzleCollection.AdventOfCode.Year2022.Day7_NoSpaceLeftOnDevice;

public class FileSystem
{
    public record FileInfo(string Name, int Size);

    public class Directory
    {
        private Directory(string name, Directory parent)
        {
            Name = name;
            Parent = parent ?? this;
        }

        public Directory Parent { get; }
        public string Name { get; }

        public IEnumerable<Directory> Directories { get; } = new List<Directory>();
        public IEnumerable<FileInfo> Files { get; } = new List<FileInfo>();

        public IEnumerable<Directory> DirectoriesDeep
            => Directories.Concat(Directories.SelectMany(dir => dir.DirectoriesDeep));

        public int Size => Files.Select(file => file.Size).Sum() + Directories.Select(dir => dir.Size).Sum();

        public static Directory CreateRoot() => new Directory("", null);

        public Directory GetOrCreateSubdirectory(string name)
        {
            var dir = Directories.FirstOrDefault(dir => dir.Name == name);

            if (dir == null)
            {
                dir = new Directory(name, this);
                ((List<Directory>)Directories).Add(dir);
            }
            return dir;
        }

        public void AddFile(FileInfo file)
        {
            ((List<FileInfo>)Files).Add(file);
        }
    }

    public Directory Root { get; } = Directory.CreateRoot();

    public const int DiskSpace = 70000000;

    public static FileSystem RecreateFromShellInput(IEnumerable<IShellCommand> input)
    {
        FileSystem fileSystem = new FileSystem();

        Directory currentDir = null;

        foreach (var command in input)
        {
            if (command is LsCommand lsCommand)
            {
                foreach (var subDirName in lsCommand.Subdirectories)
                {
                    currentDir.GetOrCreateSubdirectory(subDirName);
                }

                foreach (var file in lsCommand.Files)
                {
                    currentDir.AddFile(file);
                }
            }

            if (command is CdCommand cdCommand)
            {
                currentDir = cdCommand.Destination == ".."
                    ? currentDir.Parent
                    : cdCommand.Destination == "/"
                        ? fileSystem.Root
                        : currentDir.Directories.Single(dir => dir.Name == cdCommand.Destination);
            }

        }
        return fileSystem;
    }
}
