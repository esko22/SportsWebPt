using System;

namespace SportsWebPt.Common.Logging
{

    public delegate string FormatMessageHandler(string format, params object[] args);
    
    public interface ILog
    {
        void Trace(String message);

        void Trace(String message, Exception exception);

        void Trace(Action<FormatMessageHandler> formatMessageCallback);

        void Trace(Action<FormatMessageHandler> formatMessageCallback, Exception exception);

        void Trace(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback);

        void Trace(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

        void Debug(String message);

        void Debug(String message, Exception exception);

        void Debug(Action<FormatMessageHandler> formatMessageCallback);
    
        void Debug(Action<FormatMessageHandler> formatMessageCallback, Exception exception);

        void Debug(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback);

        void Debug(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

        void Info(String message);

        void Info(String message, Exception exception);

        void Info(Action<FormatMessageHandler> formatMessageCallback);

        void Info(Action<FormatMessageHandler> formatMessageCallback, Exception exception);

        void Info(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback);

        void Info(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

        void Warn(String message);

        void Warn(String message, Exception exception);

        void Warn(Action<FormatMessageHandler> formatMessageCallback);

        void Warn(Action<FormatMessageHandler> formatMessageCallback, Exception exception);

        void Warn(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback);

        void Warn(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

        void Error(String message);

        void Error(String message, Exception exception);
 
        void Error(Action<FormatMessageHandler> formatMessageCallback);

        void Error(Action<FormatMessageHandler> formatMessageCallback, Exception exception);

        void Error(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback);

        void Error(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

        void Fatal(String message);
        
        void Fatal(String message, Exception exception);
        
        void Fatal(Action<FormatMessageHandler> formatMessageCallback);

        void Fatal(Action<FormatMessageHandler> formatMessageCallback, Exception exception);

        void Fatal(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback);

        void Fatal(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception);

        bool IsTraceEnabled { get; }

        bool IsDebugEnabled { get; }

        bool IsErrorEnabled { get; }
        
        bool IsFatalEnabled { get; }
        
        bool IsInfoEnabled { get; }
        
        bool IsWarnEnabled { get; }
    }
}
