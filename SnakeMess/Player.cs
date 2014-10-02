namespace SnakeMess
{
    using System;
    using System.Diagnostics;

    public class Player
    {
        private readonly KeyMapping keyMap;

        public Player(int id)
        {
            Id = id;

            switch (id)
            {
                case 1:
                    Color = ConsoleColor.Green;
                    keyMap = new KeyMapping(ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow);
                    break;
                case 2:
                    Color = ConsoleColor.Blue;
                    keyMap = new KeyMapping(ConsoleKey.W, ConsoleKey.S, ConsoleKey.A, ConsoleKey.D);
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
            if (keyPushed.Equals(this.keyMap.Up))
            {
                Debug.Write("Up");
            }
            else if (keyPushed.Equals(this.keyMap.Down))
            {
                Debug.Write("Down");
            }
            else if (keyPushed.Equals(this.keyMap.Left))
            {
                Debug.Write("Left");
            }
            else if (keyPushed.Equals(this.keyMap.Left))
            {
                Debug.Write("Right");
            }
        }
    }
}