using System;
using System.IO;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceHost;

namespace SportsWebPt.Common.ServiceStack
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
            if(clientSettings == null)
                throw new ArgumentNullException("ClientSettings");

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

        protected TResponse GetSync<TResponse>(IReturn<TResponse> request)
        {
            return _serviceClientBase.Get(request);
        }

        protected TResponse GetSync<TResponse>(string uri)
        {
            return _serviceClientBase.Get<TResponse>(uri);
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

        protected TResponse PostSync<TResponse>(IReturn<TResponse> request)
        {
            return _serviceClientBase.Post(request);
        }

        protected TResponse PostFile<TResponse>(string uri, FileInfo file, object request)
        {
            return _serviceClientBase.PostFileWithRequest<TResponse>(uri, file, request);
        }

        protected TResponse Put<TResponse>(IReturn<TResponse> request)
        {
            return _serviceClientBase.Put(request);
        }

        protected TResponse Patch<TResponse>(IReturn<TResponse> request)
        {
            return _serviceClientBase.Patch(request);
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
