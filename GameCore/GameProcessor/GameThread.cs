using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Text;

namespace GameCore
{
    public static class GameThread
    {
        public static void OnInterval(int interval, Func<bool> function)
        {
            GameTiming.Timers.Add(new GameTiming.ScriptTimer()
            {
                func = function,
                triggerTime = 0,
                interval = interval
            });
        }
        public static void AfterDelay(int delay, Action function)
        {
            GameTiming.Timers.Add(new GameTiming.ScriptTimer()
            {
                func = function,
                triggerTime = GameTiming._currentTime + delay,
                interval = -1
            });
        }

        public static void Thread(IEnumerator routine) => OnInterval(25, () =>
        {
            if (!routine.Update())
                return false;

            return true;
        });
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
        public static IEnumerator Wait(float time)
        {
            Stopwatch watch = Stopwatch.StartNew();

            while (watch.Elapsed.TotalSeconds < time)
                yield return 0;
        }
        public static IEnumerator WaitForFrame()
        {
            Stopwatch watch = Stopwatch.StartNew();

            while (watch.Elapsed.TotalSeconds < 0.025)
                yield return 0;
        }

        private static bool Update(this IEnumerator routine)
        {
            if ((!(routine.Current is IEnumerator enumerator) || !MoveNext(enumerator)) && !routine.MoveNext())
                return false;

            return true;
        }

        private static bool MoveNext(IEnumerator routine) =>
            routine.Current is IEnumerator enumerator && MoveNext(enumerator) || routine.MoveNext();
    }
}
