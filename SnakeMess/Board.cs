﻿namespace SnakeMess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Board
    {
        public Board(Vector dimension, List<Player> players)
        {
            Random = new Random();
            Dimension = dimension;
            Players = players;
            Apples = new List<Apple>();
            PositionSnakes();
        }

        public Board(Vector dimension, Player player)
        {
            Random = new Random();
            Dimension = dimension;
            Players = new List<Player>
                          {
                              player
                          };
            Apples = new List<Apple>();
            this.PositionSnakes();
        }

        public Vector Dimension { get; private set; }

        public List<Apple> Apples { get; set; }

        public List<Player> Players { get; set; }

        private Random Random { get; set; }

        public static ConsoleColor GetColorForPlayer(int id)
        {
            switch (id)
            {
                case 1:
                    return ConsoleColor.Green;
                case 2:
                    return ConsoleColor.Blue;
                case 3:
                    return ConsoleColor.Magenta;
                default:
                    return ConsoleColor.Yellow;
            }
        }

        public void ResolvePlayerStatuses()
        {
            foreach (var player in Players)
            {
                var currentPlayer = player;
                if (player.Snake.Where(component => component.Type != SnakePart.Head)
                    .Any(component => component.Position == currentPlayer.Snake.GetHeadLocation()))
                {
                    player.IsDead = true;
                }

                foreach (var otherPlayer in Players.Where(otherPlayer => otherPlayer.Id != currentPlayer.Id).Where(otherPlayer => otherPlayer.Snake.Any(component => component.Position == currentPlayer.Snake.GetHeadLocation())))
                {
                    player.IsDead = true;
                }
            }
        }

        public void PlaceApple()
        {
            do
            {
                var x = Random.Next(0, Dimension.X);
                var y = Random.Next(0, Dimension.Y);
                var apple = new Apple(EdibleType.RedApple, new Vector(x, y));
                var spot = Players.All(player => !player.Snake.IsInPosition(apple.Position));
                if (spot)
                {
                    Apples.Add(apple);
                    ConsoleWriter.WriteToPosition(ConsoleColor.Red, apple.Position, Apple.Symbol);
                }
            } 
            while (Apples.Count < (Players.Count / 2));
        }

        public bool PositionOutOfBounds(Vector position)
        {
            if (position.X < 0 || position.Y < 0)
            {
                return true;
            }

            return position.X >= Dimension.X || position.Y >= Dimension.Y;
        }

        public bool ResolveBoardStatus()
        {
            if (AllPlayersDead())
            {
                return true;
            }

            var componentCount = Players.Select(player => player.Snake).Select(snake => snake.Count()).Sum();
            return componentCount >= Dimension.GetArea();
        }

        public void RemoveApple(Apple apple)
        {
            if (apple == null)
            {
                throw new ArgumentNullException("apple");
            }

            Apples.Remove(apple);
        }

        public bool AllPlayersDead()
        {
            return Players.All(player => player.IsDead);
        }

        private void PositionSnakes()
        {
            foreach (var player in Players)
            {
                var position = new Vector();
                var direction = Direction.Right;
                switch (player.Id)
                {
                    case 1:
                        position = new Vector(Dimension.X / 4, Dimension.Y / 4);
                        direction = Direction.Right;
                        break;
                    case 2:
                        position = new Vector(Dimension.X - (Dimension.X / 4), Dimension.Y / 4);
                        direction = Direction.Left;
                        break;
                    case 3:
                        position = new Vector(Dimension.X / 4, Dimension.Y - (Dimension.Y / 4));
                        direction = Direction.Right;
                        break;
                    case 4:
                        position = new Vector(Dimension.X - (Dimension.X / 4), Dimension.Y - (Dimension.Y / 4));
                        direction = Direction.Up;
                        break;
                }

                var snake = new Snake(direction, position, GetColorForPlayer(player.Id));
                player.Snake = snake;
            }
        }
    }
}
