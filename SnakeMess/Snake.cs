using System.IO;

namespace SnakeMess
{
    using System.Collections.Generic;
    using System.Linq;

    public class Snake : List<SnakeComponent>, ICollideable
    {
        public const int StartLength = 4;

        public Snake(Direction direction, Vector position)
        {
            Direction = direction;
            this.Add(new SnakeComponent(position, SnakePart.Head));

            while (this.Count() < StartLength)
            {
                position = Vector.DirectlyBehind(direction, position);
                this.Add(new SnakeComponent(position, SnakePart.Tail));
            }
        }


        public Player Player { get; set; }

        public Direction Direction { get; set; }

        public Direction LastDirection { get; private set; }

        public virtual void Move()
        {
            // Create temp for head and last tail
            // Move head forward, move last tail to old head
            var oldLastTail = this.Last();
            this.Remove(oldLastTail);
            oldLastTail.Position = this.First().Position;
            this.Insert(1, oldLastTail);
            this.First().Position = Vector.DirectlyAhead(Direction, this.First().Position);
            LastDirection = Direction;

            /*
            this.Insert(1, oldLastTail);
             */

        }

        public virtual void Grow()
        {
            this.Add(new SnakeComponent(this.Last().Position, SnakePart.Tail));
        }

        public virtual Vector GetHeadLocation()
        {
            return this.First().Position;
        }

        public virtual List<Vector> GetTailPositions()
        {
            var returnList = new List<Vector>();
            var headLocation = GetHeadLocation();
            foreach (var component in this)
            {
                if (component.Position != headLocation)
                {
                    returnList.Add(component.Position);
                }  
            }
            return returnList;
        }

        public bool IsInPosition(Vector position)
        {
            return this.Any(component => component.Position.Equals(position));
        }
    }
}