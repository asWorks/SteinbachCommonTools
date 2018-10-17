using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CommonTools
{
    public class GlobalStopwatch
    {
        private static Stopwatch w;

        public static void StartWatch()
        {
            if (w == null)
            {
                w = new System.Diagnostics.Stopwatch();
            }

            w.Stop();
            w.Start();

        }

        public static void ReStartWatch()
        {
            if (w == null)
            {
                w = new System.Diagnostics.Stopwatch();
            }

            w.Restart();

        }

        public static void StopWatch()
        {
            if (w != null)
            {
                w.Stop();
            }
        }

        public static void Reset()
        {
            if (w != null)
            {
                w.Reset();
            }
        }

        public static long  GetEllapsedMilliseconds()
        {
            
            if (w != null)
            {
                return w.ElapsedMilliseconds;
            }
      
            return 0;
 
        }

        public static TimeSpan GetEllapsedTimespan()
        {

            if (w != null)
            {
                return w.Elapsed;
            }

            return new TimeSpan(0);

        } 


    }
}
