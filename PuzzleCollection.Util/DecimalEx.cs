namespace PuzzleCollection.Util;

public static class DecimalEx
{
    public static IEnumerable<decimal> GetDigits(this decimal source, decimal numBase = 10)
    {
        int sourceInt = (int)source;
        while (sourceInt > 0)
        {
            yield return source % numBase;
            sourceInt /= (int) numBase;
        }
    }

    public static decimal FromDigits(IEnumerable<int> digits, int numBase = 10)
    {
        decimal value = 0;
        foreach (var digit in digits.Reverse())
        {
            value = value * numBase + digit;
        }
        
        return value;
    }
}
