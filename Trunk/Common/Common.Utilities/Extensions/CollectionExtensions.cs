﻿using System.Collections.Generic;

namespace SportsWebPt.Common.Utilities
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> destination,
                                        IEnumerable<T> source)
        {
            foreach (var item in source)
            {
                destination.Add(item);
            }
        }
    }
}
