using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public class Translator
    {
        public string NumberToLetter(int number)
        {
            string letter;
            switch(number)
            {
                case 1:
                    letter = "A";
                    break;
                case 2:
                    letter = "B";
                    break;
                case 3:
                    letter = "C";
                    break;
                case 4:
                    letter = "D";
                    break;
                case 5:
                    letter = "E";
                    break;
                case 6:
                    letter = "F";
                    break;
                case 7:
                    letter = "G";
                    break;
                case 8:
                    letter = "H";
                    break;
                case 9:
                    letter = "I";
                    break;
                case 10:
                    letter = "J";
                    break;
                default:
                    letter = "-999";
                    break;
            }

            return letter;
        }

        public int LetterToNumber(string letter)
        {
            int number;
            switch (letter)
            {
                case "A":
                    number = 1;
                    break;
                case "B":
                    number = 2;
                    break;
                case "C":
                    number = 3;
                    break;
                case "D":
                    number = 4;
                    break;
                case "E":
                    number = 5;
                    break;
                case "F":
                    number = 6;
                    break;
                case "G":
                    number = 7;
                    break;
                case "H":
                    number = 8;
                    break;
                case "I":
                    number = 9;
                    break;
                case "J":
                    number = 10;
                    break;
                default:
                    number = -999;
                    break;
            }

            return number;
        }
    }
}
