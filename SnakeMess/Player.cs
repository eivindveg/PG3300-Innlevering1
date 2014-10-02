namespace SnakeMess
{
    using System;
    using System.Diagnostics;

    public class Player
    {
        public Player(int id)
        {
            Id = id;

            switch (id)
            {
                case 1:
                    Color = ConsoleColor.Green;
                    KeyMap = new KeyMapping(ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow);
                    break;
                case 2:
                    Color = ConsoleColor.Blue;
                    KeyMap = new KeyMapping(ConsoleKey.W, ConsoleKey.S, ConsoleKey.A, ConsoleKey.D);
                    break;
            }
        }

        public ConsoleColor Color { get; set; }


        public Snake Snake { get; set; }

        public int Id { get; set; }

        private int Score { get; set; }

        private KeyMapping KeyMap { get; set; }

        public void KeyPushedCheck()
        {
            if (!Console.KeyAvailable)
            {
                return;
            }

            var keyPushed = Console.ReadKey(true);
            if (keyPushed.Key == KeyMap.Up && Snake.Direction != Direction.Down)
            {
                Debug.Write("Up");
            }
            else if (keyPushed.Key == KeyMap.Down && Snake.Direction != Direction.Up)
            {
                Debug.Write("Down");
            }
            else if (keyPushed.Key == KeyMap.Left && Snake.Direction != Direction.Right)
            {
                Debug.Write("Left");
            }
            else if (keyPushed.Key == KeyMap.Right && Snake.Direction != Direction.Right)
            {
                Debug.Write("Right");
            }
        }
    }
}