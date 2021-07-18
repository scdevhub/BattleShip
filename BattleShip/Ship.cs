using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip
{
    public class Ship
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public Coordinates[] CodinatesCollection { get; set; }
        public int Length { get; set; }
    }
}
