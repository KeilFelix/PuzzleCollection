namespace PuzzleCollection.Util;

public static class CharEx
{
    public static int AlphabeticalPosition(this char character)
    {
        character = char.ToUpper(character);
        if (character < 'A' || character > 'Z')
            throw new ArgumentOutOfRangeException("Character must be a letter from A to Z.");

        return character - 'A' + 1;

    }
}