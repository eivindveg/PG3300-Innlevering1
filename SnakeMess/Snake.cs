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
            // TODO CREATE SNAKE TRAIN
            this.Add(new SnakeComponent(position, SnakePart.Head));

            while (this.Count() < StartLength)
            {
                position = Vector.DirectlyBehind(direction, position);
                this.Add(new SnakeComponent(position, SnakePart.Tail));
            }
        }


        public Player Player { get; set; }

        public Direction Direction { get; set; }

        public virtual void Move()
        {
            // Create temp for head and last tail
            // Move head forward, move last tail to old head
            var oldLastTail = this.First();
            this.Last().Position = Vector.DirectlyAhead(Direction, this.First().Position);
            this.Remove(this.First());
            this.Insert(this.Count() - 2, oldLastTail);

        }

        public virtual void Grow()
        {
            this.Add(new SnakeComponent(this.First().Position, SnakePart.Tail));
        }

        public virtual Vector GetHeadLocation()
        {
            return this.First().Position;
        }

        public bool IsInPosition(Vector position)
        {
            return this.Any(component => component.Position.Equals(position));
        }
    }
}