using System;

using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;
using SportsWebPt.Common.Logging;

namespace SportsWebPt.Common.ServiceStack
{
    public class LoggingServiceRunner<T> : ServiceRunner<T>
    {
        #region Fields
        
        private readonly ILog _logger = LogManager.GetCommonLogger();

        #endregion

        #region Construction

        public LoggingServiceRunner(IAppHost appHost, ActionContext actionContext)
            : base(appHost, actionContext)
        {} 

        #endregion

        #region Methods

        public override void BeforeEachRequest(IRequestContext requestContext, T request)
        {
            _logger.Info(String.Format("Request Attempt - RequestUri: {0}", requestContext.AbsoluteUri));

            base.BeforeEachRequest(requestContext, request);
        }

        public override object AfterEachRequest(IRequestContext requestContext, T request, object response)
        {
            if (response is IHttpError)
            {
                var error = response as IHttpError;
                _logger.Info(String.Format("Request Failed - RequestUri: {0}; ResponseStatus: {1} = {2} ({3})",
                    requestContext.AbsoluteUri, error.Status, error.StatusCode, error.StatusDescription));
            }
            else
                _logger.Info(String.Format("Request Success - RequestUri: {0};", requestContext.AbsoluteUri));

            return base.AfterEachRequest(requestContext, request, response);
        }

        public override object HandleException(IRequestContext requestContext, T request, Exception ex)
        {
            _logger.Error(String.Format("Error - RequestUri: {0}", requestContext.AbsoluteUri), ex);

            return HttpResponseFormatter.InternalServerError(ex.Message,ex);
        }

        #endregion

    }
}
