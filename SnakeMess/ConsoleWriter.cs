using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SnakeMess
{
    using System;

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

        public static void WriteToPosition(ConsoleColor color, Vector position, char symbol)
        {
            Console.ForegroundColor = color;
            try
            {
                Console.SetCursorPosition(position.X, position.Y);
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
            Console.Write(symbol);
        }

        public static int WriteOptions(string[] introduction, Option[] options)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            var optionsList = new List<Option>(options);
            var width = Console.WindowWidth;
            var height = Console.WindowHeight;
            var lines = introduction.Length + options.Length + 2;

            var writeWidth = introduction.Select(line => line.Length).Concat(new[] { 0 }).Max();
            writeWidth = options.Select(option => option.Message.Length + 4).Concat(new[] { writeWidth }).Max();
            writeWidth += 2;

            var offset = new Vector((width / 2) - (writeWidth / 2), (height / 2) - (lines / 2));

            for (var x = 0; x <= writeWidth; x++)
            {
                for (var y = 0; y <= lines; y++)
                {
                    if ((x != 0 && x != writeWidth) && (y != 0 && y != lines))
                    {
                        continue;
                    }

                    Console.SetCursorPosition(offset.X + x, offset.Y + y);
                    Console.Write("#");
                }
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            for (var i = 0; i < introduction.Length; i++)
            {
                for (var j = 0; j < introduction[i].Length; j++)
                {
                    Console.SetCursorPosition(offset.X + j + 1, offset.Y + i + 1);
                    Console.Write(introduction[i].ElementAt(j));
                }
            }
            for (var i = 0; i < options.Length; i++)
            {
                for (var j = 0; j < options[i].Message.Length; j++)
                {
                    Console.SetCursorPosition(offset.X + j + 1, offset.Y + i + introduction.Length + 1);
                    Console.Write(options[i].Message.ElementAt(j));
                }
            }

            for (;;)
            {
                var cki = Console.ReadKey();

                foreach (var option in optionsList.Where(option => cki.Key == option.Key))
                {
                    return option.Value;
                }
            }
        }
    }
}
