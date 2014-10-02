namespace SnakeMess
{
    using System;

    public class KeyMapping
    {
        public KeyMapping(ConsoleKey up, ConsoleKey down, ConsoleKey left, ConsoleKey right)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
        }

        public ConsoleKey Up { get; set; }

        public ConsoleKey Down { get; set; }

        public ConsoleKey Left { get; set; }

        public ConsoleKey Right { get; set; }
    }
}