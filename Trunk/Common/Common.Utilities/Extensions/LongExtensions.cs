using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public static class LongExtensions
    {
        [DebuggerStepThrough]
        public static double ConvertBytestToKilobytes(this long bytes){
            return (bytes/1024f);
        }

        [DebuggerStepThrough]
        public static double ConvertBytesToMegabytes(this long bytes){
            return (bytes/1024f)/1024f;
        }
    }
}
