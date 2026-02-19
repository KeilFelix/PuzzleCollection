// Copyright ©️ 2026 - Felix Keil (Awesomni.Codes)
// Licensed under the MIT License.

namespace PuzzleCollection.Util
{
    public static class EqualityComparers
    {
        public class ListComparer<T> : IEqualityComparer<List<T>>
        {
            public bool Equals(List<T> x, List<T> y)
            {
                return x.SequenceEqual(y);
            }

            public int GetHashCode(List<T> obj)
            {
                // Combine hash codes of elements for a decent hash
                unchecked
                {
                    int hash = 19;
                    foreach (var item in obj)
                        hash = hash * 31 + item?.GetHashCode() ?? 0;
                    return hash;
                }
            }
        }
    }

}
