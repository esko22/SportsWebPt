using System;

namespace SportsWebPt.Common.Logging
{
    public class DebugLogger : ILog
    {
        #region ILog Members

        public void Trace(string message)
        {
            ;
        }

        public void Trace(string message, Exception exception)
        {
            ;
        }

        public void Trace(Action<FormatMessageHandler> formatMessageCallback)
        {
            ;
        }

        public void Trace(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            ;
        }

        public void Trace(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            ;
        }

        public void Trace(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            ;
        }

        public void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Debug(string message, Exception exception)
        {
            System.Diagnostics.Debug.WriteLine(message);
            System.Diagnostics.Debug.WriteLine(exception.Message);

            if (exception.InnerException != null)
                Debug(exception.InnerException.Message, exception.InnerException);
        }

        public void Debug(Action<FormatMessageHandler> formatMessageCallback)
        {
            ;
        }

        public void Debug(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            ;
        }

        public void Debug(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            ;
        }

        public void Debug(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            ;
        }

        public void Info(String message)
        {
            Debug(message);
        }

        public void Info(string message, Exception exception)
        {
            Debug(message, exception);
        }

        public void Info(Action<FormatMessageHandler> formatMessageCallback)
        {
            ;
        }

        public void Info(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            ;
        }

        public void Info(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            ;
        }

        public void Info(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            ;
        }

        public void Warn(string message)
        {
            Debug(message);
        }

        public void Warn(string message, Exception exception)
        {
            Debug(message, exception);
        }

        public void Warn(Action<FormatMessageHandler> formatMessageCallback)
        {
            ;
        }

        public void Warn(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            ;
        }

        public void Warn(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            ;
        }

        public void Warn(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            ;
        }

        public void Error(string message)
        {
            Debug(message);
        }

        public void Error(string message, Exception exception)
        {
            Debug(message,exception);
        }

        public void Error(Action<FormatMessageHandler> formatMessageCallback)
        {
            ;
        }

        public void Error(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            ;
        }

        public void Error(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            ;
        }

        public void Error(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            ;
        }

        public void Fatal(string message)
        {
            Debug(message);
        }

        public void Fatal(string message, Exception exception)
        {
            Debug(message, exception);
        }

        public void Fatal(Action<FormatMessageHandler> formatMessageCallback)
        {
            ;
        }

        public void Fatal(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            ;
        }

        public void Fatal(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            ;
        }

        public void Fatal(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            ;
        }

        public bool IsTraceEnabled
        {
            get { return false; }
        }

        public bool IsDebugEnabled
        {
            get { return false; }
        }

        public bool IsErrorEnabled
        {
            get { return false; }
        }

        public bool IsFatalEnabled
        {
            get { return false; }
        }

        public bool IsInfoEnabled
        {
            get { return false; }
        }

        public bool IsWarnEnabled
        {
            get { return false; }
        }

        #endregion

    }
}
