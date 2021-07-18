using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public class ShipBuilder
    {
        public Ship BuildShip(ShipType shipType)
        {
            if (shipType == ShipType.Battleship)
            {
                return new Ship(ShipType.Battleship, 5);
            }
            else
            {
                return new Ship(ShipType.Destroyer, 4);
            }
        }
    }
}
