namespace SnakeMess
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class Game
    {
        public Game(int numPlayers, Vector boardSize)
        {
            var players = new List<Player>(numPlayers);
            for (var i = 1; i <= numPlayers; i++)
            {
                players.Add(new Player(i));
            }

            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;
            Players = players;
            Board = new Board(boardSize, players);
        }

        private List<Player> Players { get; set; }

        private Board Board { get; set; }

        public static void Main(string[] args)
        {
            Console.WindowWidth = 45;
            Console.WindowHeight = 30;
            var dimension = new Vector(Console.WindowWidth, Console.WindowHeight);
            var game = new Game(1, dimension);
            game.Play();
        }

        protected virtual void Play()
        {
            Console.Clear();
            var gameOver = false;
            var pause = false;
            var timer = new Stopwatch();
            Board.PlaceApple();
            timer.Start();
            while (!gameOver)
            {
                CheckInput(ref gameOver, ref pause);

                if (pause || timer.ElapsedMilliseconds < 100)
                {
                    continue;
                }
                
                timer.Restart();

                MoveSnakesIfAlive();
                Board.ResolvePlayerStatuses();
                gameOver = Board.ResolveBoardStatus();
            }
        }

        protected virtual void MoveSnakesIfAlive()
        {
            foreach (var player in Board.Players.Where(player => !player.IsDead))
            {
                if (player.Snake.Move(Board.Apples))
                {
                    Board.PlaceApple();
                }
                
                player.IsDead = Board.PositionOutOfBounds(player.Snake.GetHeadLocation());
            }
        }

        protected virtual void CheckInput(ref bool gameOver, ref bool pause)
        {
            // Escape if there's no key available.
            if (!Console.KeyAvailable)
            {
                return;
            }

            var cki = Console.ReadKey(true);
            switch (cki.Key)
            {
                case ConsoleKey.Escape:
                    gameOver = true;
                    break;
                case ConsoleKey.Spacebar:
                    pause = !pause;
                    break;
                default:
                    foreach (var player in Board.Players)
                    {
                        player.KeyPushedCheck(cki.Key);
                    }

                    break;
            }
        }
    }
}
