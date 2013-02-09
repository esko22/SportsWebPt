using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;


namespace SportsWebPt.Common.Utilities
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Walks tree by using a comparison function and a get parameter function to do the walking.  This should be used for
        /// typical parent child relationship or Manager/employee where you want to find all managers employee's by following a user.managerid field.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TParam">The type of the param.</typeparam>
        /// <param name="items">The items.</param>
        /// <param name="getParam">The function to get the parameter.</param>
        /// <param name="compare">The comparison function.</param>
        /// <param name="param">The parameter.</param>
        /// <example>
        /// employees.WalkTree(x => x.UserId, (x, param) => x.ManagerId == param, 1)
        /// </example>
        /// <returns></returns>
        public static IEnumerable<T> WalkTree<T, TParam>(this IEnumerable<T> items, Func<T,TParam> getParam, Func<T, TParam, bool> compare, TParam param){
           var results = items.Where(x => compare.Invoke(x, param) ).ToList();
           var count = results.Count;
            if(count > 0){
                //must loop this way because we are modifying the collection    
                for (int i = 0; i < count; ++i){
                    var x = results[i];
                    results.AddRange(WalkTree(items, getParam, compare, getParam.Invoke(x)));
                }
            }
            return results;
        }

        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first,
        IEnumerable<TSource> second, Func<TSource, TSource, bool> comparer)
        {
            return first.Except(second, new LambdaComparer<TSource>(comparer));
        }

        public static IEnumerable<TElement> Distinct<TElement>(this IEnumerable<TElement> source, Func<TElement, TElement, bool> comparer)
        {
            return source.Distinct(new LambdaComparer<TElement>(comparer));
        }

        [DebuggerStepThrough]
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return (source == null) || (!source.Any());
        }

        [DebuggerStepThrough]
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return !IsNullOrEmpty(source);
        }


        /// <summary>
        /// Converts list to a delimiter separated string.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
        public static string ToString(this IEnumerable<string> list, string delimiter)
        {
            if (list != null && list.Count() > 0)
            {
                return string.Join(delimiter, list.ToArray());
            }

            return string.Empty;
        }

        /// <summary>
        /// Converts list to a delimiter separated string.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
        public static string ToString(this IEnumerable<string> list, char delimiter)
        {
            return list.ToString(delimiter.ToString());
        }

        /// <summary>
        /// Allows for inline looping of an enumeration
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
            return enumerable;
        }

        public static IEnumerable<T> AsNullIfEmpty<T>(this IEnumerable<T> items)
        {
            if (items == null || items.Any())
            {
                return null;
            }
            return items;
        }
     

        public static void EachParallel<T>(this IEnumerable<T> list, Action<T> action)
        {
            // enumerate the list so it can't change during execution
            if(list == null){
                return;
            }

            list = list.ToArray();
            var count = list.Count();

            switch (count){
                case 0:
                    return;
                case 1:
                    action(list.First());
                    break;
                default:{
                    // Launch each method in it's own thread
                    const int maxHandles = 64;
                    for (var offset = 0; offset <= list.Count() / maxHandles; offset++)
                    {
                        // break up the list into 64-item chunks because of a limitiation 
                        // in WaitHandle
                        var chunk = list.Skip(offset * maxHandles).Take(maxHandles);

                        // Initialize the reset events to keep track of completed threads
                        var resetEvents = new ManualResetEvent[chunk.Count()];

                        // spawn a thread for each item in the chunk
                        int i = 0;
                        foreach (var item in chunk)
                        {
                            resetEvents[i] = new ManualResetEvent(false);
                            ThreadPool.QueueUserWorkItem(data =>
                                                         {
                                                             var methodIndex = (int)((object[])data)[0];

                                                             // Execute the method and pass in the enumerated item
                                                             action((T)((object[])data)[1]);

                                                             // Tell the calling thread that we're done
                                                             resetEvents[methodIndex].Set();
                                                         }, new object[] { i, item });
                            i++;
                        }

                        // Wait for all threads to execute
                        WaitHandle.WaitAll(resetEvents);
                    }
                }
                    break;
            }
        }
    }
}