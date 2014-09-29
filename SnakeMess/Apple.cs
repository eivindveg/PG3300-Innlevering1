using System.Dynamic;

namespace SnakeMess
{
    public class Apple : Component
    {
        protected Apple(EdibleType type, Vector position) : base(position)
        {
            Type = type;
        }

        public EdibleType Type { get; private set; }
    }
}
