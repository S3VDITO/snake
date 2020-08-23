using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GameCore
{
    public class GameInitializer
    {
        /// <summary>
        /// Initialize game functions
        /// </summary>
        /// <param name="Width">Window width</param>
        /// <param name="Height">Window height</param>
        public GameInitializer(int Width = 80, int Height = 26)
        {
            Console.SetWindowSize(Width + 1, Height + 1);
            Console.SetBufferSize(Width + 1, Height + 1);
            Console.CursorVisible = false;

            var qwe = new GameMenu(MenuType.InGame);

            InitializeTickRate();
        }

        /// <summary>
        /// Initialize tick rate(капитан очевидность...)
        /// </summary>
        private static void InitializeTickRate()
        {
            while (true)
            {
                Thread.Sleep(25);

                GameTiming.ProcessTimers();

                if (!Console.KeyAvailable)
                    continue;
            }
        }
    }
}
