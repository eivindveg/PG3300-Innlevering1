namespace SnakeMess
{
    public struct Vector
    {
        public readonly int X;

        public readonly int Y;

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector operator +(Vector vector1, Vector vector2)
        {
            return new Vector(vector1.X + vector2.X, vector1.Y + vector2.Y);
        }

        public static Vector operator -(Vector vector2, Vector vector1)
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
                    return fromVector - new Vector(0, 1);
                case Direction.Up:
                    return fromVector + new Vector(0, 1);
                case Direction.Left:
                    return fromVector + new Vector(1, 0);
                case Direction.Right:
                    return fromVector - new Vector(1, 0);
            }

            return fromVector;
        }

        public static Vector DirectlyAhead(Direction fromDirection, Vector fromVector)
        {
            switch (fromDirection)
            {
                case Direction.Down:
                    return fromVector + new Vector(0, 1);
                case Direction.Up:
                    return fromVector - new Vector(0, 1);
                case Direction.Left:
                    return fromVector - new Vector(1, 0);
                case Direction.Right:
                    return fromVector + new Vector(1, 0);
            }

            return fromVector;
        }

        public bool Equals(Vector other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Vector && Equals((Vector)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public override string ToString()
        {
            return "[" + X + "," + Y + "]";
        }
    }
}