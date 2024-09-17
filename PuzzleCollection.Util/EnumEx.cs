namespace PuzzleCollection.Util;

public static class EnumEx
{
    public static IEnumerable<T> GetSetFlags<T>(this T flags) where T : struct, Enum    // New constraint for C# 7.3
    {
        foreach (T value in Enum.GetValues<T>())
            if (flags.HasFlag(value))
                yield return (T)value;
    }
}
