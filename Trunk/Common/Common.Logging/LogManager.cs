using System;

namespace SportsWebPt.Common.Logging
{
    public static class LogManager
    {
        #region Fields

		private readonly static Object _lockToken = new object();
        private static ILoggerFactory _loggerFactory;
 
	    #endregion

        #region Methods

        public static ILoggerFactory LoggerFactory
        {
            get
            {
                return _loggerFactory;
            }
            set
            {
                //TODO: refactor to be loaded via reflection through app.config 
                lock (_lockToken)
                {
                    _loggerFactory = value;
                }
            }
        }

        public static ILog GetCommonLogger()
        {
            return _loggerFactory.GetCommonLogger();
        }

        public static ILog GetLogger(String name)
        {
            return _loggerFactory.GetLogger(name);
        }

        #endregion

    }
}
