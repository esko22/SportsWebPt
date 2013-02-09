using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public class DateRange
    {
        public DateTime Start { get; private set; }
        public DateTime Finish { get; private set; }
        public DateRange(DateTime start, DateTime finish){
            Start = start;
            Finish = finish;
        }
    }
}
