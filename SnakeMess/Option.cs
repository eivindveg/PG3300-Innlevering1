using System;

namespace SnakeMess
{
    public class Option
    {
        public Option(string message, ConsoleKey key, int value)
        {
            Message = message;
            Key = key;
            Value = value;
        }

        public ConsoleKey Key { get; private set; }

        public string Message { get; private set; }

        public int Value { get; private set; }
    }
}
