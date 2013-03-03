using System;

using ServiceStack.ServiceClient.Web;
using SportsWebPt.Common.Utilities;

namespace SportsWebPt.Common.ServiceStackClient
{
    public abstract class BaseServiceStackClient : IDisposable
    {
        #region Fields

        protected BaseServiceStackClientSettings _settings;
        private ServiceClientBase _serviceClientBase;

        #endregion

        #region Construction

        protected BaseServiceStackClient(BaseServiceStackClientSettings clientSettings)
        {
            Check.Argument.IsNotNull(clientSettings, "AnnotationClientSettings");
            _settings = clientSettings;

            switch (clientSettings.ClientType.ToLowerInvariant())
            {
                case "json":
                    _serviceClientBase = new JsonServiceClient(clientSettings.BaseUri);
                    break;

                case "jsv":
                    _serviceClientBase = new JsvServiceClient(clientSettings.BaseUri);
                    break;

                case "xml":
                    _serviceClientBase = new XmlServiceClient(clientSettings.BaseUri);
                    break;
            }

            _serviceClientBase.Timeout = new TimeSpan(0, 0, 0, clientSettings.TimeOutInSeconds);

            AppendAuthToken();
        }

        protected BaseServiceStackClient(BaseServiceStackClientSettings clientSettings, bool isDebug = false)
             : this(clientSettings)
        {
           if (isDebug)
               AppendDebugFlag(true);
        }

        #endregion

        #region Methods

        private void AppendAuthToken()
        {
            if (!String.IsNullOrEmpty(_settings.AuthToken))
                _serviceClientBase.LocalHttpWebRequestFilter +=
                    request => request.Headers.Add(String.Format("Authorization: Bearer {0}", _settings.AuthToken));
        }

        private void AppendDebugFlag(bool isEnabled)
        {
            if (isEnabled)
                _serviceClientBase.LocalHttpWebRequestFilter += request => request.Headers.Add("IAFT-debug: true");
        }

        
        protected TResult GetSync<TResponse, TResult>(string uri)
            where TResponse : BaseResponse<TResult>
            where TResult : class
        {
            try
            {
                return _serviceClientBase.Get<TResponse>(uri).Response;
            }
            catch (WebServiceException e)
            {
                return default(TResult); // TODO: handle in a better fashion?
            }
        }

        protected void PostAsync<TResponse, TResult>(string uri, object request, Action<TResult, Exception> callback)
            where TResponse : BaseResponse<TResult>
            where TResult : class
        {
            try
            {
                _serviceClientBase.PostAsync<TResponse>(uri, request,
                     successResponse =>
                     {
                         callback(successResponse.Response, null);
                     },
                     (errorResponse, ex) =>
                     {
                         callback(errorResponse.Response, ex);
                     });
            }
            catch (WebServiceException e)
            {
                callback(default(TResult), e);
            }
        }

        protected TResponse PostSync<TResponse, TResult>(string uri, object request)
            where TResponse : BaseResponse<TResult>
            where TResult : class
        {
            try
            {
                return _serviceClientBase.Post<TResponse>(uri, request);
            }
            catch (WebServiceException e)
            {
                return null; // TODO: handle in a better fashion?
            }
        }

        #endregion


        #region IDisposable
        
        public void Dispose()
        {
            if(_serviceClientBase != null)
                _serviceClientBase.Dispose();

            _serviceClientBase = null;
            _settings = null;
        } 

        #endregion
    }
}
