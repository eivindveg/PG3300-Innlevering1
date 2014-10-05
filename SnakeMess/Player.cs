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

            switch (id)
            {
                case 1:
                    KeyMap = new KeyMapping(ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow);
                    break;
                case 2:
                    KeyMap = new KeyMapping(ConsoleKey.W, ConsoleKey.S, ConsoleKey.A, ConsoleKey.D);
                    break;
                case 3:
                    KeyMap = new KeyMapping(ConsoleKey.I, ConsoleKey.K, ConsoleKey.J, ConsoleKey.L);
                    break;
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
                Debug.Write("Up");
            }
            else if (key == KeyMap.Down && Snake.LastDirection != Direction.Up)
            {
                Snake.Direction = Direction.Down;
                Debug.Write("Down");
            }
            else if (key == KeyMap.Left && Snake.LastDirection != Direction.Right)
            {
                Snake.Direction = Direction.Left;
                Debug.Write("Left");
            }
            else if (key == KeyMap.Right && Snake.LastDirection != Direction.Left)
            {
                Snake.Direction = Direction.Right;
                Debug.Write("Right");
            }
        }
    }
}