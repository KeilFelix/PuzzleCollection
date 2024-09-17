namespace PuzzleCollection.CodeWars.RailFenceCipher_EncodingAndDecoding;

public static class RailFenceCipher
{
    public static string Encode(this string input, int rails) => string.Concat(Encode<char>(input, rails));

    public static string Decode(this string input, int rails) => string.Concat(Decode<char>(input, rails));

    public static IEnumerable<T> Encode<T>(this IEnumerable<T> input, int rails)
    {
        var itemWithRailNumbers = Util.EnumerableEx.RailFence(rails).Zip(input, (rail, item) => (rail, item));

        var itemsGroupedByRail = itemWithRailNumbers.GroupBy(t => t.rail);

        var itemsByRail = itemsGroupedByRail.Select(g => g.Select(t => t.item));

        return itemsByRail.SelectMany(t => t);
    }

    public static IEnumerable<T> Decode<T>(this IEnumerable<T> input, int rails)
    {
        var encryptedIndexes = Encode(Enumerable.Range(0, input.Count()), rails);

        var indexWithItems = encryptedIndexes.Zip(input, (index, item) => (index, item));

        var sortedItems = indexWithItems.OrderBy(t => t.index).Select(t => t.item);

        return sortedItems;
    }
}
