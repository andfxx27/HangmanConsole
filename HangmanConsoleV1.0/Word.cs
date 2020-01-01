using System;
using System.Collections.Generic;
using System.Text;

namespace HangmanConsoleV1._0
{
    public class Word
    {
        public string Words { get; set; }
        public int Score
        {
            get
            {
                int rawScore = 0;
                rawScore += Words.Length * 50;
                return rawScore;
            }
        }

        public Word(string Words)
        {
            this.Words = Words;
        }
    }
}
