// Copyright ©️ 2026 - Felix Keil (Awesomni.Codes)
// Licensed under the MIT License.

namespace PuzzleCollection.Util
{
    public static class Combinatorics
    {
        extension<T>(IEnumerable<T> source)
        {
            // -------------------------
            // Combinations (order doesn't matter, no repetition)
            // -------------------------
            public IEnumerable<List<T>> Combinations(int length)
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

            public IEnumerable<List<T>> Combinations()
                => source.Combinations(source.Count());

            // -------------------------
            // Combinations with repetition (order doesn't matter, repetition allowed)
            // -------------------------
            public IEnumerable<List<T>> CombinationsWithRepeat(int length)
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

            public IEnumerable<List<T>> CombinationsWithRepeat()
                => source.CombinationsWithRepeat(source.Count());

            // -------------------------
            // Variations (k-permutations, order matters, no repetition)
            // -------------------------
            public IEnumerable<List<T>> Variations(int length)
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

            public IEnumerable<List<T>> Variations()
                => source.Variations(source.Count());

            // -------------------------
            // Variations with repetition (order matters, repetition allowed)
            // -------------------------
            public IEnumerable<List<T>> VariationsWithRepeat(int length)
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

            public IEnumerable<List<T>> VariationsWithRepeat()
                => source.VariationsWithRepeat(source.Count());

            // -------------------------
            // Permutations (alias: full-length variations)
            // -------------------------
            public IEnumerable<List<T>> Permutations()
                => source.Variations(source.Count());
        }
    }
}
