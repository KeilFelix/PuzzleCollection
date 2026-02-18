namespace PuzzleCollection.Util;

public static class CharEx
{
    extension(char character)
    {
        public int AlphabeticalPosition()
        {
            character = char.ToUpper(character);
            if (character < 'A' || character > 'Z')
                throw new ArgumentOutOfRangeException("Character must be a letter from A to Z.");

            return character - 'A' + 1;
        }
    }
        
}