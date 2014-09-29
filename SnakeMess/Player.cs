namespace SnakeMess
{
    public class Player
    {
        public Player(int id)
        {
            Id = id;
        }

        public Snake Snake { get; set; }

        public int Id { get; set; }

        private int Score { get; set; }

        private KeyMapping KeyMap { get; set; }


    }
}