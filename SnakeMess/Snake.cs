namespace SnakeMess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Snake : ICollideable
    {
        public Snake()
        {
        }

        public List<SnakeComponent> Components { get; set; }

        public Player Player { get; set; }

        private Direction Direction { get; set; }

        public virtual void Move()
        {
            throw new NotImplementedException();
        }

        public virtual Vector GetHeadLocation()
        {
            throw new NotImplementedException();
        }

        public bool IsInPosition(Vector position)
        {
            return Components.Any(component => component.Position.Equals(position));
        }
    }
}