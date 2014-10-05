using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

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
            try
            {
                Console.SetCursorPosition(position.X, position.Y);
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }

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
            Console.Clear();
            var optionsList = new List<Option>(options);
            var lines = introduction.Length + options.Length + 2;

            var writeWidth = introduction.Select(line => line.Length).Concat(new[] { 0 }).Max();
            writeWidth = options.Select(option => option.Message.Length + 4).Concat(new[] { writeWidth }).Max();
            writeWidth += 2;

            var offset = GetOffset(writeWidth, lines);

            WriteBox(writeWidth, lines, offset);
            Console.ForegroundColor = ConsoleColor.Magenta;
            for (var i = 0; i < introduction.Length; i++)
            {
                    Console.SetCursorPosition(offset.X + 1, offset.Y + i + 1);
                    Console.Write(introduction[i]);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            for (var i = 0; i < options.Length; i++)
            {
                Console.SetCursorPosition(offset.X + 1, offset.Y + i + introduction.Length + 2);
                Console.Write(options[i].Message);
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

        public static void PrintControls()
        {
            const string message = "Controls for player ";
            const string keyMessage = "Press any key to continue";
            var offset = GetOffset(keyMessage.Length + 2, 8);
            for (var i = 1; i <= 3; i++)
            {
                Console.Clear();
                var mapping = Player.GetMappingFor(i);
                WriteBox(keyMessage.Length + 2, 8, offset);
                Console.ForegroundColor = ConsoleColor.Magenta;
                var tempMessage = message + i;

                Console.SetCursorPosition(offset.X + 1, offset.Y + 1);
                Console.Write(tempMessage);
                Console.SetCursorPosition(offset.X + 1, offset.Y + 2);
                Console.Write("Up: " + mapping.Up);
                Console.SetCursorPosition(offset.X + 1, offset.Y + 3);
                Console.Write("Down: " + mapping.Down);
                Console.SetCursorPosition(offset.X + 1, offset.Y + 4);
                Console.Write("Left: " + mapping.Left);
                Console.SetCursorPosition(offset.X + 1, offset.Y + 5);
                Console.Write("Right: " + mapping.Right);
                Console.SetCursorPosition(offset.X + 1, offset.Y + 7);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(keyMessage);
                Console.ReadKey();
            }
        }

        public static void PrintScores(List<Snake> snakes)
        {
            const string scoreMessage = "Player ";
            const string keyMessage = "Press any key to continue";
            var offset = GetOffset(keyMessage.Length + 2, snakes.Count + 10);
            WriteBox(keyMessage.Length + 2, snakes.Count + 4, offset);

            foreach (var snake in snakes)
            {
                var index = snakes.IndexOf(snake);
                var playerNumber = index + 1;
                var tempMessage = scoreMessage + playerNumber + ": ";
                Console.SetCursorPosition(offset.X + 1, offset.Y + index + 1);
                Console.ForegroundColor = snake.Color;
                Console.Write(tempMessage + snake.Count());
            }

            Console.SetCursorPosition(offset.X + 1, offset.Y + snakes.Count + 3);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(keyMessage);
            Console.ReadKey();
        }

        private static void WriteBox(int writeWidth, int lines, Vector offset)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
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
        }

        private static Vector GetOffset(int writeWidth, int lines)
        {
            var offset = new Vector((Console.WindowWidth / 2) - (writeWidth / 2), (Console.WindowHeight / 2) - (lines / 2));
            return offset;
        }
    }
}
