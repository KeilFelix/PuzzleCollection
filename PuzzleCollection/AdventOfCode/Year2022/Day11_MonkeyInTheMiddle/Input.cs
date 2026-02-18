using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Numerics;

namespace PuzzleCollection.AdventOfCode.Year2022.Day11_MonkeyInTheMiddle;

public static class Input
{
    public static IEnumerable<Monkey> GetMonkeys(Func<BigInteger, BigInteger>? worryLevelAdaption)
    {
        var monkeyLookup = new Dictionary<int, Monkey>();
        return File.ReadAllText("AdventOfCode/Year2022/Day11_MonkeyInTheMiddle/Monkeys.txt")
                .Split(["\r\n\r\n"], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(section => GetMonkey(section, worryLevelAdaption, monkeyLookup)).ToList();
    }

    private static Monkey GetMonkey(string monkeySection, Func<BigInteger, BigInteger>? worryLevelAdaption, Dictionary<int, Monkey> monkeyLookup)
    {
        var monkeyLines = monkeySection.Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var monkeyId = int.Parse(monkeyLines[0].Split(["Monkey ", ":"], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[0]);
        var startingItems = monkeyLines[1]
            .Split(["Starting items: ", ", "], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(long.Parse)
            .Select(i => new Item { WorryLevel = i })
            .ToList();
        var operationString = monkeyLines[2].Split(" = ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1];
        var scriptOptions = ScriptOptions.Default
            .AddReferences(typeof(BigInteger).Assembly)    // add System.Runtime.Numerics
            .AddImports("System", "System.Numerics");
        var operation = CSharpScript.EvaluateAsync<Func<BigInteger, BigInteger>>($"old => {operationString}", scriptOptions).Result;
        var testDivisor = int.Parse(monkeyLines[3].Split("Test: divisible by ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[0]);
        var positiveTestMonkeyId = int.Parse(monkeyLines[4].Split("If true: throw to monkey ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[0]);
        var negativeTestMonkeyId = int.Parse(monkeyLines[5].Split("If false: throw to monkey ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[0]);

        var monkey = new Monkey(() => monkeyLookup[positiveTestMonkeyId], () => monkeyLookup[negativeTestMonkeyId])
        {
            Id = monkeyId,
            Items = startingItems,
            Operation = operation,
            WorryLevelAdaption = worryLevelAdaption,
            Test = (worryLevel) => worryLevel % testDivisor == 0
        };

        BigInteger divisor = 1;
        divisor = divisor * 3;

        monkeyLookup.Add(monkeyId, monkey);
        return monkey;
    }

}
