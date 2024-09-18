namespace PuzzleCollection.AdventOfCode.Year2023.Day2_CubeConundrum;

public class Input
{
    public static IEnumerable<Game> Games
        => File.ReadAllLines("AdventOfCode/Year2023/Day2_CubeConundrum/Games.txt")
        .Select(ParseGame);

    public static Game ParseGame(string gameLine)
    {
        var gameLineSplitted = gameLine.Split(new string[] { "Game ", ": ", "; " }, StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList();

        if (!int.TryParse(gameLineSplitted[0], out var gameId))
        {
            throw new ArgumentException(nameof(gameLine));
        }

        return new Game(gameId, gameLineSplitted.Skip(1).Select(ParseCubeSet).ToList());
    }

    public static CubeSet ParseCubeSet(string cubeSetPhrase)
    {
        var cubeGroups = cubeSetPhrase.Split(new string[] {", " }, StringSplitOptions.RemoveEmptyEntries)
            .Select(ParseCubeGroup)
            .ToList();

        return new CubeSet(cubeGroups);
    }

    private static Cubes ParseCubeGroup(string cubeGroupPhrase)
    {
        var cubeGroupPhaseSplitted = cubeGroupPhrase.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
            .Select(t => t.Trim()).ToList();

        if (!int.TryParse(cubeGroupPhaseSplitted[0], out var cubeCount))
        {
            throw new ArgumentException(nameof(cubeGroupPhrase));
        }

        if (!Enum.TryParse<CubeColor>(cubeGroupPhaseSplitted[1], true, out var cubeColor))
        {
            throw new ArgumentException(nameof(cubeGroupPhrase));
        }

        return new Cubes(cubeColor, cubeCount);
    }
}
