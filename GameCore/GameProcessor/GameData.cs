using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore
{
    public static class GameData
    {
        public static List<Barrier> Barriers = new List<Barrier>();
        public static List<Snake> Snakes = new List<Snake>();
        public static List<Point> Points = new List<Point>();

        public static readonly int Width = 80;
        public static readonly int Height = 26;
    }
}
