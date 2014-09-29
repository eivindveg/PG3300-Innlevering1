using System.Linq;

namespace SnakeMess
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    ///     The snake mess.
    /// </summary>
    internal class SnakeMess
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        public static void Main(string[] arguments)
        {
            bool gg = false, pause = false, inUse = false;
            short newDir = 2; // 0 = up, 1 = right, 2 = down, 3 = left
            var last = newDir;
            var dimension = new Vector(Console.WindowWidth, Console.LargestWindowHeight);
            var snakes = new List<Snake>();
            var snake = new Snake();
            snakes.Add(snake);
            var board = new Board(dimension, snakes);
            int boardW = Console.WindowWidth, boardH = Console.WindowHeight;
            var rng = new Random();
            var position = new Vector();
            Apple app;

            // var snake = new List<Coord> {new Coord(10, 10), new Coord(10, 10), new Coord(10, 10), new Coord(10, 10)};
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(10, 10);
            Console.Write("@");
            for (;;)
            {
                var x = rng.Next(0, board.Dimension.Y);
                var y = rng.Next(0, board.Dimension.Y);
                app = new Apple(EdibleType.RedApple, new Vector(x, y));
                var spot = snake.Components.All(i => i.Position.X != app.Position.X || i.Position.Y != app.Position.Y);

                if (!spot)
                {
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(app.Position.X, app.Position.Y);
                Console.Write("$");
                break;
            }

            var timer = new Stopwatch();
            timer.Start();
            while (!gg)
            {
                if (Console.KeyAvailable)
                {
                    var cki = Console.ReadKey(true);
                    switch (cki.Key)
                    {
                        case ConsoleKey.Escape:
                            gg = true;
                            break;
                        case ConsoleKey.Spacebar:
                            pause = !pause;
                            break;
                        default:
                            if (cki.Key == ConsoleKey.UpArrow && last != 2)
                            {
                                newDir = 0;
                            }
                            else if (cki.Key == ConsoleKey.RightArrow && last != 3)
                            {
                                newDir = 1;
                            }
                            else if (cki.Key == ConsoleKey.DownArrow && last != 0)
                            {
                                newDir = 2;
                            }
                            else if (cki.Key == ConsoleKey.LeftArrow && last != 1)
                            {
                                newDir = 3;
                            }

                            break;
                    }
                }

                if (pause || timer.ElapsedMilliseconds < 100)
                {
                    continue;
                }

                timer.Restart();
                var tail = snake.Components.First();
                var head = snake.Components.Last();
                var newHead = new Vector(head.Position.X, head.Position.Y);
                switch (newDir)
                {
                    case 0:
                        newHead.Y -= 1;
                        break;
                    case 1:
                        newHead.X += 1;
                        break;
                    case 2:
                        newHead.Y += 1;
                        break;
                    default:
                        newHead.X -= 1;
                        break;
                }

                if (newHead.X < 0 || newHead.X >= boardW)
                {
                    gg = true;
                }
                else if (newHead.X < 0 || newHead.X >= boardH)
                {
                    gg = true;
                }

                if (newHead.X == app.Position.X && newHead.Y == app.Position.Y)
                {
                    if (snake.Components.Count + 1 >= boardW * boardH)
                    {
                        // No more room to place apples -- game over.
                        gg = true;
                    }
                    else
                    {
                        for (;;)
                        {
                            var x = rng.Next(0, boardW);
                            var y = rng.Next(0, boardH);
                            app.Position = new Vector(x, y);
                            var found = snake.Components.All(i => i.Position.X != app.Position.X || i.Position.Y != app.Position.Y);

                            if (!found)
                            {
                                continue;
                            }

                            inUse = true;
                            break;
                        }
                    }
                }

                if (!inUse)
                {
                    snake.Components.RemoveAt(0);
                    if (snake.Components.Any(x => x.Position.X == newHead.X && x.Position.Y == newHead.Y))
                    {
                        gg = true;
                    }
                }

                if (gg)
                {
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(head.Position.X, head.Position.X);
                Console.Write("O");
                if (!inUse)
                {
                    Console.SetCursorPosition(tail.Position.X, tail.Position.Y);
                    Console.Write(" ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(app.Position.X, app.Position.Y);
                    Console.Write("$");
                    inUse = false;
                }

                snake.Components.Add(new SnakeComponent(newHead, SnakePart.Head));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(newHead.X, newHead.Y);
                Console.Write("@");
                last = newDir;
            }
        }
    }
}