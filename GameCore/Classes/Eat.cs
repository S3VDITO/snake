using System;
using System.Collections.Generic;
using System.Text;

namespace GameCore
{
    public static class Eat
    {
        private static Random random = new Random();

        public static Point EatPoint { get; private set; }

        public static void CreateEat()
        {
            EatPoint = new Point(new Vector2D(random.Next(1, GameData.Width - 1), random.Next(1, GameData.Height - 1)), '@');
            EatPoint.Draw();
        }

        public static void MoveEat()
        {
            EatPoint.Origin = new Vector2D(random.Next(1, GameData.Width - 1), random.Next(1, GameData.Height - 1));
        }
    }
}
