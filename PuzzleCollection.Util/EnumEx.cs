// Copyright ©️ 2026 - Felix Keil (Awesomni.Codes)
// Licensed under the MIT License.

using System.Numerics;

namespace PuzzleCollection.Util;

public static class EnumEx
{
    extension<T>(T source) where T : struct, Enum
    {
        public IEnumerable<T> GetSetFlags()
        {
            foreach (T value in Enum.GetValues<T>())
                if (source.HasFlag(value))
                    yield return (T)value;
        }

        public int HighestSetBit()
        {
            ulong v = Convert.ToUInt64(source);
            if (v == 0UL) return -1;
            return BitOperations.Log2(v);
        }
    }

}
