using System.Numerics;

namespace PuzzleCollection.Util;

public static class EnumEx
{
    public static IEnumerable<T> GetSetFlags<T>(this T flags) where T : struct, Enum    // New constraint for C# 7.3
    {
        foreach (T value in Enum.GetValues<T>())
            if (flags.HasFlag(value))
                yield return (T)value;
    }

    public static int HighestSetBit<TEnum>(TEnum value) where TEnum : Enum
    {
        ulong v = Convert.ToUInt64(value);
        if (v == 0UL) return -1;
        return BitOperations.Log2(v);
    }
}
