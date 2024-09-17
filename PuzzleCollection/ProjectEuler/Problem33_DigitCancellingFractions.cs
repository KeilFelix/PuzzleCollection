using PuzzleCollection.Util;

namespace PuzzleCollection.ProjectEuler;

public class Problem33_DigitCancellingFractions : IPuzzle
{
    public record Fraction(int Numerator, int Denominator)
    {
        public IEnumerable<Fraction> FractionWithCancelledDigits
        {
            get
            {
                var numeratorDigits = Numerator.GetDigits().ToList();
                var denominatorDigits = Denominator.GetDigits().ToList();

                var commonDigits = numeratorDigits.Intersect(denominatorDigits).ToList();

                foreach (var commonDigit in commonDigits)
                {
                    var newNumerator = new List<int>(numeratorDigits);
                    newNumerator.Remove(commonDigit);

                    var newDenominator = new List<int>(denominatorDigits);
                    newDenominator.Remove(commonDigit);

                    yield return new Fraction(IntEx.FromDigits(newNumerator), IntEx.FromDigits(newDenominator));
                }

            }
        }

        //Helper function, simplifies a fraction.
        public Fraction Simplify()
        {
            int numerator = Numerator;
            int denominator = Denominator;
            for (int divideBy = denominator; divideBy > 0; divideBy--)
            {
                bool divisible = numerator % divideBy == 0 && denominator % divideBy == 0;
                
                if(divisible)
                {
                    numerator /= divideBy;
                    denominator /= divideBy;
                }
            }
            return new Fraction(numerator, denominator);
        }

        public bool HasSameValue(Fraction other)
        {
            return Numerator * other.Denominator == Denominator * other.Numerator;
        }
    }
    public string GetSolution()
    {
        var firstFourDigitCancellingFractions = GetTwoDigitFractions()
            .Where(fraction => fraction.Numerator % 10 != 0 && fraction.Denominator % 10 != 0) // Skip trivial fractions
            .Where(fraction => fraction.FractionWithCancelledDigits.Any(f => fraction.HasSameValue(f)))
            .Take(4);

        var product = firstFourDigitCancellingFractions
            .Select(fraction => fraction.Simplify())
            .Aggregate(new Fraction(1,1), (curFraction, fraction) => new Fraction(curFraction.Numerator * fraction.Numerator, curFraction.Denominator * fraction.Denominator).Simplify());

        return $"The denominator is {product.Denominator}.";
    }

    public IEnumerable<Fraction> GetTwoDigitFractions()
    {
        for (int denominator = 10; denominator < 100; denominator++)
        {
            for (int numerator = 10; numerator < denominator; numerator++)
            {
                yield return new Fraction(numerator, denominator);
            }
        }
    }
}
