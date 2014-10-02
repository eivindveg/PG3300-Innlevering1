namespace SnakeMess
{
    public abstract class Component
    {

        protected Component(Vector position)
        {
            Position = position;
        }

        public Vector Position { get; set; }
    }
}
