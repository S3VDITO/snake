using System.Runtime.InteropServices;

namespace GameCore
{

    [StructLayout(LayoutKind.Sequential)]
    internal struct Vector2D
    {
        public int X;
        public int Y;

        public static Vector2D Zero = new Vector2D(0, 0);

        public Vector2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector2D operator -(Vector2D left, Vector2D right) =>
            new Vector2D(left.X - right.X, left.Y - right.Y);

        public static Vector2D operator -(Vector2D left, int right) =>
            new Vector2D(left.X - right, left.Y - right);

        public static Vector2D operator +(Vector2D left, Vector2D right) =>
            new Vector2D(left.X + right.X, left.Y + right.Y);

        public static Vector2D operator +(Vector2D left, int right) =>
            new Vector2D(left.X + right, left.Y + right);

        public static Vector2D operator *(Vector2D left, Vector2D right) =>
            new Vector2D(left.X * right.X, left.Y * right.Y);

        public static Vector2D operator *(Vector2D left, int right) =>
            new Vector2D(left.X * right, left.Y * right);

        public static Vector2D operator /(Vector2D left, Vector2D right) =>
            new Vector2D(left.X / right.X, left.Y / right.Y);

        public static Vector2D operator /(Vector2D left, int right) =>
            new Vector2D(left.X / right, left.Y / right);

        public static bool operator ==(Vector2D left, Vector2D right) => left.Equals(right);
        public static bool operator !=(Vector2D left, Vector2D right) => !left.Equals(right);

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
                return false;

            Vector2D p = (Vector2D)obj;
            return (X == p.X) && (Y == p.Y);
        }
        public override string ToString() => $"({X}, {Y})";
        public override int GetHashCode() => X ^ Y;
    }
}
