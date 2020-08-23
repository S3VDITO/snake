using System;
using System.Collections;

using static GameCore.GameTiming;

namespace GameCore
{
    internal static class GameCoroutine
    {
        /// <summary>
        /// Create repeat action after time interval
        /// </summary>
        /// <param name="interval">Time interval in milliseconds</param>
        /// <param name="function">Action, if action call exception or return false then it stop</param>
        public static void OnInterval(int interval, Func<bool> function)
        {
            
            Timers.Add(new ScriptTimer()
            {
                func = function,
                triggerTime = 0,
                interval = interval
            });
        }

        /// <summary>
        /// Create delayed action
        /// </summary>
        /// <param name="delay">Time delay in milliseconds</param>
        /// <param name="function">Action</param>
        public static void AfterDelay(int delay, Action function)
        {
            Timers.Add(new ScriptTimer()
            {
                func = function,
                triggerTime = _currentTime + delay,
                interval = -1
            });
        }

        /// <summary>
        /// Create co-routine(pseudo thread)
        /// </summary>
        /// <param name="routine">IEnumerator method</param>
        // IEnumerator Routine() 
        // { 
        //     yield return Wait(5); 
        //     Console.WriteLine("Hello!!!"); 
        // }
        //
        // ..... MAGIC CODE .....
        //
        // Thread(routine());
        public static void Thread(IEnumerator routine) => OnInterval(50, () =>
        {
            if (!routine.Update())
                return false;

            return true;
        });

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
