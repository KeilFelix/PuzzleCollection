using System.Text.RegularExpressions;
using PuzzleCollection.Util;

namespace PuzzleCollection.AdventOfCode.Year2022.Day5_SupplyStacks;

public static class Input
{
    public static IEnumerable<MoveInstruction> GetMoveInstructions()
        => File.ReadAllLines("AdventOfCode\\Year2022\\Day5_SupplyStacks\\MoveInstructions.txt")
            .Select(GetMoveInstructionFromLine);



    private static MoveInstruction GetMoveInstructionFromLine(string line)
    {
        var lineMatch = new Regex(@"move\s(?<Count>\d+)\sfrom\s(?<From>\d+)\sto\s(?<To>\d+)").Match(line);

        return new MoveInstruction(
            int.Parse(lineMatch.Groups["From"].Value),
            int.Parse(lineMatch.Groups["To"].Value),
            int.Parse(lineMatch.Groups["Count"].Value));
    }


    //...parsing this is just tedious and no programming fun
    //Dear programming god. Please forgive me that I parsed this by hand!
    private static string[] stackInput =
    {
        "GFVHPS",
        "GJFBVDZM",
        "GMLJN",
        "NGZVDWP",
        "VRCB",
        "VRSMPWLZ",
        "THP",
        "QRSNCHZV",
        "FLGPVQJ",
    };

    public static List<Stack<Crate>> GetStacks()
        => stackInput
            .Select(stackStr =>
                stackStr.ToCharArray()
                    .Select(identifier => new Crate(identifier))
                    .ToStack())
            .ToList();
}
