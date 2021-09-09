using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;

namespace Snake
{
    class Game
    {
        public Snake snake ;
        public Accessories accessories ;
        public Nutriment nutriment ;
        public Obstacle obstacle ;

        ResultContext context = new ResultContext();

        public void Play()
        {
            snake = new Snake();
            accessories = new Accessories();
            nutriment = new Nutriment();
            obstacle = new Obstacle();

            string choice = Menu();
            Console.Clear();
            switch (choice)
            {
                case "Start Game":
                    Start();
                    break;
                case "Best Results":
                   Results();
                    break;
                case "Exit":
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
        public void CreateField()
        {
            Console.ForegroundColor = ConsoleColor.Blue;


            if (accessories.level == 2)
            {

                accessories.fieldCoordinates[0] = 23;
                accessories.fieldCoordinates[1] = 3;
                accessories.fieldCoordinates[2] = 25;
                accessories.fieldCoordinates[3] = 45;
                accessories.fieldCoordinates[4] = 21;
            }
            if (accessories.level == 3)
            {

                accessories.fieldCoordinates[0] = 21;
                accessories.fieldCoordinates[1] = 4;
                accessories.fieldCoordinates[2] = 24;
                accessories.fieldCoordinates[3] = 41;
                accessories.fieldCoordinates[4] = 19;
            }
            if (accessories.level == 4)
            {

                accessories.fieldCoordinates[0] = 19;
                accessories.fieldCoordinates[1] = 5;
                accessories.fieldCoordinates[2] = 23;
                accessories.fieldCoordinates[3] = 37;
                accessories.fieldCoordinates[4] = 17;
            }
            if (accessories.level == 5)
            {

                accessories.fieldCoordinates[0] = 17;
                accessories.fieldCoordinates[1] = 6;
                accessories.fieldCoordinates[2] = 22;
                accessories.fieldCoordinates[3] = 33;
                accessories.fieldCoordinates[4] = 15;
            }
            Console.SetCursorPosition(Console.WindowWidth / 2 - accessories.fieldCoordinates[0], accessories.fieldCoordinates[1]);
            Console.WriteLine('╔');
            Console.SetCursorPosition(Console.WindowWidth / 2 - (accessories.fieldCoordinates[0] - 1), accessories.fieldCoordinates[1]);
            Console.WriteLine(new string('═', accessories.fieldCoordinates[3]));
            Console.SetCursorPosition(Console.WindowWidth / 2 + accessories.fieldCoordinates[0], accessories.fieldCoordinates[1]);
            Console.WriteLine('╗');
            Console.SetCursorPosition(Console.WindowWidth / 2 - accessories.fieldCoordinates[0], accessories.fieldCoordinates[2]);
            Console.WriteLine('╚');
            Console.SetCursorPosition(Console.WindowWidth / 2 - (accessories.fieldCoordinates[0] - 1), accessories.fieldCoordinates[2]);
            Console.WriteLine(new string('═', accessories.fieldCoordinates[3]));
            Console.SetCursorPosition(Console.WindowWidth / 2 + accessories.fieldCoordinates[0], accessories.fieldCoordinates[2]);
            Console.WriteLine('╝');

            for (int i = 0; i < accessories.fieldCoordinates[4]; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - accessories.fieldCoordinates[0], (accessories.fieldCoordinates[1] + 1) + i);
                Console.WriteLine('║');
            }

            for (int i = 0; i < accessories.fieldCoordinates[4]; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 + accessories.fieldCoordinates[0], (accessories.fieldCoordinates[1] + 1) + i);
                Console.WriteLine('║');
            }
            Console.ResetColor();
        }

