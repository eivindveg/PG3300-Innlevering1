namespace SnakeMess
{
    using System;
    using System.Diagnostics;
    using System.Linq;

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
            var dimension = new Vector(Console.WindowWidth, Console.WindowHeight);
            Debug.WriteLine(dimension);
            var player = new Player(1);
            var board = new Board(dimension, player);
            int boardW = Console.WindowWidth, boardH = Console.WindowHeight;
            var rng = new Random();
            Apple app;

            // var snake = new List<Coord> {new Coord(10, 10), new Coord(10, 10), new Coord(10, 10), new Coord(10, 10)};
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;
            var headPosition = player.Snake.GetHeadLocation();
            Debug.WriteLine(headPosition);
            Console.SetCursorPosition(headPosition.X, headPosition.Y);
            Console.Write("@");

            var applePosition = board.PlaceApple();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(applePosition.X, applePosition.Y);
            Console.Write("$");

            Debug.WriteLine("Placed apple");
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
                var tail = player.Snake.First();
                var head = player.Snake.Last();
                var newHead = new Vector(head.Position.X, head.Position.Y);
                switch (newDir)
                {
                    case 0:
                        newHead = newHead - new Vector(0, 1);
                        break;
                    case 1:
                        newHead = newHead + new Vector(1, 0);
                        break;
                    case 2:
                        newHead = newHead + new Vector(0, 1);
                        break;
                    default:
                        newHead = newHead - new Vector(1, 0);
                        break;
                }

                if (newHead.X < 0 || newHead.X >= boardW)
                {
                    Debug.WriteLine(boardW);
                    Debug.WriteLine(newHead.X);
                    Debug.WriteLine("Out of horizontal bounds");
                    gg = true;
                }
                else if (newHead.Y < 0 || newHead.Y >= boardH)
                {
                    Debug.WriteLine(boardH);
                    Debug.WriteLine(newHead.Y);
                    Debug.WriteLine("Out of vertical bounds");
                    gg = true;
                }

                foreach (var apple in board.Apples)
                {
                    if (newHead.X == apple.Position.X && newHead.Y == apple.Position.Y)
                    {
                        board.RemoveApple(apple);
                        if (player.Snake.Count + 1 >= boardW * boardH)
                        {
                            // No more room to place apples -- game over.
                            gg = true;
                        }
                        else
                        {
                            board.PlaceApple();
                        }
                    }
                }

                if (!inUse)
                {
                    player.Snake.RemoveAt(0);
                    if (player.Snake.Any(x => x.Position.X == newHead.X && x.Position.Y == newHead.Y))
                    {
                        gg = true;
                    }
                }

                if (gg)
                {
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(head.Position.X, head.Position.Y);
                Console.Write("O");
                if (!inUse)
                {
                    Console.SetCursorPosition(tail.Position.X, tail.Position.Y);
                    Console.Write(" ");
                }
                
                foreach (var apple in board.Apples)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(apple.Position.X, apple.Position.Y);
                    Console.Write("$");
                    inUse = false;
                }
                

                player.Snake.Add(new SnakeComponent(newHead, SnakePart.Head));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(newHead.X, newHead.Y);
                Console.Write("@");
                last = newDir;
            }
            Debug.WriteLine(gg);
        }
    }
}