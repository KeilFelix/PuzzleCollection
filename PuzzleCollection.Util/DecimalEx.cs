namespace PuzzleCollection.Util;

public static class DecimalEx
{
    extension(decimal source)
    {
        public IEnumerable<decimal> GetDigits(decimal numBase = 10)
        {
            int sourceInt = (int)source;
            while (sourceInt > 0)
            {
                yield return source % numBase;
                sourceInt /= (int)numBase;
            }
        }
    }
    
    extension(IEnumerable<int> digits)
    {
        public decimal FromDigits(int numBase = 10)
        {
            decimal value = 0;
            foreach (var digit in digits.Reverse())
            {
                value = value * numBase + digit;
            }

            return value;
        }
    }
    
}
