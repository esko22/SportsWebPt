using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SportsWebPt.Common.Utilities
{
    public interface ICacheProvider
    {
        /// <summary>
        /// Gets the <see cref="System.Object"/> with the specified key.
        /// </summary>
        /// <value></value>
        object this[string key] { get; }

        /// <summary>
        /// Gets the specified item key.
        /// </summary>
        /// <param name="itemKey">The item key.</param>
        /// <returns></returns>
        object Get(string itemKey);

        /// <summary>
        /// Clears the cache.
        /// </summary>
        void ClearCache();

        /// <summary>
        /// Removes the specified item key.
        /// </summary>
        /// <param name="itemKey">The item key.</param>
        void Remove(string itemKey);

        /// <summary>
        /// Inserts the specified value into the cache with the keyString as its Key.
        /// </summary>
        /// <param name="keyString">The key string.</param>
        /// <param name="value">The value.</param>
        /// <param name="cacheDurationInSeconds">The cache duration in seconds.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="isSliding">Is it a sliding expiration</param>
        void Add(string keyString, object value, int cacheDurationInSeconds, CacheManagerPriority priority, bool isSliding = false);

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        IDictionaryEnumerator GetEnumerator();

        /// <summary>
        /// Gets the amount of items in the cache.
        /// </summary>
        /// <returns></returns>
        int GetCount();
    }
}
