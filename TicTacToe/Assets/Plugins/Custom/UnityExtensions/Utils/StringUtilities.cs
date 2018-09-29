///////////////////////////////////////////////////////////////
//
// StringUtilities (c) 2017 Muhammad Zain Jahangir
//
// Created by Muhammad Zain Jahangir on 9/13/2017
//
///////////////////////////////////////////////////////////////

using System.Text;

namespace Custom
{
    public static class StringUtilities
    {
        /// <summary>
        /// Removes underscores, converts the first letter to upper case, and adds spaces between words.
        /// </summary>
        public static string ToFriendly(this string name)
        {
            var stringBuilder = new StringBuilder();

            var isFirstLetter = true;
            var lastCharacter = ' ';

            foreach (var character in name)
            {
                if (character == '_')
                {
                    if (!isFirstLetter)
                    {
                        stringBuilder.Append(' ');
                    }
                    lastCharacter = ' ';
                    continue;
                }

                if (isFirstLetter && char.IsLetter(character))
                {
                    var upperCharacter = char.ToUpper(character);
                    stringBuilder.Append(upperCharacter);
                    isFirstLetter = false;
                    lastCharacter = upperCharacter;
                    continue;
                }

                if (char.IsUpper(character) && !char.IsUpper(lastCharacter))
                {
                    stringBuilder.Append(' ');
                }
                stringBuilder.Append(character);

                lastCharacter = character;
            }

            return stringBuilder.ToString();
        }
    }
}
