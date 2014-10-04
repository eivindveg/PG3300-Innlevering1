namespace SnakeMess
{
    public class Apple : Component, ICollideable
    {

        public Apple(EdibleType type, Vector position) : base(position)
        {
            Type = type;
        }

        public EdibleType Type { get; private set; }

        #pragma warning disable 659
        public override bool Equals(object obj)
        #pragma warning restore 659
        {
            if (!(obj is Apple))
            {
                return false;
            }
            var other = (Apple)obj;
            return Type == other.Type && Position == other.Position;
        }

        public bool IsInPosition(Vector location)
        {
            return Position == location;
        }

        protected bool Equals(Apple other)
        {
            return Type == other.Type && Position == other.Position;
        }
    }
}
