using System;
using System.Collections;
using System.Collections.Specialized;

namespace SportsWebPt.Common.Logging
{
    public abstract class LoggerFactory : ILoggerFactory
    {
        #region Fields

        private readonly Hashtable _cachedLoggers = CollectionsUtil.CreateCaseInsensitiveHashtable();

        #endregion

        #region ILoggerFactory Members

        protected abstract ILog CreateLogger(string name);

        public ILog GetCommonLogger()
        {
            return GetLogger("Common");
        }

        public ILog GetLogger(string name)
        {
            return FindLogger(name);
        }

        private ILog FindLogger(string name)
        {
            var log = _cachedLoggers[name] as ILog;
            if (log == null)
            {
                lock (_cachedLoggers)
                {
                    log = _cachedLoggers[name] as ILog;
                    if (log == null)
                    {
                        log = CreateLogger(name);
                        if (log == null)
                        {
                            throw new ArgumentException(string.Format("{0} returned null on creating logger instance for name {1}", this.GetType().FullName, name));
                        }
                        _cachedLoggers.Add(name, log);
                    }
                }
            }

            return log;
        }

        #endregion
    }
}
