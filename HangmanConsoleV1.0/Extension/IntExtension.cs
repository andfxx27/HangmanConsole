using System;
using System.Collections.Generic;
using System.Text;

namespace HangmanConsoleV1._0
{
    public static class IntExtension
    {
        public static bool BetweenRange(this int input, int firstNum, int secondNum)
        {
            return (input >= firstNum && input <= secondNum);
        }
    }
}
