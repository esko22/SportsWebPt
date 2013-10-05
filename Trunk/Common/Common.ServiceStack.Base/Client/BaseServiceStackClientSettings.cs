using System;

namespace SportsWebPt.Common.ServiceStack
{
    public class BaseServiceStackClientSettings
    {
        #region Fields

        private readonly String _clientType = "json";
        private readonly String _version = "v1";
        private readonly String _baseUri = "http://localhost";
        private readonly int _timeoutInSeconds = 30;

        public const uint DEFAULT_UPLOAD_MULTIPART_SIZE = 20 * 1024 * 1024; //in bytes
        public const uint DEFAULT_DOWNLOAD_MULTIPART_SIZE = 1024 * 1024; //in bytes
        public const uint DEFAULT_MULTIPART_SIZE_THRESHOLD = 25 * 1024 * 1024; //in bytes

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
