using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class Result : IComparable
    {
        public int Id { get; set; }
        public int score { get; set; }
        public string dateTime { get; set; }

        public int CompareTo(object obj)
        {
            Result temp = (Result)obj;
            if (temp.score > this.score)
            {
                return 1;
            }
            else if (temp.score < this.score)
            {
                return -1;
            }
            else return 0;
        }
    }
}
