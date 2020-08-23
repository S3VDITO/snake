using System;
using System.Collections;
using System.Diagnostics;

namespace GameCore
{
    /// <summary>
    /// Type menus
    /// </summary>
    [Flags]
    internal enum MenuType
    {
        Pause,
        InGame
    }

    /// <summary>
    /// All objects can be spawned on map
    /// </summary>
    [Flags]
    internal enum ObjectType
    {
        Eat,
        Snake,
        Barrier
    }

    /// <summary>
    /// In-game functions/extensions
    /// </summary>
    internal static class GameFunctions
    {
        /// <summary>
        /// Private randomizer
        /// </summary>
        private static Random Random = new Random();

        /// <summary>
        /// Random int from 0 to max
        /// </summary>
        /// <param name="max">Max range</param>
        /// <returns>Random num</returns>
        public static int RandomInt(int max) => Random.Next(0, max);

        /// <summary>
        /// Random int from min to max
        /// </summary>
        /// <param name="min">Min range</param>
        /// <param name="max">Max range</param>
        /// <returns>Random num</returns>
        public static int RandomIntRange(int min, int max) => Random.Next(min, max);

        /// <summary>
        /// Get window width
        /// </summary>
        public static int WindowWidth { get => Console.WindowWidth - 1; }

        /// <summary>
        /// Get window height
        /// </summary>
        public static int WindowHeight { get => Console.WindowHeight - 1; }

        /// <summary>
        /// Get random X and Y vector
        /// </summary>
        public static Vector2D RandomXY { get => new Vector2D(RandomInt(WindowWidth), RandomInt(WindowHeight)); }

        #region Co-routine func

        /// <summary>
        /// Waiting key pressing and send key to Action
        /// </summary>
        /// <param name="action">Action after key pressed</param>
        /// <param name="keys">All keys waiters</param>
        /// <returns></returns>
        public static IEnumerator KeyPressWaiter(Action<ConsoleKey> action, params ConsoleKey[] keys)
        {
            if (!Console.KeyAvailable)
                yield break;

            ConsoleKey keyPressed = Console.ReadKey().Key;

            while (true)
            {
                foreach (var key in keys)
                {
                    if (key == keyPressed)
                    {
                        action?.Invoke(keyPressed);
                        yield break;
                    }
                }

                yield return 0;
            }
        }

        /// <summary>
        /// Waiting time in seconds
        /// </summary>
        /// <param name="time">Seconds for waiting (0.5f = 500ms / 1f=1000ms=1sec)</param>
        /// <returns></returns>
        public static IEnumerator Wait(float time)
        {
            Stopwatch watch = Stopwatch.StartNew();

            while (watch.Elapsed.TotalSeconds < time)
                yield return 0;
        }

        /// <summary>
        /// Waiting game tick
        /// </summary>
        /// <returns></returns>
        public static IEnumerator WaitForFrame()
        {
            Stopwatch watch = Stopwatch.StartNew();

            while (watch.Elapsed.TotalSeconds < 0.025)
                yield return 0;
        }
        #endregion
    }
}
