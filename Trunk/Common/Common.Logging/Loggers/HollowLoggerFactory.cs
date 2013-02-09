﻿using System;

namespace SportsWebPt.Common.Logging
{
    public class HollowLoggerFactory : ILoggerFactory
    {
        #region Fields

        private ILog _logger = new HollowLogger();

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
