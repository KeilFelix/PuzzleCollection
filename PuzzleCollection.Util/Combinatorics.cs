using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleCollection.Util
{
    public static class Combinatorics
    {
        // -------------------------
        // Combinations (order doesn't matter, no repetition)
        // -------------------------
        public static IEnumerable<List<T>> Combinations<T>(this IEnumerable<T> source, int length)
        {
            if (length < 0) throw new ArgumentException("Length cannot be negative.");

            if (length == 0)
            {
                yield return new List<T>();
            }
            else
            {
                int index = 0;
                foreach (var item in source)
                {
                    if (length == 1)
                    {
                        yield return new List<T> { item };
                    }
                    else
                    {
                        foreach (var result in source.Skip(index + 1).Combinations(length - 1))
                        {
                            yield return [item, .. result];
                        }
                    }
                    index++;
                }
            }
        }

        public static IEnumerable<List<T>> Combinations<T>(this IEnumerable<T> source)
            => source.Combinations(source.Count());

        // -------------------------
        // Combinations with repetition (order doesn't matter, repetition allowed)
        // -------------------------
        public static IEnumerable<List<T>> CombinationsWithRepeat<T>(this IEnumerable<T> source, int length)
        {
            if (length < 0) throw new ArgumentException("Length cannot be negative.");

            if (length == 0)
            {
                yield return new List<T>();
            }
            else
            {
                int index = 0;
                foreach (var item in source)
                {
                    if (length == 1)
                    {
                        yield return new List<T> { item };
                    }
                    else
                    {
                        foreach (var result in source.Skip(index).CombinationsWithRepeat(length - 1))
                        {
                            yield return [item, .. result];
                        }
                    }
                    index++;
                }
            }
        }

        public static IEnumerable<List<T>> CombinationsWithRepeat<T>(this IEnumerable<T> source)
            => source.CombinationsWithRepeat(source.Count());

        // -------------------------
        // Variations (k-permutations, order matters, no repetition)
        // -------------------------
        public static IEnumerable<List<T>> Variations<T>(this IEnumerable<T> source, int length)
        {
            if (length < 0) throw new ArgumentException("Length cannot be negative.");

            if (length == 0)
            {
                yield return new List<T>();
            }
            else
            {
                int index = 0;
                foreach (var item in source)
                {
                    if (length == 1)
                    {
                        yield return new List<T> { item };
                    }
                    else
                    {
                        foreach (var result in source.Where((_, i) => i != index).Variations(length - 1))
                        {
                            yield return [item, .. result];
                        }
                    }
                    index++;
                }
            }
        }

        public static IEnumerable<List<T>> Variations<T>(this IEnumerable<T> source)
            => source.Variations(source.Count());

        // -------------------------
        // Variations with repetition (order matters, repetition allowed)
        // -------------------------
        public static IEnumerable<List<T>> VariationsWithRepeat<T>(this IEnumerable<T> source, int length)
        {
            if (length < 0) throw new ArgumentException("Length cannot be negative.");

            if (length == 0)
            {
                yield return new List<T>();
            }
            else
            {
                foreach (var item in source)
                {
                    if (length == 1)
                    {
                        yield return new List<T> { item };
                    }
                    else
                    {
                        foreach (var result in source.VariationsWithRepeat(length - 1))
                        {
                            yield return [item, .. result];
                        }
                    }
                }
            }
        }

        public static IEnumerable<List<T>> VariationsWithRepeat<T>(this IEnumerable<T> source)
            => source.VariationsWithRepeat(source.Count());

        // -------------------------
        // Permutations (alias: full-length variations)
        // -------------------------
        public static IEnumerable<List<T>> Permutations<T>(this IEnumerable<T> source)
            => source.Variations(source.Count());
    }
}
