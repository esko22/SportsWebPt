using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SportsWebPt.Common.Utilities
{
    public abstract class AbstractCacheBase<TKey, TValue> : DisposableBase
    {
        // Fields
        private readonly IDictionary<TKey, TValue> cache;
        private readonly ReaderWriterLockSlim syncLock;

        // Methods
        protected AbstractCacheBase()
            : this(null)
        {
        }

        protected AbstractCacheBase(IEqualityComparer<TKey> comparer)
        {
            this.syncLock = new ReaderWriterLockSlim();
            this.cache = new Dictionary<TKey, TValue>(comparer);
        }

        protected override void DisposeCore()
        {
            this.cache.Clear();
            this.syncLock.Dispose();
            base.DisposeCore();
        }

        protected TValue GetOrCreate(TKey key, Func<TValue> factory)
        {
            TValue local;
            Check.Argument.IsNotNull(key, "key");
            Check.Argument.IsNotNull(factory, "factory");
            using (this.syncLock.ReadAndWrite())
            {
                if (this.cache.TryGetValue(key, out local))
                {
                    return local;
                }
                using (this.syncLock.Write())
                {
                    if (!this.cache.TryGetValue(key, out local))
                    {
                        local = factory();
                        this.cache.Add(key, local);
                    }
                }
            }
            return local;
        }
    }


}
