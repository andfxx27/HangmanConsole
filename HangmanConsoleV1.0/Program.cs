using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HangmanConsoleV1._0.Extension;

namespace HangmanConsoleV1._0
{
    public class Program
    {
        static void Main(string[] args)
        {
            Match wonMatch;
            var listMatch = new List<Match>();

            int choice;
            string input;
            bool isActive = true;
            bool isValid = false;

            while (isActive)
            {
                #region "Intro"
                Console.WriteLine("Welcome to Hangman Console V1.0!");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("1. Play");
                Console.WriteLine("2. Tutorial");
                Console.WriteLine("3. Highschore");
                Console.WriteLine("4. Exit");
                Console.Write("Your option: ");
                
                input = Console.ReadLine();
                while (!isValid)
                {
                    bool isConverted = Int32.TryParse(input, out int result);

                    if (isConverted)
                    {
                        if (result.BetweenRange(1, 4))
                        {
                            isValid = true;
                        }
                    }

                    if (isValid.Equals(false))
                    {
                        Console.WriteLine("Please enter a valid input. It must be a number between 1 & 4.");
                        Console.Write("Your option: ");
                        input = Console.ReadLine();
                    }
                }

                choice = Int32.Parse(input);
                #endregion

                #region "Exit Game"
                if (choice.Equals(4))
                {
                    return;
                }
                #endregion

                switch (choice)
                {
                    case 1:

                        string[] listWords;

                        try
                        {
                            string filePath = Path.GetFullPath("Words.txt");
                            using (StreamReader streamReader = new StreamReader(filePath))
                            {
                                string words = streamReader.ReadToEnd();
                                listWords = words.Split(',');
                                streamReader.Close();
                            }

                            if (listWords.Length > 0 && !listWords[0].Equals(""))
                            {
                                Console.Clear();
                                wonMatch = Hangman.Play(listWords);
                                listMatch.Add(wonMatch);
                            }
                            else
                            {
                                Console.WriteLine("No words found.");
                                break;
                            }
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("Problem found while loading the game.");
                            Console.WriteLine(e.Message);
                        }

                        break;
                    case 2:
                        #region "Tutorial"
                        try
                        {
                            string filePath = Path.GetFullPath("Instruction.txt");
                            using (StreamReader streamReader = new StreamReader(filePath))
                            {
                                string instruction = streamReader.ReadToEnd();
                                Console.WriteLine(instruction);
                                Console.WriteLine();
                                streamReader.Close();
                            }
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("The instruction manual could not be read.");
                            Console.WriteLine(e.Message);
                        }
                        break;
                    #endregion
                    case 3:
                        #region "Highscore"
                        var highscore = new System.Text.StringBuilder();
                        #endregion
                        break;
                }

                #region "Match Transition"
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
                Console.Clear();
                #endregion
            }
        }
    }

    class Hangman
    {
        public static Match Play(string[] listWords)
        {
            var permaIndex = new List<int>();
            var guessedCharacter = new List<string>();
            List<int> listIndex;

            string inputGuess;
            string userName;
            bool isValid = false;

            /// <summary>
            /// The next part ask user to input their username before the game starts
            /// </summary>
            #region "Gameplay Intro"
            Console.Write("Please input your name [Any string between 8 - 12 characters]: ");
            userName = Console.ReadLine();

            while (!isValid)
            {
                if (userName.Length >= 8 && userName.Length <= 12)
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid input. It must be a string between 8 - 12 characters.");
                    Console.Write("Please input your name: ");
                    userName = Console.ReadLine();
                }
            }
            #endregion

            Match match = new Match(userName);
            
            foreach (var item in listWords)
            {
                Console.Clear();
                for (int i = 0; i < item.Length; i++)
                {
                    Console.Write(" _ ");
                }

                Console.WriteLine();
                Console.Write("Guess a character: ");

                while (match.PlayerHealth > 0)
                {
                    inputGuess = Console.ReadLine();

                    while (guessedCharacter.Contains(inputGuess))
                    {
                        Console.WriteLine("Please input another character.");
                        Console.Write("Guess a character: ");
                        inputGuess = Console.ReadLine();
                    }

                    listIndex = item.MakeGuess(inputGuess).ToList();
                    Console.WriteLine();

                    if (listIndex.Count.Equals(0))
                    {
                        match.PlayerHealth--;
                        Console.WriteLine($"Wrong guess! Please try again. You have {match.PlayerHealth} point(s) left.");
                        guessedCharacter.Add(inputGuess);
                    }
                    else
                    {
                        /// <summary>
                        /// The next part display the correctly guessed character.
                        /// </summary>
                        #region "Guess Result"
                        foreach (var index in listIndex)
                        {
                            permaIndex.Add(index);
                        }

                        if (permaIndex.Count <= item.Length)
                        {
                            Console.Clear();
                            for (int i = 0; i < item.Length; i++)
                            {
                                if (permaIndex.Contains(i))
                                {
                                    Console.Write($" {item[i]} ");
                                }
                                else
                                {
                                    Console.Write(" _ ");
                                }
                            }
                        }

                        if (permaIndex.Count == item.Length)
                        {
                            permaIndex = new List<int>();
                            guessedCharacter = new List<string>();
                            match.MakeCorrectGuess(item);
                            Console.WriteLine();
                            Console.WriteLine("Congratulation! You won this stage..");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadLine();
                            break;
                        }
                        #endregion
                    }

                    Console.WriteLine();
                    Console.Write("Guess a character: ");
                }
            }

            return match;
        } 
    }
}
