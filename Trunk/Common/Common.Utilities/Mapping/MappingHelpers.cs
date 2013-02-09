using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportsWebPt.Common.Utilities
{
    public static class MappingHelpers
    {
        //TODO: Need to get this to handle mapping methods that have params
        public static IEnumerable<TTypeOfReturn> ToConvertedList<TTypeOfReturn, TTypeOfSubject>(
           IEnumerable<TTypeOfSubject> subjects, Func<TTypeOfSubject, TTypeOfReturn> func)
        {
            return ToConvertedList(subjects, func, new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount });
        }

        public static IEnumerable<TTypeOfReturn> ToConvertedList<TTypeOfReturn, TTypeOfSubject>(
            IEnumerable<TTypeOfSubject> subjects, Func<TTypeOfSubject, TTypeOfReturn> func, ParallelOptions parallelOptions)
        {
            var returnList = new ConcurrentBag<TTypeOfReturn>();
    
            if(subjects != null)
                Parallel.ForEach(subjects, p => returnList.Add(func.Invoke(p)));

            return returnList;
        }
    }
}
