using PuzzleCollection.Util;

namespace PuzzleCollection.ProjectEuler;

public class Problem36_DoubleBasePalindromes : IPuzzle
{
    private bool IsDoubleBasePalindrome(int value)
    {
        if(value % 2 == 0 || value % 10 == 0)
        {
            return false;
        }

        return value.GetDigits().IsPalindrome() && value.GetDigits(2).IsPalindrome();
    }

    public string GetSolution()
    {
        var doubleBasePalindromes = Enumerable.Range(1, 999999)
            .Where(IsDoubleBasePalindrome)
            .ToList();

        var sum = doubleBasePalindromes.Sum();

        return $"The sum of all double base palindromes is {sum}";
    }
}
