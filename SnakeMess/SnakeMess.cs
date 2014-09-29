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
            Apple app;

            // var snake = new List<Coord> {new Coord(10, 10), new Coord(10, 10), new Coord(10, 10), new Coord(10, 10)};
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(10, 10);
            Console.Write("@");
            for (;;)
            {
                var x = rng.Next(0, board.dimension.x);
                var y = rng.Next(0, board.dimension.y);
                app = new RedApple(new Vector(x, y));
                var spot = snake.components.All(i => i.position.x != app.position.x || i.position.y != app.position.y);

                if (!spot)
                {
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(app.position.x, app.position.y);
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
                var tail = snake.components.First();
                var head = snake.components.Last();
                var newHead = new Vector(head.position.x, head.position.y);
                switch (newDir)
                {
                    case 0:
                        newHead.y -= 1;
                        break;
                    case 1:
                        newHead.x += 1;
                        break;
                    case 2:
                        newHead.y += 1;
                        break;
                    default:
                        newHead.x -= 1;
                        break;
                }

                if (newHead.x < 0 || newHead.x >= boardW)
                {
                    gg = true;
                }
                else if (newHead.x < 0 || newHead.x >= boardH)
                {
                    gg = true;
                }

                if (newHead.x == app.position.x && newHead.y == app.position.y)
                {
                    if (snake.components.Count + 1 >= boardW * boardH)
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
                            app.position = new Vector(x, y);
                            var found = snake.components.All(i => i.position.x != app.position.x || i.position.y != app.position.y);

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
                    snake.components.RemoveAt(0);
                    if (snake.components.Any(x => x.position.x == newHead.x && x.position.y == newHead.y))
                    {
                        gg = true;
                    }
                }

                if (gg)
                {
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(head.position.x, head.position.x);
                Console.Write("O");
                if (!inUse)
                {
                    Console.SetCursorPosition(tail.position.x, tail.position.y);
                    Console.Write(" ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(app.position.x, app.position.y);
                    Console.Write("$");
                    inUse = false;
                }

                snake.components.Add(new SnakeComponent(newHead));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(newHead.x, newHead.y);
                Console.Write("@");
                last = newDir;
            }
        }
    }
}