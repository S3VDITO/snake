using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace GameCore
{
    public static class GameManager
    {
        public static readonly int Width = 80;
        public static readonly int Height = 26;

        public static void Initialize()
        {
            InitializeWindow();
            CreateBarriers();
            Eat.CreateEat();

            new Snake(new Vector2D(20, 10));
            while (true)
            {
                Thread.Sleep(25);

                GameTiming.ProcessTimers();

                if (!Console.KeyAvailable)
                    continue;

                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    break;
            }
        }

        public static void CreateBarriers()
        {
            if (GameData.Barriers.Count != 0)
                GameData.Barriers.ForEach(barrier => barrier.Point.Draw());

            else
            {
                new Barrier(new Vector2D(0, 0));
                new Barrier(new Vector2D(0, 1));
                new Barrier(new Vector2D(0, 2));
                new Barrier(new Vector2D(0, 3));
                new Barrier(new Vector2D(0, 4));
                new Barrier(new Vector2D(0, 5));
            }
        }

        private static void InitializeWindow()
        {
            Console.SetWindowSize(Width + 1, Height + 1);
            Console.SetBufferSize(Width + 1, Height + 1);
            Console.CursorVisible = false;
        }
    }
}
