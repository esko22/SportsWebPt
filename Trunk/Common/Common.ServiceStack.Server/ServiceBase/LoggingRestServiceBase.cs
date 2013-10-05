using System;
using System.Diagnostics;


using ServiceStack.Text;
using SportsWebPt.Common.Logging;

namespace SportsWebPt.Common.ServiceStack
{
    public class LoggingRestServiceBase<TTypeOfRequest,TTypeOfResponse> : ExtendedRestServiceBase<TTypeOfRequest,TTypeOfResponse>
    {
        #region Fields

        private readonly ILog _logger = LogManager.GetCommonLogger();
        private Stopwatch _requestStopwatch;

        #endregion

        protected override void OnBeforeExecute(TTypeOfRequest request)
        {
            _logger.Info(String.Format("{0} request submitted", ServiceName));
            _requestStopwatch = Stopwatch.StartNew();

            base.OnBeforeExecute(request);
        }

        protected override object OnAfterExecute(object response)
        {
            _requestStopwatch.Stop();
            _logger.Info(String.Format("{0} request completed with duration of {1}", ServiceName, _requestStopwatch.Elapsed));

            return base.OnAfterExecute(response);
        }

        protected override object HandleException(global::ServiceStack.ServiceHost.IHttpRequest httpReq, TTypeOfRequest request, Exception ex)
        {
            _logger.Error(GetRequestErrorBody(), ex);

            return base.HandleException(httpReq, request, ex);
        }

        public virtual string GetRequestErrorBody()
        {
            var requestString = String.Empty;
            try
            {
                requestString = TypeSerializer.SerializeToString(CurrentRequestDto);
            }
            catch
            { }//Serializing request successfully is not critical and only provides added error info

            return string.Format("[{0}: {1}]:\n[REQUEST: {2}]", GetType().Name, DateTime.UtcNow, requestString);
        }

    }
}
