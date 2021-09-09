using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class Obstacle
    {
        public List<int> left = new List<int>();
        public List<int> top = new List<int>();

        public Obstacle()
        {
            left.Add(new Random().Next(Console.WindowWidth / 2 - 24, Console.WindowWidth / 2 + 25));
            top.Add(new Random().Next(3,26));
        }

        public void PrintObstacle()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < left.Count; i++)
            {
                Console.SetCursorPosition(left[i], top[i]);
                Console.WriteLine('X');
            }
            Console.ResetColor();
        }
        
    }
}
