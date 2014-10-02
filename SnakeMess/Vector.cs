namespace SnakeMess
{
    public struct Vector
    {
        public int X;

        public int Y;

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector DirectlyBehind(Direction fromDirection, Vector fromVector)
        {
            switch (fromDirection)
            {
                case Direction.Down:
                    return new Vector(fromVector.X, fromVector.Y - 1);
                case Direction.Up:
                    return new Vector(fromVector.X, fromVector.Y + 1);
                case Direction.Left:
                    return new Vector(fromVector.X + 1, fromVector.Y);
                case Direction.Right:
                    return new Vector(fromVector.X - 1, fromVector.Y);
            }

            return fromVector;
        }
    }
}