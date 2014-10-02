using System.Collections.Generic;

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
            var dimension = new Vector(Console.WindowWidth, Console.WindowHeight);
            Debug.WriteLine(dimension);
            List<Player> players = new List<Player>
            {
                new Player(1),
                new Player(2),
                new Player(3)
            };

            //var player = new Player(1);

            var board = new Board(dimension, players);
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

            board.PlaceApple();

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

                foreach (var localPlayer in board.Players)
                {
                    if (!localPlayer.IsDead)
                    {
                        localPlayer.Snake.Move();
                        localPlayer.IsDead = board.PositionOutOfBounds(localPlayer.Snake.GetHeadLocation());
                    }

                    var headLocation = localPlayer.Snake.GetHeadLocation();
                    foreach (var apple in board.Apples.Where(apple => headLocation == apple.Position))
                    {
                        board.RemoveApple(apple);
                        if (localPlayer.Snake.Count + 1 >= boardW * boardH)
                        {
                            // No more room to place apples -- game over.
                            Debug.WriteLine("Good game!");
                            gg = true;
                        }
                        else
                        {
                            board.PlaceApple();
                            localPlayer.Snake.Grow();
                        }
                    }

                    if (localPlayer.Snake.Where(component => component.Type != SnakePart.Head).Any(component => component.Position == headLocation))
                    {
                        localPlayer.IsDead = true;
                        Debug.WriteLine("You fail!");
                    }
                    foreach (var otherPlayer in board.Players)
                    {
                        if (otherPlayer.Id == localPlayer.Id)
                        {
                            continue;
                        }
                        foreach (var component in otherPlayer.Snake)
                        {
                            if (component.Position == headLocation)
                            {
                                localPlayer.IsDead = true;
                            }
                        }
                    }
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

                if (board.AllPlayersDead())
                {
                    gg = true;
                    continue;
                }

                board.ReDraw();
            }
        }
    }
}