using System;
using BL;
namespace BattleShip.CLII
{
    class Program
    {
        
        static void Main(string[] args)
        {
            bool human = false;
            bool hasWon = false;
            Program prgm = new Program();

            GamerUIBuilder guib = new GamerUIBuilder();
            string name;
            guib.Greeting();

            GameBoard gb1 = new GameBoard();
            gb1.ShipBuildAndPlace();

            GameBoard gb2 = new GameBoard();
            gb2.ShipBuildAndPlace();

            Console.Write("Enter Your Name (Player 1) : ");
            name = Console.ReadLine();
            guib.ShipAssignmentDone(name);

            guib.ShowCoordinateAssignment(name, gb1.shipCollection);

            Console.Write("PC SHIP ASSIGNMENT : ");

            guib.ShowCoordinateAssignment("PC", gb2.shipCollection);

            do
            {
                if (human)
                {
                    Coordinates coordinates = guib.GetValidGameMoveFromUser(gb2);
                    AttackStatus ast = gb2.Attack(coordinates);

                    Console.WriteLine("P1 : Attack Status : "+ast.ToString());
                    
                    hasWon = prgm.CheckForVictory(gb2);

                    if (!hasWon)
                    {
                        human = false; 
                    }
                }
                else
                {
                    ReEvaluate:

                    int x = new Automated().GetLocation();
                    int y = new Automated().GetLocation();
                    Coordinates coordinates = new Coordinates(x, y);
                    bool isDuplicate = gb1.DuplicateAttack(coordinates);

                    if (isDuplicate)
                    {
                        goto ReEvaluate;
                    }
                    else
                    {
                        AttackStatus ast = gb1.Attack(coordinates);
                        Console.WriteLine("P2 : Attack Status : " + ast.ToString());

                    }

                    hasWon = prgm.CheckForVictory(gb1);



                    if (!hasWon)
                    {
                        human = true; 
                    }
                }


            } while (!hasWon);

            if (hasWon)
            {
                Console.WriteLine((human ? "Player 01" : "PC") + " has Won the Game");
                Console.ReadKey();
                Environment.Exit(0);

            }

            //Attack
            //Check Victory
            //Automate Attack For PC
            //Repeat

            Console.ReadKey();
        }

        public bool CheckForVictory(GameBoard gb)
        {
            int maxHealth = 0;

            foreach (var singleShip in gb.shipCollection)
            {
                maxHealth = singleShip.Health;
            }

            if (maxHealth == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
