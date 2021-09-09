using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class Snake
    {
        public List<char> body = new List<char>();
        public List<int> left = new List<int>();
        public List<int> top = new List<int>();

        public int increasingLeft;
        public int increasingTop;
        public string direction;
        public int speed;

        public Snake()
        {
            body.Add('*');
            left.Add((Console.WindowWidth - 1) / 2);
            top.Add(14);
            speed = 250;
        }

        public void PrintSnake()
        {
            for (int i = 0; i < body.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(left[i], top[i]);
                Console.WriteLine(body[i]);
                Console.ResetColor();
            }
        }
        public void ClearSnake()
        {
            for (int i = 0; i < body.Count; i++)
            {
                Console.SetCursorPosition(left[i], top[i]);
                Console.WriteLine(" ");
            }
        }
        public void ChangeCoordinates()
        {
            for (int i = body.Count - 1; i > 0; i--)
            {
                if (i == body.Count - 1)
                {
                   increasingLeft = left[i];
                   increasingTop = top[i];
                }
                left[i] = left[i - 1];
                top[i] = top[i - 1];
            }
            switch (direction)
            {
                case "left":
                    left[0]--;
                    break;
                case "right":
                    left[0]++;
                    break;
                case "up":
                    top[0]--;
                    break;
                case "down":
                    top[0]++;
                    break;
            }
        }
    }
}
