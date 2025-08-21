using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
