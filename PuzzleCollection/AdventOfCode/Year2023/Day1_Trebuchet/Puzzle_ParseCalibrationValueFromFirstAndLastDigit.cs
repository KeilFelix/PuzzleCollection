using System.Text.RegularExpressions;

namespace PuzzleCollection.AdventOfCode.Year2023.Day1_Trebuchet;

public abstract class PuzzleBase_ParseCalibrationValueFromFirstAndLastDigit : IPuzzle
{
    public string GetSolution()
    {
        var calibrationValues = Input.CalibrationLines.Select(ParseCalibrationValue);

        var sumOfCalibrationValues = calibrationValues.Sum();

        return $"The sum of the calibration values is {sumOfCalibrationValues}.";
    }

    private int ParseCalibrationValue(string calibrationLine)
    {
        var matchedDigits = GetMatches(calibrationLine);

        return ParseMatch(matchedDigits.First()) * 10 + ParseMatch(matchedDigits.Last());
    }

    private Dictionary<string, int> digitWords = new Dictionary<string, int>
    {
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 },
    };

    protected abstract IEnumerable<string> GetMatches(string input);

    private int ParseMatch(string match)
        => match.Length == 1
            ? int.Parse(match)
            : digitWords[match];
}

public class Puzzle1_ParseCalibrationValueFromFirstAndLastDigit : PuzzleBase_ParseCalibrationValueFromFirstAndLastDigit
{
    protected Regex SingleDigitRegex { get; } = new Regex(@"\d");

    protected override IEnumerable<string> GetMatches(string input)
    {
        return SingleDigitRegex.Matches(input).Select(match => match.Value);
    }
}

public class Puzzle2_ParseCalibrationValueFromFirstAndLastDigitWithWordNumbers : PuzzleBase_ParseCalibrationValueFromFirstAndLastDigit
{
    protected Regex SingleDigitRegex { get; } = new Regex(@"(?=(\d|one|two|three|four|five|six|seven|eight|nine))");

    protected override IEnumerable<string> GetMatches(string input)
    {
        return SingleDigitRegex.Matches(input).Select(match => match.Groups[1].Value);
    }
}
