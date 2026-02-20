// Copyright ©️ 2026 - Felix Keil (Awesomni.Codes)
// Licensed under the MIT License.

using PuzzleCollection.Util;
using System.Text.RegularExpressions;

namespace PuzzleCollection.AdventOfCode.Year2023.Day5_IfYouGiveASeedAFertilizer;

public static class Input
{
    public static Almanac GetAlmanac(bool seedRanges)
        => ParseAlmanac(File.ReadAllText("AdventOfCode/Year2023/Day5_IfYouGiveASeedAFertilizer/AlmanacInput.txt"), seedRanges);


    public static Almanac ParseAlmanac(string almanacText, bool seedRanges)
    {
        var almanacSections = almanacText.Split([$"{Environment.NewLine}{Environment.NewLine}"], StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList();

        var rawSeedList = almanacSections.First().Split(System.Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).Skip(1).Select(long.Parse).ToList();
        List<Range> seedNumbers;
        if (seedRanges)
        {
            var seedRangesList = rawSeedList
                .PairWithPrevious()
                .Select((cur, index) => (cur, index))
                .Where(t => t.index % 2 == 1)
                .Select(t => (Start: t.cur.Previous, Length: t.cur.Current));

            seedNumbers = seedRangesList.Select(t => new Range(t.Start, t.Start + t.Length - 1)).ToList();
        }
        else
        {
            seedNumbers = rawSeedList.Select(seedNumber => new Range(seedNumber, seedNumber)).ToList();
        }

        var maps = almanacSections.Skip(1).Select(ParseMap).ToList();

        AlmanacMap ParseMap(string mapText)
        {
            Regex mapHeaderRegex = new(@"(?<SourceCategory>[a-z]+)-to-(?<DestinationCategory>[a-z]+)\smap:", RegexOptions.Compiled);
            var mapLines = mapText.Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList();
            var mapHeader = mapLines.First();
            var mapHeaderMatch = mapHeaderRegex.Match(mapHeader);
            var sourceCategory = mapHeaderMatch.Groups.GetValueOrDefault("SourceCategory")?.Value ?? throw new InvalidOperationException();
            var destinationCategory = mapHeaderMatch.Groups.GetValueOrDefault("DestinationCategory")?.Value ?? throw new InvalidOperationException();

            var mapRanges = mapLines.Skip(1).Select(line =>
            {
                var mapRangeParts = line.Split(System.Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList();
                var destinationRangeStart = long.Parse(mapRangeParts[0]);
                var sourceRangeStart = long.Parse(mapRangeParts[1]);
                var range = int.Parse(mapRangeParts[2]);
                return new RangeMap(new Range(sourceRangeStart, sourceRangeStart + range - 1), new Range(destinationRangeStart, destinationRangeStart + range - 1));
            }).ToList();

            return new AlmanacMap(sourceCategory, destinationCategory, mapRanges.AsReadOnly());
        }

        return new Almanac(seedNumbers.AsReadOnly(), maps.AsReadOnly());
    }
}