        public void Results()
        {
            context = new ResultContext();
            Result[] results = context.Results.ToArray();
            Array.Sort(results);
            Result[] best10 = results.Take(10).ToArray();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition((Console.WindowWidth - 12) / 2, 7);
            Console.WriteLine("Best Results");
            Console.SetCursorPosition((Console.WindowWidth - 15) / 2, 8);
            Console.WriteLine("Score      Data");
            for (int i = 0; i < best10.Length; i++)
            {
                Console.SetCursorPosition((Console.WindowWidth - 21) / 2, 9+i);
                Console.WriteLine($"{i+1}.  {best10[i].score}      {best10[i].dateTime}");
            }

            Console.SetCursorPosition((Console.WindowWidth - 26) / 2, 21);
            Console.WriteLine("Press ENTER to return Menu");
            Console.ResetColor();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Enter)
                    {
                        Play();
                    }
                }
            }

        }

        public void PrintScore()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition((Console.WindowWidth - 8) / 2, 0);
            Console.WriteLine($"Score {accessories.points}");
            Console.SetCursorPosition((Console.WindowWidth - 7) / 2, 1);
            Console.WriteLine($"Level {accessories.level} ");
            Console.ResetColor();
        }
        public void Start()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var Key = Console.ReadKey();
                    ChangeDirection(Key);
                }
                snake.ClearSnake();
                snake.ChangeCoordinates();
                PrintGame();
                IsEatenNutriment();
                Checkings();
                Thread.Sleep(snake.speed);
            }
        }

        private void Checkings()
        {
            for (int i = 0; i < obstacle.left.Count; i++)
            {
                if (obstacle.left[i] == snake.left[0] && obstacle.top[i] == snake.top[0])
                {
                    GameOver();
                }
            }
            for (int i = 1; i < snake.body.Count; i++)
            {
                if (snake.left[0] == snake.left[i] && snake.top[0] == snake.top[i])
                    GameOver();
            }


            if (snake.left[0] == Console.WindowWidth / 2 - accessories.fieldCoordinates[0])
            {
                snake.left[0] = Console.WindowWidth / 2 + (accessories.fieldCoordinates[0] - 1);
                Console.SetCursorPosition(snake.left[0], snake.top[0]);
                Console.WriteLine(snake.body[0]);
            }
            if (snake.left[0] == Console.WindowWidth / 2 + accessories.fieldCoordinates[0])
            {
                snake.left[0] = Console.WindowWidth / 2 - (accessories.fieldCoordinates[0] - 1);
                Console.SetCursorPosition(snake.left[0], snake.top[0]);
                Console.WriteLine(snake.body[0]);
            }
            if (snake.top[0] == accessories.fieldCoordinates[1])
            {
                snake.top[0] = accessories.fieldCoordinates[2] - 1;
                Console.SetCursorPosition(snake.left[0], snake.top[0]);
                Console.WriteLine(snake.body[0]);
            }
            if (snake.top[0] == accessories.fieldCoordinates[2])
            {
                snake.top[0] = accessories.fieldCoordinates[1] + 1;
                Console.SetCursorPosition(snake.left[0], snake.top[0]);
                Console.WriteLine(snake.body[0]);
            }
        }

        public void GameOver()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition((Console.WindowWidth - 9) / 2, 7);
            Console.WriteLine("Game Over");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition((Console.WindowWidth - 13) / 2, 8);
            Console.WriteLine($"Your Score {accessories.points}");
            Console.SetCursorPosition((Console.WindowWidth - 26) / 2, 9);
            Console.WriteLine("Press ENTER to return Menu");
            Console.ResetColor();

            Result result = new Result() { score = accessories.points, dateTime = DateTime.Now.ToShortDateString() };
            context.Results.Add(result);
            context.SaveChanges();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Enter)
                    {
                        Play();
                    }
                }
            }
        }

        private void IsEatenNutriment()
        {
            if (nutriment.left == snake.left[0] && nutriment.top == snake.top[0])
            {
                snake.ClearSnake();
                accessories.points++;
                snake.body.Add('*');
                snake.left.Add(snake.increasingLeft);
                snake.top.Add(snake.increasingTop);
                snake.PrintSnake();

                IsNextLevel();
                ChangeNutriment();
                PrintScore();
                nutriment.PrintNutriment();

            }
        }

        private void IsNextLevel()
        {
            if ((snake.body.Count - 1) % 5 == 0)
            {

                accessories.level++;
                ClearField();
                CreateField();
                ChangeObstacles();
                if (snake.speed > 50)
                {
                    snake.speed -= 50;
                }
                else if (snake.speed > 10)
                {
                    snake.speed -= 20;
                }
            }
        }

        private void ClearField()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - accessories.fieldCoordinates[0], accessories.fieldCoordinates[1]);
            Console.WriteLine(" ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - (accessories.fieldCoordinates[0] - 1), accessories.fieldCoordinates[1]);
            Console.WriteLine(new string(' ', accessories.fieldCoordinates[3]));
            Console.SetCursorPosition(Console.WindowWidth / 2 + accessories.fieldCoordinates[0], accessories.fieldCoordinates[1]);
            Console.WriteLine(" ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - accessories.fieldCoordinates[0], accessories.fieldCoordinates[2]);
            Console.WriteLine(" ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - (accessories.fieldCoordinates[0] - 1), accessories.fieldCoordinates[2]);
            Console.WriteLine(new string(' ', accessories.fieldCoordinates[3]));
            Console.SetCursorPosition(Console.WindowWidth / 2 + accessories.fieldCoordinates[0], accessories.fieldCoordinates[2]);
            Console.WriteLine(" ");

            for (int i = 0; i < accessories.fieldCoordinates[4]; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - accessories.fieldCoordinates[0], (accessories.fieldCoordinates[1] + 1) + i);
                Console.WriteLine(" ");
            }

            for (int i = 0; i < accessories.fieldCoordinates[4]; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 + accessories.fieldCoordinates[0], (accessories.fieldCoordinates[1] + 1) + i);
                Console.WriteLine(" ");
            }
        }

        private void ChangeNutriment()
        {
        noric:

            nutriment.left = new Random().Next(Console.WindowWidth / 2 - (accessories.fieldCoordinates[0] - 1), Console.WindowWidth / 2 + accessories.fieldCoordinates[0]);
            nutriment.top = new Random().Next(accessories.fieldCoordinates[1] + 1, accessories.fieldCoordinates[2]);

            for (int i = 0; i < snake.body.Count; i++)
            {
                if (snake.left[i] == nutriment.left && snake.top[i] == nutriment.top)
                {
                    goto noric;
                }
            }
        }

        private void ChangeObstacles()
        {
            RemoveObstacles();
            AddNewObstacles();
            obstacle.PrintObstacle();

        }

        private void AddNewObstacles()
        {
            for (int i = 0; i < accessories.level; i++)
            {
            changeObstacle:

                obstacle.left.Add(new Random().Next(Console.WindowWidth / 2 - (accessories.fieldCoordinates[0] - 1), Console.WindowWidth / 2 + accessories.fieldCoordinates[0]));
                obstacle.top.Add(new Random().Next((accessories.fieldCoordinates[1] + 1), accessories.fieldCoordinates[2]));

                for (int j = 0; j < snake.body.Count; j++)
                {
                    if (obstacle.left[i] == snake.left[j] && obstacle.top[i] == snake.top[j])
                    {
                        goto changeObstacle;
                    }
                }
            }
        }

        public void PrintGame()
        {
            snake.PrintSnake();
            CreateField();
            PrintScore();
            obstacle.PrintObstacle();
            nutriment.PrintNutriment();
        }

        private void ChangeDirection(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.RightArrow:
                    if (snake.direction != "left")
                        snake.direction = "right";
                    break;
                case ConsoleKey.LeftArrow:
                    if (snake.direction != "right")
                        snake.direction = "left";
                    break;
                case ConsoleKey.UpArrow:
                    if (snake.direction != "down")
                        snake.direction = "up";
                    break;
                case ConsoleKey.DownArrow:
                    if (snake.direction != "up")
                        snake.direction = "down";
                    break;
            }
        }


        private void RemoveObstacles()
        {
            for (int i = 0; i < obstacle.left.Count; i++)
            {
                Console.SetCursorPosition(obstacle.left[i], obstacle.top[i]);
                Console.WriteLine(" ");
                obstacle.left.RemoveAt(i);
                obstacle.top.RemoveAt(i);
                i--;
            }
        }
        public string Menu()
        {
            Console.Clear();
            string cursor = "Start Game";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition((Console.WindowWidth - 10) / 2, 7);
            Console.WriteLine("Start Game");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition((Console.WindowWidth - 12) / 2, 8);
            Console.WriteLine("Best Results");
            Console.SetCursorPosition((Console.WindowWidth - 4) / 2, 9);
            Console.WriteLine("Exit");
            while (true)
            {

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey();
                    switch (key.Key)
                    {
                        case ConsoleKey.DownArrow:
                            switch (cursor)
                            {
                                case "Start Game":
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.SetCursorPosition((Console.WindowWidth - 10) / 2, 7);
                                    Console.WriteLine("Start Game");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.SetCursorPosition((Console.WindowWidth - 12) / 2, 8);
                                    Console.WriteLine("Best Results");
                                    cursor = "Best Results";
                                    break;
                                case "Best Results":
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.SetCursorPosition((Console.WindowWidth - 12) / 2, 8);
                                    Console.WriteLine("Best Results");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.SetCursorPosition((Console.WindowWidth - 4) / 2, 9);
                                    Console.WriteLine("Exit");
                                    cursor = "Exit";
                                    break;
                                case "Exit":
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.SetCursorPosition((Console.WindowWidth - 4) / 2, 9);
                                    Console.WriteLine("Exit");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.SetCursorPosition((Console.WindowWidth - 10) / 2, 7);
                                    Console.WriteLine("Start Game");
                                    cursor = "Start Game";
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            switch (cursor)
                            {
                                case "Start Game":
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.SetCursorPosition((Console.WindowWidth - 10) / 2, 7);
                                    Console.WriteLine("Start Game");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.SetCursorPosition((Console.WindowWidth - 4) / 2, 9);
                                    Console.WriteLine("Exit");
                                    cursor = "Exit";
                                    break;
                                case "Best Results":
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.SetCursorPosition((Console.WindowWidth - 12) / 2, 8);
                                    Console.WriteLine("Best Results");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.SetCursorPosition((Console.WindowWidth - 10) / 2, 7);
                                    Console.WriteLine("Start Game");
                                    cursor = "Start Game";
                                    break;
                                case "Exit":
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.SetCursorPosition((Console.WindowWidth - 4) / 2, 9);
                                    Console.WriteLine("Exit");
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.SetCursorPosition((Console.WindowWidth - 12) / 2, 8);
                                    Console.WriteLine("Best Results");
                                    cursor = "Best Results";
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case ConsoleKey.Enter:
                            Console.ResetColor();
                            return cursor;
                    }
                }
            }
        }
    }
}
