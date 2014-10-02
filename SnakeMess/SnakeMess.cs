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

                gg = board.PositionOutOfBounds(newHead);

                foreach (var apple in board.Apples.Where(apple => newHead == apple.Position))
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
                        player.Snake.Grow();
                    }
                }

                if (!inUse)
                {
                    // player.Snake.RemoveAt(0);
                    if (player.Snake.Any(component => component.Position == newHead))
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

                foreach (var localPlayer in board.Players)
                {
                    player.Snake.Move();
                    Console.ForegroundColor = player.Color;
                    foreach (var component in localPlayer.Snake)
                    {
                        Debug.WriteLine(component.Position);
                        Console.SetCursorPosition(component.Position.X, component.Position.Y);
                        switch (component.Type)
                        {
                            case SnakePart.Head:
                                Console.Write("@");
                                break;
                            case SnakePart.Tail:
                                Console.Write("O");
                                break;
                        }
                    }
                }

                last = newDir;
            }
            Debug.WriteLine(gg);
        }
    }
}