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

                foreach (var otherPlayer in from otherPlayer in this.Players.Where(otherPlayer => otherPlayer.Id != currentPlayer.Id) from component in otherPlayer.Snake.Where(component => component.Position == currentPlayer.Snake.GetHeadLocation()) select otherPlayer)
                {
                    player.IsDead = true;
                }
            }
        }

        public void PlaceApple()
        {
            while (Apples.Count < 1)
            {
                var x = Random.Next(0, Dimension.X);
                var y = Random.Next(0, Dimension.Y);
                var apple = new Apple(EdibleType.RedApple, new Vector(x, y));
                var spot = Players.All(player => !player.Snake.IsInPosition(apple.Position));
                if (spot)
                {
                    Apples.Add(apple);
                }
            }
        }

        public bool PositionOutOfBounds(Vector position)
        {
            if (position.X < 0 || position.Y < 0)
            {
                return true;
            }

            return position.X >= Dimension.X || position.Y >= Dimension.Y;
        }

        public void RemoveApple(Apple apple)
        {
            if (apple == null)
            {
                throw new ArgumentNullException("apple");
            }

            Apples.Remove(apple);
        }

        public void ReDraw()
        {
            foreach (var apple in Apples)
            {
                ConsoleWriter.WriteToPosition(ConsoleColor.Red, apple.Position, '$');
            }

            foreach (var player in Players)
            {
                foreach (var component in player.Snake)
                {
                    try
                    {
                        switch (component.Type)
                        {
                            case SnakePart.Head:
                                ConsoleWriter.WriteToPosition(player.Color, component.Position, '@');
                                break;
                            case SnakePart.Tail:
                                ConsoleWriter.WriteToPosition(player.Color, component.Position, 'O');
                                break;
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                    }
                }
            }
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

                var snake = new Snake(direction, position);
                player.Snake = snake;
            }
        }
    }
}
