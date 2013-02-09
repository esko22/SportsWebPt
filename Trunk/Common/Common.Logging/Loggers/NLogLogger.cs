using System;

using SportsWebPt.Common.Utilities;

using NLog;

namespace SportsWebPt.Common.Logging
{
    /// <summary>
    /// TODO: this should be moved out of 
    /// Common.Logging to break reference to NLog
    /// </summary>
    public class NLogLogger : ILog
    {
        #region Fields

        private readonly Logger _logger;

        #endregion

        #region Construction

        public NLogLogger(Logger logger)
        {
            Check.Argument.IsNotNull(logger, "ILogger");
            _logger = logger;
        }

        #endregion

        #region ILog Members

        public void Trace(string message)
        {
            _logger.Trace(message);
        }

        public void Trace(string message, Exception exception)
        {
            _logger.TraceException(message,exception);
        }

        public void Trace(Action<FormatMessageHandler> formatMessageCallback)
        {
            throw new NotImplementedException();
        }

        public void Trace(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Trace(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            throw new NotImplementedException();
        }

        public void Trace(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Debug(string message, Exception exception)
        {
            _logger.DebugException(message,exception);
        }

        public void Debug(Action<FormatMessageHandler> formatMessageCallback)
        {
            throw new NotImplementedException();
        }

        public void Debug(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Debug(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            throw new NotImplementedException();
        }

        public void Debug(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Info(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Info(Action<FormatMessageHandler> formatMessageCallback)
        {
            throw new NotImplementedException();
        }

        public void Info(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Info(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            throw new NotImplementedException();
        }

        public void Info(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Warn(Action<FormatMessageHandler> formatMessageCallback)
        {
            throw new NotImplementedException();
        }

        public void Warn(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Warn(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            throw new NotImplementedException();
        }

        public void Warn(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(string message, Exception exception)
        {
            _logger.ErrorException(message, exception);
        }

        public void Error(Action<FormatMessageHandler> formatMessageCallback)
        {
            throw new NotImplementedException();
        }

        public void Error(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Error(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            throw new NotImplementedException();
        }

        public void Error(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Fatal(Action<FormatMessageHandler> formatMessageCallback)
        {
            throw new NotImplementedException();
        }

        public void Fatal(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Fatal(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            throw new NotImplementedException();
        }

        public void Fatal(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            throw new NotImplementedException();
        }

        public bool IsTraceEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsDebugEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsErrorEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsFatalEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsInfoEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsWarnEnabled
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
