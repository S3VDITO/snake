using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameCore
{
    internal static class GameTiming
    {
        private static Stopwatch Stopwatch = new Stopwatch();

        static GameTiming()
        {
            Stopwatch.Start();
        }

        public static List<ScriptTimer> Timers = new List<ScriptTimer>();
        public static long _currentTime;

        public static void ProcessTimers()
        {
            _currentTime = Stopwatch.ElapsedMilliseconds;

            var timers = Timers.ToArray();

            foreach (var timer in timers)
            {
                if (_currentTime >= timer.triggerTime)
                {
                    try
                    {
                        var parameters = timer.func.Method.GetParameters();
                        var returnType = timer.func.Method.ReturnType;
                        var returnValue = timer.func.DynamicInvoke();

                        if (returnType == typeof(bool) && (bool)returnValue == false)
                        {
                            Timers.Remove(timer);
                            continue;
                        }

                        if (timer.interval != -1)
                            timer.triggerTime = _currentTime + timer.interval;
                        else
                            Timers.Remove(timer);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Timers.Remove(timer);
                    }
                }
            }
        }

        public class ScriptTimer
        {
            public Delegate func;
            public long triggerTime;
            public int interval;
        }
    }
}
