using System;

using NLog;

namespace SportsWebPt.Common.Logging
{
    public class NLogLoggerFactory : LoggerFactory
    {

        #region ILoggerFactory Members

        protected override ILog CreateLogger(string name)
        {
            return new NLogLogger(NLog.LogManager.GetLogger(name));
        }

        #endregion
    }
}
