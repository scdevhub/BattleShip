using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public class Automated
    {
        int min = 1, max;
        public Automated(int _max = 10)
        {
            max = _max;
        }

        public int GetLocation()
        {
            return new Random().Next(min, max);
        }


    }
}
