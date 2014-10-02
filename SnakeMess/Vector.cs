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

        public static Vector operator +(Vector vector1, Vector vector2)
        {
            return new Vector(vector1.X + vector2.X, vector1.Y + vector2.Y);
        }

        public static Vector operator -(Vector vector1, Vector vector2)
        {
            return new Vector(vector2.X - vector1.X, vector2.Y - vector1.Y);
        }

        public static bool operator ==(Vector vector1, Vector vector2)
        {
            return vector1.X == vector2.X && vector1.Y == vector2.Y;
        }

        public static bool operator !=(Vector vector1, Vector vector2)
        {
            return !(vector1 == vector2);
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