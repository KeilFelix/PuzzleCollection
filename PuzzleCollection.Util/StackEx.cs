namespace PuzzleCollection.Util;

public static class StackEx
{
    public static IEnumerable<TSource> Pop<TSource>(this Stack<TSource> stack, int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return stack.Pop();
        }
    }
}