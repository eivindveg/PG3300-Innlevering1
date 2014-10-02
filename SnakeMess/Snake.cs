namespace SnakeMess
{
    using System;
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
            

            while(this.Count() <= StartLength)
            {
                position = Vector.DirectlyBehind(direction, position);
                this.Add(new SnakeComponent(position, SnakePart.Tail));
            }
        }


        public Player Player { get; set; }

        public Direction Direction { get; set; }

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
            return this.Any(component => component.Position.Equals(position));
        }
    }
}