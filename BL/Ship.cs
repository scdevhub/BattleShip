using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public class Ship
    {
        public ShipType ShipType { get; set; }
        public int Health { get; set; }
        public Coordinates[] GamePositision { get; set; }
        public int Length { get; set; }

        public Ship(ShipType _shipType, int _length)
        {
            ShipType = _shipType;
            Length = _length;
            Health = _length;
            GamePositision = new Coordinates[_length];
        }
    }
}
