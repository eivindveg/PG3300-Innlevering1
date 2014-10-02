namespace SnakeMess
{
    public class Apple : Component
    {
        public Apple(EdibleType type, Vector position) : base(position)
        {
            Type = type;
        }

        public EdibleType Type { get; private set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Apple))
            {
                return false;
            }
            var other = (Apple) obj;
            return Type == other.Type && Position == other.Position;
        }
    }
}
