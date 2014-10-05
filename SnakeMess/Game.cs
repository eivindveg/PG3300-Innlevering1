using System.Threading;

namespace SnakeMess
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class Game
    {
        public static readonly string[] Introduction =
        {
            "Welcome to SnakeUnmess",
            "Select number of players:"
        };

        public Game(int numPlayers, Vector boardSize)
        {
            var players = new List<Player>(numPlayers);
            for (var i = 1; i <= numPlayers; i++)
            {
                players.Add(new Player(i));
            }

            Console.CursorVisible = false;
            Players = players;
            Board = new Board(boardSize, players);
        }

        private List<Player> Players { get; set; }

        private Board Board { get; set; }

        public static void Main(string[] args)
        {
            Console.CursorVisible = false; 
            Console.WindowHeight = 40;
            Console.WindowWidth = 60;
            var dimension = new Vector(Console.WindowWidth, Console.WindowHeight);
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
            var startOptions = new[]
            {
                new Option("[1] player", ConsoleKey.D1, 1),
                new Option("[2] players", ConsoleKey.D2, 2),
                new Option("[3] players", ConsoleKey.D3, 3), 
                new Option("[4] show controls", ConsoleKey.D4, 4), 
            };
            for (;;)
            {
                var players = ConsoleWriter.WriteOptions(Introduction, startOptions);
                if (players == 4)
                {
                    ConsoleWriter.PrintControls();
                    continue;
                }

                var game = new Game(players, dimension);
                game.Play();
                var snakes = game.Board.Players.Select(player => player.Snake).ToList();
                ConsoleWriter.PrintScores(snakes);
            }
        }

        protected virtual void Play()
        {
            Console.Clear();
            var gameOver = false;
            var pause = false;
            var timer = new Stopwatch();
            Board.PlaceApples();
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
                    Board.PlaceApples();
                }
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
