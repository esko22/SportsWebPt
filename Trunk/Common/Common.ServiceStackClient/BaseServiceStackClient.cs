using System;

using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceHost;
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
            Check.Argument.IsNotNull(clientSettings, "ClientSettings");
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


        protected TResponse GetSync<TResponse>(string uri)
        {
            return _serviceClientBase.Get<TResponse>(uri);
        }

        protected TResponse GetSync<TResponse>(IReturn<TResponse> request)
        {
            return _serviceClientBase.Get(request);
        }


        protected void PostAsync<TResponse>(string uri, object request, Action<TResponse, Exception> callback)
        {
            _serviceClientBase.PostAsync(uri, request,
                                            successResponse => callback(successResponse, null),
                                            callback);
        }

        protected TResponse PostSync<TResponse>(string uri, object request)
        {
            return _serviceClientBase.Post<TResponse>(uri, request);
        }

        protected TResponse PutSync<TResponse>(string uri, object request)
        {
            return _serviceClientBase.Put<TResponse>(uri, request);
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
