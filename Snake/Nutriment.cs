using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class Nutriment
    {
        public int left;
        public int top;

        public Nutriment()
        {
            left = new Random().Next(Console.WindowWidth / 2 - 24, Console.WindowWidth / 2 + 25);
            top = new Random().Next(3, 26);
        }

        public void PrintNutriment()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(left, top);
            Console.WriteLine('O');
            Console.ResetColor();
        }   
    }
}
