using System;

namespace SportsWebPt.Common.Logging
{
    public class DebugLoggerFactory : ILoggerFactory
    {
        #region Fields

        private readonly ILog _logger = new DebugLogger();

        #endregion


        #region ILoggerFactory Members

        public ILog GetCommonLogger()
        {
            return _logger;
        }

        public ILog GetLogger(string name)
        {
            return _logger;
        }

        #endregion
    }
}
