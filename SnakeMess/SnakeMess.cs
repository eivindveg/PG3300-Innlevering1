using System.Collections;

namespace SnakeMess
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    ///     The coord.
    /// </summary>
    internal class Coord
    {
        /// <summary>
        /// The ok.
        /// </summary>
        public const string Ok = "Ok";


        /// <summary>
        /// Initializes a new instance of the <see cref="Coord"/> class.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        public Coord(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Coord"/> class.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        public Coord(Coord input)
        {
            X = input.X;
            Y = input.Y;
        }

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        public int Y { get; set; }
    }

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

            // var snake = new List<Coord> {new Coord(10, 10), new Coord(10, 10), new Coord(10, 10), new Coord(10, 10)};
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(10, 10);
            Console.Write("@");
            for (;;)
            {
                app.X = rng.Next(0, board.dimension.x);
                app.Y = rng.Next(0, board.dimension.y);
                var spot = snake.All(i => i.X != app.X || i.Y != app.Y);

                if (!spot)
                {
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(app.X, app.Y);
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
                var tail = new Coord(snake.First());
                var head = new Coord(snake.Last());
                var newHead = new Coord(head);
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
                else if (newHead.Y < 0 || newHead.Y >= boardH)
                {
                    gg = true;
                }

                if (newHead.X == app.X && newHead.Y == app.Y)
                {
                    if (snake.Count + 1 >= boardW * boardH)
                    {
                        // No more room to place apples -- game over.
                        gg = true;
                    }
                    else
                    {
                        for (;;)
                        {
                            app.X = rng.Next(0, boardW);
                            app.Y = rng.Next(0, boardH);
                            var found = snake.All(i => i.X != app.X || i.Y != app.Y);

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
                    snake.RemoveAt(0);
                    if (snake.Any(x => x.X == newHead.X && x.Y == newHead.Y))
                    {
                        gg = true;
                    }
                }

                if (gg)
                {
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(head.X, head.Y);
                Console.Write("O");
                if (!inUse)
                {
                    Console.SetCursorPosition(tail.X, tail.Y);
                    Console.Write(" ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(app.X, app.Y);
                    Console.Write("$");
                    inUse = false;
                }

                snake.Add(newHead);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(newHead.X, newHead.Y);
                Console.Write("@");
                last = newDir;
            }
        }
    }
}