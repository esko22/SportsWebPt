using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SportsWebPt.Common.Utilities
{
    /// <summary>
    /// The Cache Manager, responsible for caching objects using any type of cache provider
    /// </summary>
    public class CacheManager : ICacheManager
    {

        #region Fields

        private const int LongCacheTimeSeconds = 1500; // 25 minutes
        private const int NormalCacheTimeSeconds = 600; // 10 minutes
        private const int ShortCacheTimeSeconds = 300; // 5 minutes
        private readonly ICacheProvider cacheProvider;

        #endregion


        #region Constructors
        
        public CacheManager(ICacheProvider cacheProvider)
        {
            if (cacheProvider == null)
                throw new ArgumentNullException("cacheProvider");

            ShortCacheTime = new TimeSpan(0, 0, 0, ShortCacheTimeSeconds);
            NormalCacheTime = new TimeSpan(0, 0, 0, NormalCacheTimeSeconds);
            LongCacheTime = new TimeSpan(0, 0, 0, LongCacheTimeSeconds);
            this.cacheProvider = cacheProvider;
        }

        #endregion


        #region Properties

        /// <summary>
        /// Gets the get default cache time.
        /// </summary>
        /// <value>The get default cache time.</value>
        public TimeSpan NormalCacheTime { get; set; }

        /// <summary>
        /// Gets the get ten minutes cache time.
        /// </summary>
        /// <value>The get ten minutes cache time.</value>
        public TimeSpan ShortCacheTime { get; set; }

        /// <summary>
        /// Gets the get long cache time.
        /// </summary>
        /// <value>The get long cache time.</value>
        public TimeSpan LongCacheTime { get; set; }

        #endregion


        #region Indexer

        public object this[string key]
        {
            get { return GetFromCache(key); }
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Determines whether the Cache contains the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// 	<c>true</c> if the cache contains the key; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsKey(string key)
        {
            return GetFromCacheObject(key) != null;
        }

        /// <summary>
        /// Gets the specified key from the cache.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <exception cref="T:System.ArgumentNullException">If key is null</exception>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">If key is not found</exception>
        /// <returns>Default value of the type. If this is a value type, null will not be returned</returns>
        public TValue Get<TValue>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            if (!ContainsKey(key))
            {
                throw new KeyNotFoundException("Specified key could not be found: " + key);
            }

            return GetFromCache<TValue>(key);
        }

        /// <summary>
        /// Gets the item and removes it from cache.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="T:System.ArgumentNullException">If key is null</exception>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">If key is not found</exception>
        public TValue Pop<TValue>(string key){
            var result = Get<TValue>(key);
            Remove(key);
            return result;
        }

        /// <summary>
        /// Gets the specified key from the cache.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool TryGetValue<TValue>(string key, out TValue value)
        {
            bool rVal = false;
            value = default(TValue);
            try
            {
                if (!string.IsNullOrEmpty(key) && ContainsKey(key))
                {
                    value = GetFromCache<TValue>(key);
                    rVal = true;
                }
            }
            catch (Exception)
            {
            }

            return rVal;
        }

        /// <summary>
        /// Tries the pop value. Popping gets the item and removes it from cache.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool TryPopValue<TValue>(string key, out TValue value){
            var result = TryGetValue<TValue>(key, out value);
            if(result){
                Remove(key);
            }

            return result;
            
            
        }

        /// <summary>
        /// Inserts the specified key into the cache.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add<TValue>(string key, TValue value)
        {
            Add(key, value, NormalCacheTime);
        }

        /// <summary>
        /// Inserts the specified key into the cache.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="cacheDuration">Duration of the cache.</param>
        public void Add<TValue>(string key, TValue value, TimeSpan cacheDuration)
        {
            Add(key, value, cacheDuration, CacheManagerPriority.Default);
        }

        /// <summary>
        /// Inserts the specified key into the cache.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="cacheDuration">Duration of the cache.</param>
        /// <param name="priority">The priority.</param>
        public void Add<TValue>(string key, TValue value, TimeSpan cacheDuration, CacheManagerPriority priority)
        {
            cacheProvider.Add(MakeKey(key), value, (int) cacheDuration.TotalSeconds, priority);
        }

        /// <summary>
        /// Inserts the specified key into the cache.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="cacheDuration">Duration of the cache.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="isSliding">Is sliding expiration</param>
        public void Add<TValue>(string key, TValue value, TimeSpan cacheDuration, CacheManagerPriority priority, bool isSliding)
        {
            cacheProvider.Add(MakeKey(key), value, (int)cacheDuration.TotalSeconds, priority, isSliding);
        }

        /// <summary>
        /// Removes the specified key from the cache.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(string key)
        {
            cacheProvider.Remove(MakeKey(key));
        }

        /// <summary>
        /// Clears the cache.
        /// </summary>
        public void ClearCache()
        {
            cacheProvider.ClearCache();
        }

        /// <summary>
        /// Gets the amount of items in the cache.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return cacheProvider.GetCount();
        }

        /// <summary>
        /// Gets a list of cached objects.
        /// </summary>
        /// <returns></returns>
        public IList<string> GetListOfCachedKeys()
        {
            IDictionaryEnumerator itemsInCache = cacheProvider.GetEnumerator();
            IList<string> keysInCache = new List<string>();
            while (itemsInCache.MoveNext())
            {
                keysInCache.Add(itemsInCache.Key.ToString());
            }
            return keysInCache;
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Creates the key used for identifying the cache object.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private static string MakeKey(string key)
        {
            //could have more complicated logic here if needed
            return key;
        }


        /// <summary>
        /// Gets from cache object.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private TValue GetFromCache<TValue>(string key)
        {
            object result = GetFromCache(MakeKey(key));
            if (result == null)
            {
                return default(TValue);
            }
            else
            {
                return (TValue)result;
            }
        }

        /// <summary>
        /// Gets from cache object.
        /// This doesn't use any boxing.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private object GetFromCacheObject(string key)
        {
            return GetFromCache(MakeKey(key));
        }

        /// <summary>
        /// Gets from cache object.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        private object GetFromCache(string key)
        {
            return cacheProvider[key];
        }

        #endregion
    }
}
