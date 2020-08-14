using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore
{
    internal class Vector2D
    {
        public int X;
        public int Y;

        public static Vector2D Zero = new Vector2D(0, 0);

        public Vector2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int DistanceTo(Vector2D other) => (other - this).Length();

        public virtual int Length() => (int)Math.Sqrt(X * X + Y * Y);

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

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
                return false;

            Vector2D p = (Vector2D)obj;
            return (X == p.X) && (Y == p.Y);
        }
        public override string ToString() => $"({X}, {Y})";
        public override int GetHashCode() => base.GetHashCode();
    }

    internal class Vector3D : Vector2D
    {
        public int Z;

        public new Vector3D Zero = new Vector3D(0, 0, 0);

        public Vector3D(int x, int y, int z) : base(x, y)
        {
            Z = z;
        }

        public static Vector3D operator -(Vector3D left, Vector3D right) =>
            new Vector3D(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        public static Vector3D operator -(Vector3D left, int right) =>
            new Vector3D(left.X - right, left.Y - right, left.Z - right);

        public static Vector3D operator +(Vector3D left, Vector3D right) =>
            new Vector3D(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        public static Vector3D operator +(Vector3D left, int right) =>
            new Vector3D(left.X + right, left.Y + right, left.Z + right);

        public static Vector3D operator *(Vector3D left, Vector3D right) =>
            new Vector3D(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

        public static Vector3D operator *(Vector3D left, int right) =>
            new Vector3D(left.X * right, left.Y * right, left.Z * right);

        public static Vector3D operator /(Vector3D left, Vector3D right) =>
            new Vector3D(left.X / right.X, left.Y / right.Y, left.Z / right.Z);

        public static Vector3D operator /(Vector3D left, int right) =>
            new Vector3D(left.X / right, left.Y / right, left.Z / right);

        public int DistanceTo(Vector3D other) => (other - this).Length();

        public override int Length() => (int)Math.Sqrt(X * X + Y * Y + Z * Z);

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
                return false;

            Vector3D p = (Vector3D)obj;
            return (X == p.X) && (Y == p.Y) && (Z == p.Z);
        }
        public override string ToString() => $"({X}, {Y}, {Z})";

        public override int GetHashCode() => base.GetHashCode();
    }
}
