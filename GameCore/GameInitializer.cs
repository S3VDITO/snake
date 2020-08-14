using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace GameCore
{
    public static class GameInitializer
    {
        internal static readonly int Width = 80;
        internal static readonly int Height = 26;

        internal static Dictionary<ObjectType, List<Point>> Map = new Dictionary<ObjectType, List<Point>>()
        {
            [ObjectType.Barrier] = new List<Point>()
            {
                new Point(new Vector2D(7,0), '#'),
                new Point(new Vector2D(7,1), '#'),
                new Point(new Vector2D(7,2), '#'),
                new Point(new Vector2D(7,3), '#'),
                new Point(new Vector2D(7,4), '#'),
                new Point(new Vector2D(7,5), '#'),
                new Point(new Vector2D(7,6), '#'),
                new Point(new Vector2D(7,7), '#'),
                new Point(new Vector2D(7,8), '#'),
                new Point(new Vector2D(7,9), '#')
            },
            [ObjectType.Eat] = new List<Point>()
            {
                new Point(GameFunctions.RandomXY(), '@')
            }
        };

        public static void Initialize()
        {
            Console.SetWindowSize(Width + 1, Height + 1);
            Console.SetBufferSize(Width + 1, Height + 1);
            Console.CursorVisible = false;

            new Snake(new Vector2D(Width / 2, Height / 2));

            while (true)
            {
                Thread.Sleep(25);

                GameTiming.ProcessTimers();

                if (!Console.KeyAvailable)
                    continue;

                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    break;
            }

            Environment.Exit(1337);
        }
    }
}
