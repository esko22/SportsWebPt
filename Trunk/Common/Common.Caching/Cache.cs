using System;
using System.Collections.Concurrent;

namespace SportsWebPt.Common.Caching
{
    public class Cache<TTypeOfCacheKey,TTypeOfCachedObject> where TTypeOfCachedObject : class 
    {

        #region Fields

        protected readonly ConcurrentDictionary<TTypeOfCacheKey, TTypeOfCachedObject> _cache = new ConcurrentDictionary<TTypeOfCacheKey, TTypeOfCachedObject>();

        #endregion

        #region Methods

        public void Add(TTypeOfCacheKey cacheKey, TTypeOfCachedObject cachedObject)
        {
            _cache.GetOrAdd(cacheKey,cachedObject);
        }

        public TTypeOfCachedObject Remove(TTypeOfCacheKey cacheKey)
        {
            TTypeOfCachedObject cachedObject = null;
            _cache.TryRemove(cacheKey,out cachedObject);

            return cachedObject;
        }

        public virtual TTypeOfCachedObject GetCacheItem(TTypeOfCacheKey cacheKey)
        {
            if(_cache.ContainsKey(cacheKey))
            {
                return _cache[cacheKey];
            }

            return null;
        }

        #endregion




    }
}
