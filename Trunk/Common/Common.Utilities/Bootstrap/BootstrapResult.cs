using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportsWebPt.Common.Utilities
{
    public class BootstrapResult
    {
        public TaskContinuation Continuation { get; private set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }

        public object Data { get; set; }
        
         public BootstrapResult(TaskContinuation continuation, object data, string message): this(continuation, data, message, null)
         {
         }

        public BootstrapResult(TaskContinuation continuation, object data, string message, Exception exception)
         {
             Continuation = continuation;
             Data = data;
             Message = message;
            Exception = exception;
         }
    }
}
