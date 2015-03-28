using System;
using System.Collections.Generic;
using System.Configuration;
using SportsWebPt.Common.Logging;
using SportsWebPt.Common.ServiceStack;
using YelpSharp;

namespace SportsWebPt.Platform.Web.Core
{
    public class WebPlatformConfigSettings
    {
        #region Fields

        private static WebPlatformConfigSettings _instance;
        private static readonly Object _lockToken = new object();
        private readonly ILog _logger = LogManager.GetCommonLogger();

        #endregion
        
        #region Properties

        public static WebPlatformConfigSettings Instance
        {
            get
            {
                lock (_lockToken)
                {
                    if (_instance == null)
                    {
                        _instance = new WebPlatformConfigSettings();
                    }
                }

                return _instance;
            }
        }

        public SportsWebPtClientSettings ServiceStackClientSettings { get; private set; }
        public Options YelpOptions { get; private set; }
        public String YelpSearchTerm { get; private set; }
        public int SportsWebPtClinicId { get; private set; }
        public String ClientId { get; private set; }
        public String AuthorityUri { get; private set; }
        public String CallbackUri { get; private set; }
        public String IdentityStore { get; private set; }
        public String SessionPayReturnUri { get; private set; }

        #endregion

        #region Construction

        private WebPlatformConfigSettings()
        {
            try
            {
                BindConfigValues();
            }
            catch (Exception ex)
            {
                _logger.Error("Issue loading configuration settings", ex);
            }
        }

        #endregion

        #region Methods

        private void BindConfigValues()
        {
            ServiceStackClientSettings = new SportsWebPtClientSettings()
            {
                BaseUri = ConfigurationManager.AppSettings["platofrmServiceUri"]
            };

            SportsWebPtClinicId = int.Parse(ConfigurationManager.AppSettings["sportsWebPtClinicId"]);
            YelpSearchTerm = ConfigurationManager.AppSettings["yelpSearchTerm"];

            ClientId = ConfigurationManager.AppSettings["clientId"];
            AuthorityUri = ConfigurationManager.AppSettings["authorityUri"];
            CallbackUri = ConfigurationManager.AppSettings["callbackUri"];
            SessionPayReturnUri = ConfigurationManager.AppSettings["sessionPayReturnUri"];
            IdentityStore = ConfigurationManager.AppSettings["identityStore"];

            YelpOptions = new Options()
            {
                AccessToken = ConfigurationManager.AppSettings["yelpTokenKey"],
                AccessTokenSecret = ConfigurationManager.AppSettings["yelpTokenSecret"],
                ConsumerKey = ConfigurationManager.AppSettings["yelpConsumerKey"],
                ConsumerSecret = ConfigurationManager.AppSettings["yelpConsumerSecret"]
            };

            if (String.IsNullOrEmpty(YelpOptions.AccessToken) ||
                String.IsNullOrEmpty(YelpOptions.AccessTokenSecret) ||
                String.IsNullOrEmpty(YelpOptions.ConsumerKey) ||
                String.IsNullOrEmpty(YelpOptions.ConsumerSecret))
            {
                throw new InvalidOperationException("No OAuth info available.  Please modify Config.cs to add your YELP API OAuth keys");
            }

        }

        #endregion
    }
}
