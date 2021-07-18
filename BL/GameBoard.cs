using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace BL
{
    public class GameBoard
    {
        private const int MaxWidth = 10;
        private const int MaxHeight = 10;
        private const int MaxDistroyers = 2;
        private const int MaxBattleShips = 1;

        Dictionary<Coordinates, AttackStatus> shotsTaken;

        public Ship[] shipCollection { get; set; }

        public GameBoard()
        {
            shotsTaken = new Dictionary<Coordinates, AttackStatus>();
            shipCollection = new Ship[MaxDistroyers + MaxBattleShips];
        }

        public void ShipBuildAndPlace()
        {
            int builtDistroyersCount = 0;
            int builtBattleShipCount = 0;

            for (int i = 0; i < shipCollection.Length; i++)
            {
                if (builtBattleShipCount <  MaxBattleShips)
                {
                    Ship newShip = new ShipBuilder().BuildShip(ShipType.Battleship);
                    newShip.GamePositision = PositionShip(newShip.Length);
                    shipCollection[i] = newShip;
                    builtBattleShipCount = builtBattleShipCount + 1;
                }
                else if (builtDistroyersCount < MaxDistroyers)
                {
                    Ship newShip = new ShipBuilder().BuildShip(ShipType.Destroyer);
                    newShip.GamePositision = PositionShip(newShip.Length);
                    shipCollection[i] = newShip;
                    builtDistroyersCount = builtDistroyersCount + 1;
                }
                
            }
        }

        private Coordinates[] PositionShip(int shipSize) 
        {
            bool noOverlaps = false;
            bool hasFullCoordinates = true;
            int xPosition = 0;
            int yPosition = 0;
            Coordinates co;
            Coordinates[] returnedCoordinates = new Coordinates[shipSize - 1];
            Coordinates[] finalCoordinates = new Coordinates[shipSize];
            do
            {
                hasFullCoordinates = false;
                xPosition = new Automated().GetLocation();
                yPosition = new Automated().GetLocation();

                co = new Coordinates(xPosition, yPosition);

                noOverlaps = OverlapsAnotherShip(co);

                returnedCoordinates = TraverseLeftOrRight(co, shipSize - 1);

                if (!returnedCoordinates.Contains(null))
                {
                    finalCoordinates[0] = co;

                    for (int i = 0; i < finalCoordinates.Length - 1; i++)
                    {
                        finalCoordinates[i+1] = returnedCoordinates[i];
                    }

                    hasFullCoordinates = true;
                }

                if (!hasFullCoordinates)
                {
                    returnedCoordinates = TraverseUpOrDown(co, shipSize - 1); 
                }

                if (!returnedCoordinates.Contains(null)  && !hasFullCoordinates)
                {
                    finalCoordinates[0] = co;

                    for (int i = 0; i < finalCoordinates.Length - 1; i++)
                    {
                        finalCoordinates[i + 1] = returnedCoordinates[i];
                    }

                    hasFullCoordinates = true;
                }


            } while (noOverlaps && !hasFullCoordinates);


            if (finalCoordinates.Contains(null))
            {
                Console.WriteLine("NULL X " + xPosition.ToString() + " Y "+yPosition.ToString());
            }

            return finalCoordinates;
        }

        private Coordinates[] TraverseLeftOrRight(Coordinates coordinates, int length)
        {
            int currentX = coordinates.X;
            int currentY = coordinates.Y;

            Coordinates[] leftOrRight = new Coordinates[length];
            bool noOverlaps = false;
            bool overMax = (currentY + length) > MaxWidth ? true : false;
            bool overMin = (currentY - length) < 1 ? true : false;


            if (!overMax)
            {
                for (int i = 0; i < length; i++)
                {
                    noOverlaps = OverlapsAnotherShip(new Coordinates(currentX, currentY + i + 1));
                    if (!noOverlaps)
                    {
                        leftOrRight[i] = new Coordinates(currentX, currentY + i + 1);
                    }
                    else
                    {
                        leftOrRight = new Coordinates[length];
                        break;
                    }
                } 
            }

            if (!overMin && leftOrRight[0] == null)
            {
                for (int i = 0; i < length; i++)
                {
                    noOverlaps = OverlapsAnotherShip(new Coordinates(currentX, currentY - (i + 1)));
                    if (!noOverlaps)
                    {
                        leftOrRight[i] = new Coordinates(currentX, currentY - (i + 1));
                    }
                    else
                    {
                        leftOrRight = new Coordinates[length];
                        break;
                    }
                }
            }

            return leftOrRight;

        }

        private Coordinates[] TraverseUpOrDown(Coordinates coordinates, int length)
        {
            int currentX = coordinates.X;
            int currentY = coordinates.Y;

            Coordinates[] upOrDown = new Coordinates[length];
            bool noOverlaps = false;
            bool overMax = (currentX + length) > MaxWidth ? true : false;
            bool overMin = (currentX - length) < 1 ? true : false;


            if (!overMax)
            {
                for (int i = 0; i < length; i++)
                {
                    noOverlaps = OverlapsAnotherShip(new Coordinates(currentX + i + 1, currentY));
                    if (!noOverlaps)
                    {
                        upOrDown[i] = new Coordinates(currentX + i + 1, currentY);
                    }
                    else
                    {
                        upOrDown = new Coordinates[length];
                        break;
                    }
                }
            }

            if (!overMin && upOrDown[0] == null)
            {
                for (int i = 0; i < length; i++)
                {
                    noOverlaps = OverlapsAnotherShip(new Coordinates(currentX - (i + 1), currentY));
                    if (!noOverlaps)
                    {
                        upOrDown[i] = new Coordinates(currentX - (i + 1), currentY);
                    }
                    else
                    {
                        upOrDown = new Coordinates[length];
                        break;
                    }
                }
            }

            return upOrDown;

        }

        private bool OverlapsAnotherShip(Coordinates coordinate)
        {
            foreach (var singleShip in shipCollection)
            {
                if (singleShip != null)
                {
                    if (singleShip.GamePositision.Contains(coordinate))
                        return true;
                }
            }

            return false;
        }

        public AttackStatus Attack(Coordinates coordinates)
        {
            AttackStatus ast = AttackStatus.NA;

            foreach (var singleShip in shipCollection)
            {
                if (singleShip.GamePositision.Contains(coordinates))
                {
                    singleShip.Health -= 1;

                    if (singleShip.Health == 0)
                    {
                        ast = AttackStatus.HitAndSunk;
                        shotsTaken.Add(coordinates, ast);
                        break;
                    }
                    else
                    {
                        ast = AttackStatus.Hit;
                        shotsTaken.Add(coordinates, ast);
                        break;
                    }


                }
                
            }

            if (ast == AttackStatus.NA)
            {
                ast = AttackStatus.Miss;
                shotsTaken.Add(coordinates, ast);
            }

            return ast;
        }

        public bool ValidPosition(string position)
        {
            string firstPart = position.Substring(0, 1);
            string secondPart = position.Substring(1, 1);

            int result;


            if (Regex.Matches(firstPart, @"[a-jA-J]").Count == 0)
            {
                return false;
            }
            else if (!Int32.TryParse(secondPart, out result))
            {
                return false;

            }
            else if (result >= 1 && result <= 10)
            {
                return true;
            }
            else
            {
                return true;
            }
        }
        public bool DuplicateAttack(Coordinates coordinates)
        {
            if (shotsTaken.ContainsKey(coordinates))
            {
                return true;
            }

            return false;
        }

       



    }
}
