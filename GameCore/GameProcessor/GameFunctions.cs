using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCore
{
    internal static class GameFunctions
    {
        private static Random random = new Random();

        public static int RandomInt(int max) => random.Next(0, max);

        public static int RandomIntRange(int min, int max) => random.Next(min, max);

        public static Vector2D RandomXY() => new Vector2D(RandomInt(GameInitializer.Width), RandomInt(GameInitializer.Height));
    }
}
