using System;

namespace SportsWebPt.Common.ServiceStackClient
{
    public class BaseServiceStackClientSettings
    {
        #region Fields

        private readonly String _clientType = "json";
        private readonly String _version = "v1";
        private readonly String _baseUri = "http://localhost";
        private readonly int _timeoutInSeconds = 30;

        #endregion

        #region Properties

        public String BaseUri { get; set; }
        public String Version { get; set; }
        public String AuthToken { get; set; }
        public String ClientType { get; set; }
        public int TimeOutInSeconds { get; set; }
        public Boolean IsDebug { get; set; }

        #endregion

        #region Constructor

        public BaseServiceStackClientSettings()
        {
            BaseUri = _baseUri;
            Version = _version;
            ClientType = _clientType;
            TimeOutInSeconds = _timeoutInSeconds;
        }

        #endregion
    }
}
