using System;

namespace SportsWebPt.Common.Logging
{
    public interface ILoggerFactory
    {
        ILog GetCommonLogger();

        ILog GetLogger(string name);

    }
}
