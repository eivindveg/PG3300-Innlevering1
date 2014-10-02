namespace SnakeMess
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    internal class SnakeMess
    {

        public static void Main(string[] arguments)
        {
            bool gg = false, pause = false, inUse = false;
            var dimension = new Vector(Console.WindowWidth, Console.LargestWindowHeight);
            Debug.WriteLine(dimension);
            var player = new Player(1);
            var board = new Board(dimension, player);
            int boardW = Console.WindowWidth, boardH = Console.WindowHeight;

            // var snake = new List<Coord> {new Coord(10, 10), new Coord(10, 10), new Coord(10, 10), new Coord(10, 10)};
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;

            // var headPosition = localPlayer.Snake.GetHeadLocation();
            // Debug.WriteLine(headPosition);
            /*
            Console.SetCursorPosition(headPosition.X, headPosition.Y);
            Console.Write("@");
             */

            var applePosition = board.PlaceApple();

            /*
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(applePosition.X, applePosition.Y);
            Console.Write("$");
             */

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
                            foreach (var localPlayer in board.Players)
                            {
                                localPlayer.KeyPushedCheck(cki.Key);
                            }
                            break;
                    }
                }

                if (pause || timer.ElapsedMilliseconds < 100)
                {
                    continue;
                }

                timer.Restart();

                // var head = localPlayer.Snake.First();
                /*
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
                 */
                foreach (var localPlayer in board.Players)
                {
                    localPlayer.Snake.Move();
                    gg = board.PositionOutOfBounds(localPlayer.Snake.GetHeadLocation());

                    var headLocation = localPlayer.Snake.GetHeadLocation();
                    foreach (var apple in board.Apples.Where(apple => headLocation == apple.Position))
                    {
                        board.RemoveApple(apple);
                        if (localPlayer.Snake.Count + 1 >= boardW * boardH)
                        {
                            // No more room to place apples -- game over.
                            gg = true;
                        }
                        else
                        {
                            board.PlaceApple();
                            localPlayer.Snake.Grow();
                        }

                        // player.Snake.RemoveAt(0);
                        if (localPlayer.Snake.Any(component => component.Position == headLocation))
                        {
                            gg = true;
                        }
                    }
                }

                if (!inUse)
                {

                }

                if (gg)
                {
                    continue;
                }

                /*
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(head.Position.X, head.Position.Y);
                Console.Write("O");
                 */
                
                foreach (var apple in board.Apples)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(apple.Position.X, apple.Position.Y);
                    Console.Write("$");
                }
                
                board.ReDraw();
            }
            Debug.WriteLine(gg);
        }
    }
}