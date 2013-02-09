using System;
using System.Text.RegularExpressions;


namespace SportsWebPt.Common.Utilities
{
    /// <summary>
    /// Represents the method that performs an action.
    /// Used when the object is not in the cache.
    /// </summary>
    public delegate T AddToCacheAction<T>();

    public static class CacheManagerExtensions
    {
        /// <summary>
        /// Caches the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="addToCache">The add to cache.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns></returns>
        public static T Add<T>(this ICacheManager cacheManager, AddToCacheAction<T> addToCache, string cacheKey)
        {
            return Add(cacheManager, addToCache, cacheKey, cacheManager.NormalCacheTime);
        }

        /// <summary>
        /// Caches the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="addToCache">The add to cache.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="cacheDuration">Duration of the cache.</param>
        /// <returns></returns>
        public static T Add<T>(this ICacheManager cacheManager, AddToCacheAction<T> addToCache, string cacheKey,
                               TimeSpan cacheDuration)
        {
            return Add(cacheManager, addToCache, cacheKey, cacheDuration, CacheManagerPriority.Normal);
        }


        /// <summary>
        /// Caches the object of T.
        /// If the object is in the cache then it will return that object
        /// or else it will insert <see cref="addToCache"/> to the cache.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="addToCache">This will run if the object is not in the cache.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="cacheDuration">Duration of the cache.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="isSliding">Is it sliding expiration</param>
        /// <returns>The object from the cache.</returns>
        /// 
        public static T Add<T>(this ICacheManager cacheManager, AddToCacheAction<T> addToCache, string cacheKey,
                               TimeSpan cacheDuration, CacheManagerPriority priority, bool isSliding = false)
        {
            T cachedObject = default(T);
            if (cacheManager.ContainsKey(cacheKey))
            {
                cachedObject = cacheManager.Get<T>(cacheKey);
            }

            //if we are null or the default of the type
            if (Equals(cachedObject, default(T)))
            {
                cachedObject = addToCache();
                //we have a value so lets add it to the cache
                if (!Equals(cachedObject, default(T)))
                {
                    cacheManager.Add(cacheKey, cachedObject, cacheDuration, priority, isSliding);
                }
            }


            return cachedObject;
        }


        /// <summary>
        /// Removes all the objects from the cache that start with <see cref="keyStartsWith"/>.
        /// </summary>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="keyStartsWith">The starts with.</param>
        public static void RemoveAllThatStartWith(this ICacheManager cacheManager, string keyStartsWith)
        {
            RemoveAll(cacheManager, item => item.StartsWith(keyStartsWith));
        }

        /// <summary>
        /// Removes all cache object that match the Regular Expression pattern.
        /// </summary>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="regxPattern">The regx.</param>
        public static void RemoveAllByPattern(this ICacheManager cacheManager, string regxPattern)
        {
            var regex = new Regex(regxPattern);
            RemoveAll(cacheManager, item => regex.Match(item).Success);
        }

        /// <summary>
        /// Removes all Objects from the cache that match the condition.
        /// </summary>
        /// <param name="match">The Predicate<> delegate that defines the conditions of the elements to search for.</param>
        public static void RemoveAll(this ICacheManager cacheManager, Predicate<string> match)
        {
            if (match == null)
                throw new ArgumentNullException("match");

            foreach (string item in cacheManager.GetListOfCachedKeys())
            {
                if (match(item.Contains("|") ? item.Substring(0, item.LastIndexOf("|")) : item))
                    //this will remove the extra information from the end of the key.
                    cacheManager.Remove(item);
            }
        }
    }
}