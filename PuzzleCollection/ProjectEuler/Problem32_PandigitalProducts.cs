// Copyright ©️ 2026 - Felix Keil (Awesomni.Codes)
// Licensed under the MIT License.

using PuzzleCollection.Util;
namespace PuzzleCollection.ProjectEuler;

public class Problem32_PandigitalProducts : IPuzzle
{
    public record Product(int Multiplicand, int Multiplier)
    {
        public int Value => Multiplicand * Multiplier;

        public bool IsPandigital()
        {
            var digits = new System.Collections.Generic.HashSet<int>();
            foreach (var digit in Multiplicand.ToDigits().Concat(Multiplier.ToDigits()).Concat(Value.ToDigits()))
            {
                if (digit == 0 || !digits.Add(digit))
                {
                    return false;
                }
            }
            return digits.Count == 9;
        }
    }

    public string GetSolution()
    {
        int maxMultiplier = 9876;

        var products = new List<Product>();
        for (int multiplicand = 1; multiplicand < maxMultiplier; multiplicand++)
        {
            for (int multiplier = 1; multiplier < maxMultiplier; multiplier++)
            {
                var product = new Product(multiplicand, multiplier);
                if (product.IsPandigital())
                {
                    products.Add(product);
                }
            }
        }

        return $"The sum is {products.DistinctBy(p => p.Value).Sum(p => p.Value)}.";
    }
}
