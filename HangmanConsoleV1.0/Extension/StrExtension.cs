using System;
using System.Collections.Generic;
using System.Text;

namespace HangmanConsoleV1._0.Extension
{
    public static class StrExtension
    {
        public static IEnumerable<int> MakeGuess(this string word, string character)
        {
            int minIndex = word.IndexOf(character);
            while (minIndex != -1)
            {
                yield return minIndex;
                minIndex = word.IndexOf(character, minIndex + 1);
            }
        }
    }
}
