using System;
using System.Diagnostics;

namespace SportsWebPt.Common.Logging
{
    public class MethodStopwatch
    {

        public static void Clock(Action actionToClock, ILog logger)
        {
            var stopWatch = new Stopwatch();
            
            stopWatch.Start();

            logger.Info(String.Format("Starting execution of {0}",actionToClock.Method.Name));

            actionToClock.Invoke();

            logger.Info(String.Format("Execution of {0} completed with duration of {1}", actionToClock.Method.Name, stopWatch.Elapsed));

        }

        public static TResult Clock<TResult,T1>(Func<T1, TResult> funcToClock, T1 t1, ILog logger)
        {
            var stopWatch = new Stopwatch();

            stopWatch.Start();

            logger.Info(String.Format("Starting execution of {0}", funcToClock.Method.Name));

            var results = funcToClock.Invoke(t1);

            logger.Info(String.Format("Execution of {0} completed with duration of {1}", funcToClock.Method.Name, stopWatch.Elapsed));

            return results;

        }

        public static TResult Clock<TResult, T1, T2>(Func<T1,T2, TResult> funcToClock, T1 t1,T2 t2, ILog logger)
        {
            var stopWatch = new Stopwatch();

            stopWatch.Start();

            logger.Info(String.Format("Starting execution of {0}", funcToClock.Method.Name));

            var results = funcToClock.Invoke(t1, t2);

            logger.Info(String.Format("Execution of {0} completed with duration of {1}", funcToClock.Method.Name, stopWatch.Elapsed));

            return results;

        }
        

    }
}
