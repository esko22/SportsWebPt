using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SportsWebPt.Common.Utilities
{
    public interface ICacheManager
    {
        /// <summary>
        /// Gets the normal cache time.
        /// </summary>
        /// <value>The normal cache time.</value>
        TimeSpan NormalCacheTime { get; set; }

        /// <summary>
        /// Gets the short cache time.
        /// </summary>
        /// <value>The short cache time.</value>
        TimeSpan ShortCacheTime { get; set; }

        /// <summary>
        /// Gets the long cache time.
        /// </summary>
        /// <value>The long cache time.</value>
        TimeSpan LongCacheTime { get; set; }

        /// <summary>
        /// Determines whether the Cache contains the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// 	<c>true</c> if the cache contains the key; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsKey(string key);

        /// <summary>
        /// Gets the specified key from the cache.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <exception cref="T:System.ArgumentNullException">If key is null</exception>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">If key is not found</exception>
        /// <returns></returns>
        TValue Get<TValue>(string key);

        /// <summary>
        /// Gets the item and removes it from cache.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <exception cref="T:System.ArgumentNullException">If key is null</exception>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">If key is not found</exception>
        /// <returns></returns>
        TValue Pop<TValue>(string key);

        /// <summary>
        /// Gets the specified key from the cache.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        bool TryGetValue<TValue>(string key, out TValue value);

        /// <summary>
        /// Tries the pop value. Popping gets the item and removes it from cache.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        bool TryPopValue<TValue>(string key, out TValue value);

        /// <summary>
        /// Inserts the specified key into the cache.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void Add<TValue>(string key, TValue value);

        /// <summary>
        /// Inserts the specified key into the cache.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="cacheDuration">Duration of the cache.</param>
        void Add<TValue>(string key, TValue value, TimeSpan cacheDuration);

        /// <summary>
        /// Inserts the specified key into the cache.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="cacheDuration">Duration of the cache.</param>
        /// <param name="priority">The priority.</param>
        void Add<TValue>(string key, TValue value, TimeSpan cacheDuration, CacheManagerPriority priority);

        /// <summary>
        /// Inserts the specified key into the cache.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="cacheDuration">Duration of the cache.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="isSliding">Is this a sliding expiration</param>
        void Add<TValue>(string key, TValue value, TimeSpan cacheDuration, CacheManagerPriority priority, bool isSliding);


        /// <summary>
        /// Removes the specified key from the cache.
        /// </summary>
        /// <param name="key">The key.</param>
        void Remove(string key);

        /// <summary>
        /// Clears the cache.
        /// </summary>
        void ClearCache();

        /// <summary>
        /// Gets the amount of items in the cache.
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Gets a list of cached objects.
        /// </summary>
        /// <returns></returns>
        IList<string> GetListOfCachedKeys();
    }
}
