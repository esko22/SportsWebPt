using System;
using System.Collections.Generic;
using System.Configuration;
using SportsWebPt.Common.Logging;
using SportsWebPt.Common.ServiceStackClient;
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
        public String FacebookClientKey { get; private set; }
        public String FacebookClientSecret { get; private set; }
        public String GoogleClientKey { get; private set; }
        public String GoogleClientSecret { get; private set; }
        public Options YelpOptions { get; private set; }
        public String YelpSearchTerm { get; private set; }

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

            FacebookClientKey = ConfigurationManager.AppSettings["facebookClientKey"];
            FacebookClientSecret = ConfigurationManager.AppSettings["facebookClientSecret"];
            GoogleClientKey = ConfigurationManager.AppSettings["googleClientKey"];
            GoogleClientSecret = ConfigurationManager.AppSettings["googleClientSecret"];
            YelpSearchTerm = ConfigurationManager.AppSettings["yelpSearchTerm"];


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
