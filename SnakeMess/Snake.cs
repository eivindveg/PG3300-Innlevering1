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
                Add(new SnakeComponent(position, SnakePart.Tail));
            }
        }


        public Player Player { get; set; }

        public Direction Direction { get; set; }

        public Direction LastDirection { get; private set; }

        public virtual bool Move(List<Apple> apples)
        {
            // Create temp for head and last tail
            // Move head forward, move last tail to old head
            var oldLastTail = this.Last();
            var head = this.First();
            var newLastTailPosition = head.Position;
            
            head.Position = Vector.DirectlyAhead(Direction, head.Position);
            var appleToEat = apples.FirstOrDefault(apple => apple.IsInPosition(head.Position));
            if (appleToEat != null)
            {
                Insert(1, new SnakeComponent(newLastTailPosition, SnakePart.Tail));
                apples.Remove(appleToEat);
            }
            else
            {
                Remove(oldLastTail);
                ConsoleWriter.BlankLocation(oldLastTail.Position);
                oldLastTail.Position = newLastTailPosition;
                Insert(1, oldLastTail);
            }
            
            LastDirection = Direction;
            return appleToEat != null;
        }

        public virtual void Grow()
        {
            Add(new SnakeComponent(this.Last().Position, SnakePart.Tail));
        }

        public virtual Vector GetHeadLocation()
        {
            return this.First().Position;
        }

        public virtual List<Vector> GetTailPositions()
        {
            return (from component in this where component.Type == SnakePart.Tail select component.Position).ToList();
        }

        public bool IsInPosition(Vector position)
        {
            return this.Any(component => component.Position.Equals(position));
        }
    }
}