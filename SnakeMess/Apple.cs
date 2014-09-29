namespace SnakeMess
{
    public class Apple : Component
    {
        public Apple(EdibleType type, Vector position) : base(position)
        {
            Type = type;
        }

        public EdibleType Type { get; private set; }
    }
}
