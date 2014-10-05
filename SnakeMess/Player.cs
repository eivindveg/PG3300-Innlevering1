namespace SnakeMess
{
    using System;
    using System.Diagnostics;

    public class Player
    {
        public Player(int id)
        {
            Id = id;
            IsDead = false;

            KeyMap = GetMappingFor(id);
        }

        public static KeyMapping GetMappingFor(int id)
        {
            switch (id)
            {
                case 1:
                    return new KeyMapping(ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow);
                case 2:
                    return new KeyMapping(ConsoleKey.W, ConsoleKey.S, ConsoleKey.A, ConsoleKey.D);
                case 3:
                    return new KeyMapping(ConsoleKey.I, ConsoleKey.K, ConsoleKey.J, ConsoleKey.L);
                default:
                    return null;
            }
        }

        public Snake Snake { get; set; }

        public bool IsDead { get; set; }

        public int Id { get; set; }

        private int Score { get; set; }

        private KeyMapping KeyMap { get; set; }

        public void KeyPushedCheck(ConsoleKey key)
        {
            if (key == KeyMap.Up && Snake.LastDirection != Direction.Down)
            {
                Snake.Direction = Direction.Up;
            }
            else if (key == KeyMap.Down && Snake.LastDirection != Direction.Up)
            {
                Snake.Direction = Direction.Down;
            }
            else if (key == KeyMap.Left && Snake.LastDirection != Direction.Right)
            {
                Snake.Direction = Direction.Left;
            }
            else if (key == KeyMap.Right && Snake.LastDirection != Direction.Left)
            {
                Snake.Direction = Direction.Right;
            }
        }
    }
}