// Copyright ©️ 2026 - Felix Keil (Awesomni.Codes)
// Licensed under the MIT License.

namespace PuzzleCollection.Util;

public static class StackEx
{
    extension<T>(Stack<T> stack)
    {
        public IEnumerable<T> Pop(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return stack.Pop();
            }
        }
    }

}