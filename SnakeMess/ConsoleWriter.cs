using System;

namespace SnakeMess
{
    public class ConsoleWriter
    {
        private ConsoleWriter()
        {
        }

        public static void BlankLocation(Vector position)
        {
            Console.SetCursorPosition(position.X, position.Y);
            Console.Write(" ");
        }
    }
}
