using System;
using System.Collections;


namespace SportsWebPt.Common.Utilities
{
    public class NullCache : ICacheProvider
    {
        #region ICacheProvider Members

        public object this[string key]
        {
            get { return null; }
        }

        public object Get(string itemKey)
        {
            return null;
        }

        public void ClearCache()
        {
        }

        public void Remove(string itemKey)
        {
        }

        public void Add(string keyString, object value, int cacheDurationInSeconds, CacheManagerPriority priority, bool isSliding)
        {
            
        }

        public void Add(string keyString, object value, int cacheDurationInSeconds, CacheManagerPriority priority)
        {
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            return new Hashtable().GetEnumerator();
        }

        public int GetCount()
        {
            return 0;
        }

        #endregion
    }
}