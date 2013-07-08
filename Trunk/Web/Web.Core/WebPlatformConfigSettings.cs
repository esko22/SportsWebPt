using System;
using System.Collections.Generic;
using System.Configuration;
using SportsWebPt.Common.Logging;
using SportsWebPt.Common.ServiceStackClient;

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

        }

        #endregion
    }
}
