using PuzzleCollection.Util;

namespace PuzzleCollection.ProjectEuler;

public class Problem43_SubStringDivisibility : IPuzzle
{
    public string GetSolution()
    {
        var multiplesOf2 = IntEx.MultiplesOf(2).TakeWhile(n => n < 1000).SelectMany(UniqueThreeDigitsFilter).ToList();
        var multiplesOf3 = IntEx.MultiplesOf(3).TakeWhile(n => n < 1000).SelectMany(UniqueThreeDigitsFilter).ToList();
        var multiplesOf5 = IntEx.MultiplesOf(5).TakeWhile(n => n < 1000).SelectMany(UniqueThreeDigitsFilter).ToList();
        var multiplesOf7 = IntEx.MultiplesOf(7).TakeWhile(n => n < 1000).SelectMany(UniqueThreeDigitsFilter).ToList();
        var multiplesOf11 = IntEx.MultiplesOf(11).TakeWhile(n => n < 1000).SelectMany(UniqueThreeDigitsFilter).ToList();
        var multiplesOf13 = IntEx.MultiplesOf(13).TakeWhile(n => n < 1000).SelectMany(UniqueThreeDigitsFilter).ToList();
        var multiplesOf17 = IntEx.MultiplesOf(17).TakeWhile(n => n < 1000).SelectMany(UniqueThreeDigitsFilter).ToList();
        var digits = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        var substringDivisiblePandigitals = new List<decimal>();
        foreach (var d1 in digits)
        {
            IEnumerable<int> curDigits1 = Enumerable.Repeat(d1, 1);
            foreach (var digits2 in multiplesOf2)
            {
                var curDigits2 = curDigits1.Concat(digits2).Memoize();
                if (curDigits2.Distinct().Count() != 4) continue;

                foreach (var digits3 in multiplesOf3)
                {
                    if (digits2[1] != digits3[0]) continue;
                    if (digits2[2] != digits3[1]) continue;

                    var curDigits3 = curDigits2.Append(digits3[2]).Memoize();
                    if (curDigits3.Distinct().Count() != 5) continue;

                    foreach (var digits5 in multiplesOf5)
                    {
                        if (digits3[1] != digits5[0]) continue;
                        if (digits3[2] != digits5[1]) continue;

                        var curDigits5 = curDigits3.Append(digits5[2]).Memoize();
                        if (curDigits5.Distinct().Count() != 6) continue;

                        foreach (var digits7 in multiplesOf7)
                        {
                            if (digits5[1] != digits7[0]) continue;
                            if (digits5[2] != digits7[1]) continue;

                            var curDigits7 = curDigits5.Append(digits7[2]).Memoize();
                            if (curDigits7.Distinct().Count() != 7) continue;

                            foreach (var digits11 in multiplesOf11)
                            {
                                if (digits7[1] != digits11[0]) continue;
                                if (digits7[2] != digits11[1]) continue;

                                var curDigits11 = curDigits7.Append(digits11[2]).Memoize();
                                if (curDigits11.Distinct().Count() != 8) continue;

                                foreach (var digits13 in multiplesOf13)
                                {
                                    if (digits11[1] != digits13[0]) continue;
                                    if (digits11[2] != digits13[1]) continue;

                                    var curDigits13 = curDigits11.Append(digits13[2]).Memoize();
                                    if (curDigits13.Distinct().Count() != 9) continue;

                                    foreach (var digits17 in multiplesOf17)
                                    {
                                        if (digits13[1] != digits17[0]) continue;
                                        if (digits13[2] != digits17[1]) continue;

                                        var curDigits17 = curDigits13.Append(digits17[2]).Memoize();
                                        if (curDigits17.Distinct().Count() != 10) continue;

                                        substringDivisiblePandigitals.Add(DecimalEx.FromDigits(curDigits17.Reverse()));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        

        return $"The sum of all is {substringDivisiblePandigitals.Sum()}";
    }

    public IEnumerable<List<int>> UniqueThreeDigitsFilter(int number)
    {
        var digits = IntEx.GetDigits(number).Reverse().ToList();

        while (digits.Count < 3)
        {
            digits.Insert(0, 0);
        }
        if (digits.Distinct().Count() != 3) return Enumerable.Empty<List<int>>();
        return new List<List<int>> { digits };
    }

}

