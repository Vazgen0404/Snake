using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class Accessories
    {
        public int points;
        public int level;
        public int[] fieldCoordinates;

        public Accessories()
        {
            fieldCoordinates = new int[5] { 25, 2, 26, 49, 23 };
            level = 1;
        }
    }
}
