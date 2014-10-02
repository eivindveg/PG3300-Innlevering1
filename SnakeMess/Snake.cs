namespace SnakeMess
{
    using System;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class Snake : List<SnakeComponent>, ICollideable
    {
        public const int StartLength = 4;

        public Snake(Direction direction, Vector position)
        {
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
            var oldLastTail = this.Last();
            this.First().Position = Vector.DirectlyAhead(Direction, this.First().Position);
            this.Remove(this.Last());
            this.Insert(1, oldLastTail);

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