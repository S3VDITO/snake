using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore
{
    public class Barrier
    {
        public Point Point { get; set; }

        public Vector2D Origin
        {
            get => Point.Origin;
            set => Point.Origin = value;
        }

        public Barrier(Vector2D origin)
        {
            Point point = new Point(origin, '#');
            point.Draw();
            Point = point;

            GameData.Barriers.Add(this);
        }
    }
}
