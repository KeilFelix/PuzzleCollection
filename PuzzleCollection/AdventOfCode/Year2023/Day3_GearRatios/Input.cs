namespace PuzzleCollection.AdventOfCode.Year2023.Day3_GearRatios;

public class Input
{
    public static EngineSchematic EngineSchematic
        => ParseEngineSchematic(File.ReadAllText("AdventOfCode/Year2023/Day2_CubeConundrum/Games.txt"));

    public static EngineSchematic ParseEngineSchematic(string engineSchematicRaw)
    {
        return new EngineSchematic(new List<PartNumber>(), new List<Symbol>());
    }

}
