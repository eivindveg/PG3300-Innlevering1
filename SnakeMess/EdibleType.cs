using System;

namespace SnakeMess
{
    public enum EdibleType
    {
        RedApple,
        GoldenApple
    }

    public class EdibleTypeColor
    {
        public static ConsoleColor GetColorForType(EdibleType type)
        {
            switch (type)
            {
                case EdibleType.GoldenApple:
                    return ConsoleColor.DarkYellow;
                default:
                    return ConsoleColor.Red;
            }
        }
    }
}
