using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using SportsWebPt.Common.Logging;

namespace SportsWebPt.Common.Caching
{
    public class LastAccessAwareCache<TTypeOfCacheKey, TTypeOfCacheObject> where TTypeOfCacheObject : class, ILastAccessAwareCacheMemeber 
    {

        #region Fields

        private Timer _expirationPoller;
        private readonly TimeSpan _expirationLength;
        private readonly TimeSpan _pollingLength;
        private ConcurrentDictionary<TTypeOfCacheKey, TTypeOfCacheObject> _cache;
        private ILog _logger = LogManager.GetCommonLogger();

        #endregion

        #region Construction

        public LastAccessAwareCache(TimeSpan expirationLength,TimeSpan pollingLength, IEqualityComparer<TTypeOfCacheKey> equalityComparer)
        {
            _expirationLength = expirationLength;
            _pollingLength = pollingLength;
            _expirationPoller = new Timer(RemoveUnaccessedCacheItems, null, _pollingLength, _pollingLength);
            _cache = new ConcurrentDictionary<TTypeOfCacheKey, TTypeOfCacheObject>(equalityComparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LastAccessAwareCache&lt;TTypeOfCacheKey, TTypeOfCacheObject&gt;"/> class.
        /// 10 minute default expiration
        /// 1 minute polling time
        /// </summary>
        public LastAccessAwareCache()
            : this(new TimeSpan(0, 10, 0), new TimeSpan(0, 1, 0), null)
        {;} 

        #endregion

        #region Methods

        private void RemoveUnaccessedCacheItems(Object state)
        {
            var cacheKeysForRemoval = _cache.Keys.Where(c => !_cache[c].IsSticky && _cache[c].LastAccessed < (DateTime.Now - _expirationLength)).ToList();

            TTypeOfCacheObject removedObject;
            cacheKeysForRemoval.ForEach(p =>
                                            {
                                                _cache.TryRemove(p, out removedObject);
                                                removedObject.Dispose();
                                                _logger.Info(String.Format("Removed item {0} @ {1}",p,DateTime.Now.ToShortTimeString()));
                                            });
        }


        public TTypeOfCacheObject GetCacheItem(TTypeOfCacheKey cacheKey)
        {
            TTypeOfCacheObject cacheItem;

            if (_cache.TryGetValue(cacheKey, out cacheItem))
            {
                cacheItem.LastAccessed = DateTime.Now;
            }

            return cacheItem;
        }

        public void AddCacheItem(TTypeOfCacheKey cacheKey, TTypeOfCacheObject cacheObject)
        {
            cacheObject.LastAccessed = DateTime.Now;
            _cache.TryAdd(cacheKey, cacheObject);
            _logger.Info(String.Format("Added item {0} @ {1}", cacheKey, DateTime.Now.ToShortTimeString()));
        }

        public Boolean ContainsKey(TTypeOfCacheKey cacheKey)
        {
            return _cache.ContainsKey(cacheKey);
        }

        public void ClearCache()
        {
            _cache.Clear();
        }

        #endregion

    }
}
