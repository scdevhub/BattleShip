using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinates(int _X, int _Y)
        {
            X = _X;
            Y = _Y;
        }

        public override int GetHashCode()
        {
            string uniqueHash = this.X.ToString() + this.Y.ToString() + "00";
            return (Convert.ToInt32(uniqueHash));
        }

        public override bool Equals(object obj)
        {
            Coordinates receivedCoordinates = obj as Coordinates;

            if (receivedCoordinates == null)
                return false;

            return receivedCoordinates.X == this.X &&
                   receivedCoordinates.Y == this.Y;
        }
    }
}
