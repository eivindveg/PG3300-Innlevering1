namespace SnakeMess
{
    public class SnakeComponent : Component
    {
        public SnakeComponent(Vector position, SnakePart type) : base(position)
        {
            Type = type;
        }

        public SnakePart Type { get; private set; }
    }
}