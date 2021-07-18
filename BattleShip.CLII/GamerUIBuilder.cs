using BL;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.CLII
{
    public class GamerUIBuilder
    {
        public void Greeting() 
        {
            Console.WriteLine("********************************************************");
            Console.WriteLine("\t Welcome to Battle Ship Game Play");
            Console.WriteLine("********************************************************");
        }

        public void ShipAssignmentDone(string name)
        {
            Console.WriteLine("\n");
            Console.WriteLine("Player 1 and 2 Ship Assignment is Done...");
            Console.WriteLine("\n");
            Console.WriteLine("Player 1 = "+name+", Player 2 : PC");

        }

        public void ShowCoordinateAssignment(string player, Ship[] shipCollection)
        {
            Console.WriteLine("\n");
            Console.WriteLine("Player : "+player+"'s Ship Locations");


            foreach (var ship in shipCollection)
            {
                Console.Write("\t * "+ship.ShipType.ToString() + " -");

                for (int i = 0; i < ship.GamePositision.Length; i++)
                {
                    int x = Convert.ToInt32(ship.GamePositision[i].X);
                    int y = Convert.ToInt32(ship.GamePositision[i].Y);
                    string getLetter = new Translator().NumberToLetter(x);
                    Console.Write(" " + getLetter + ""+y.ToString()+",");

                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public Coordinates GetValidGameMoveFromUser(GameBoard gameBoard)
        {

            Coordinates coordinates = null ;

            ReEnter:
            Console.Write("What is your next move ? [Type EXIT to Exit] : ");
            string userInput = Console.ReadLine().ToLower();

            if (userInput.Equals("exit"))
            {
                Console.WriteLine("Thanks, press enter to exit");
                Console.ReadLine();
                Environment.Exit(0);
            }
            else if (userInput.Length > 2)
            {
                Console.Write("\nInvalid Input.. Only two Digits Allowed for Coordinations EX:A2,B2. To Exit Type EXIT \n");
                goto ReEnter;
            }
            else if (userInput.Length < 2)
            {
                Console.Write("\nInvalid Input.. Only two Digits Allowed for Coordinations EX:A2,B2. To Exit Type EXIT \n");
                goto ReEnter;
            }
            else
            {
                
                bool isValid = gameBoard.ValidPosition(userInput);
                if (!isValid)
                {
                    Console.Write("\nInvalid Input.. Only two Digits Allowed for Coordinations EX:A2,B2. To Exit Type EXIT \n");
                    goto ReEnter;
                }
                else
                {
                    int xCoordination = new Translator().LetterToNumber(userInput.Substring(0, 1).ToUpper());
                    int yCoordination = Convert.ToInt32(userInput.Substring(1, 1));

                    coordinates = new Coordinates(xCoordination, yCoordination);

                    if (!gameBoard.DuplicateAttack(coordinates))
                    {
                        return coordinates;
                    }
                    else
                    {
                        coordinates = null;
                        Console.Write("\nDuplicate Attack, Retry");
                        goto ReEnter;
                    }

                }
            }

            return coordinates;

        }

        public void GameStatusDisplay()
        {

        }

        
    }
}
