// Copyright ©️ 2026 - Felix Keil (Awesomni.Codes)
// Licensed under the MIT License.

namespace PuzzleCollection.AdventOfCode.Year2023.Day5_IfYouGiveASeedAFertilizer;

public record Almanac(IReadOnlyCollection<Range> SeedRanges, IReadOnlyCollection<AlmanacMap> Maps)
{
    public (IReadOnlyCollection<Range> Ranges, string Category) Map(IReadOnlyCollection<Range> ranges, string category)
    {
        var categoryMap = Maps.Single(m => m.SourceCategory == category);
        var mappedRanges = ranges.SelectMany(n => categoryMap.MapRange(n)).ToList().AsReadOnly();
        return (mappedRanges, categoryMap.DestinationCategory);
    }
}

public record AlmanacMap(string SourceCategory, string DestinationCategory, IReadOnlyCollection<RangeMap> MapRanges)
{
    public IEnumerable<Range> MapRange(Range toMap)
    {
        var leftOver = new Queue<Range>();
        leftOver.Enqueue(toMap);

        foreach (var mapRange in MapRanges)
        {
            var itemsToProcess = leftOver.Count;
            for (var i = 0; i < itemsToProcess; i++)
            {
                var leftOverRange = leftOver.Dequeue();

                var (mapped, unmapped) = mapRange.Map(leftOverRange);
                if (mapped.HasValue)
                {
                    yield return mapped.Value;

                }

                foreach (var unmappedRange in unmapped)
                {
                    leftOver.Enqueue(unmappedRange);
                }
            }
        }

        while (leftOver.Count > 0)
        {
            yield return leftOver.Dequeue();
        }
    }
}

public record RangeMap(Range Source, Range Destination)
{
    public long Delta => Destination.Start - Source.Start;
    public (Range? Mapped, IEnumerable<Range> Unmapped) Map(Range toMap)
    {
        var (unmappableRanges, mappableRange, _) = Range.Intersect(toMap, Source);


        Range? mappedRange = mappableRange.HasValue
            ? new Range(mappableRange.Value.Start + Delta, mappableRange.Value.End + Delta)
            : null;
        return (mappedRange, unmappableRanges);
    }
}

public readonly record struct Range(long Start, long End)
{
    public long Length => End - Start + 1;


    public bool IsEmpty() => Length <= 0;

    public static (IEnumerable<Range> aRemainders, Range? intersection, IEnumerable<Range> bRemainders) Intersect(Range a, Range b)
    {
        var intersectionStart = Math.Max(a.Start, b.Start);
        var intersectionEnd = Math.Min(a.End, b.End);

        if (intersectionStart > intersectionEnd)
        {
            return ([a], null, [b]);
        }

        var intersection = new Range(intersectionStart, intersectionEnd);

        IEnumerable<Range> aRemainders()
        {
            // Left remainder: part of 'a' that's before the intersection
            if (a.Start < intersection.Start)
            {
                yield return new Range(a.Start, intersection.Start - 1);
            }
            // Right remainder: part of 'a' that's after the intersection
            if (a.End > intersection.End)
            {
                yield return new Range(intersection.End + 1, a.End);
            }
        }

        IEnumerable<Range> bRemainders()
        {
            // Left remainder: part of 'b' that's before the intersection
            if (b.Start < intersection.Start)
            {
                yield return new Range(b.Start, intersection.Start - 1);
            }
            // Right remainder: part of 'b' that's after the intersection
            if (b.End > intersection.End)
            {
                yield return new Range(intersection.End + 1, b.End);
            }
        }

        return (aRemainders(), intersection, bRemainders());
    }
};