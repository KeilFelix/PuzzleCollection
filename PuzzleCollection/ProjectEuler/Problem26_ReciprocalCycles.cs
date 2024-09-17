namespace PuzzleCollection.ProjectEuler;

public class Problem26_ReciprocalCycles : IPuzzle
{
    static int FindRecurringCycleLength(int d)
    {
        List<int> remainders = new List<int>();
        int numerator = 1;

        for (int i = 0; ; i++)
        {
            numerator %= d;

            if (numerator == 0)
                return 0; // Keine Wiederholung

            if (remainders.Contains(numerator))
                return i - remainders.IndexOf(numerator);

            remainders.Add(numerator);
            numerator *= 10;
        }
    }

    public string GetSolution()
    {
        int maxD = 1000;
        int maxLength = 0;
        int resultD = 0;

        for (int d = 1; d <= maxD; d++)
        {
            int length = FindRecurringCycleLength(d);
            if (length > maxLength)
            {
                maxLength = length;
                resultD = d;
            }
        }

        return $"Die längste wiederkehrende Dezimalzahl hat {maxLength} Stellen und tritt bei 1/{resultD} auf.";
    }
}
