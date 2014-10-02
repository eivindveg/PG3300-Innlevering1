using System;

namespace SnakeMess
{
    public class Player
    {
        public Player(int id)
        {
            Id = id;
            if (id == 1)
            {
                Color = ConsoleColor.Green;
            }
            else if (id == 2)
            {
                Color = ConsoleColor.Blue;
            }

        }

        public Snake Snake { get; set; }

        public int Id { get; set; }

        private int Score { get; set; }

        private KeyMapping KeyMap { get; set; }

        public ConsoleColor Color { get; set; }
    }
}