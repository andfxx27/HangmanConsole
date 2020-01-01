using System;
using System.Collections.Generic;
using System.Text;

namespace HangmanConsoleV1._0
{
    public class Match
    {
        private List<Word> GuessedWords = new List<Word>();
        public string PlayerName { get; set; }
        public int PlayerHealth { get; set; }
        public int PlayerScore
        {
            get
            {
                int score = 0;
                foreach (var item in GuessedWords)
                {
                    score += item.Score;
                }

                return score;
            }
        }

        public Match(string PlayerName)
        {
            this.PlayerName = PlayerName;
            this.PlayerHealth = 8;
        }

        public void MakeCorrectGuess(string guessedWord)
        {
            Word GuessedWord = new Word(guessedWord);

            this.GuessedWords.Add(GuessedWord);
        }
    }
}
