﻿using System;
using System.Collections.Generic;
using System.Configuration;

using SportsWebPt.Common.Logging;
using SportsWebPt.Common.Utilities.ServiceApi;

namespace SportsWebPt.Platform.Core
{
    public class PlatformServiceConfiguration : IBaseApiConfig
    {
        #region Fields

        private static PlatformServiceConfiguration _instance;
        private static Object _lockToken = new object();
        private ILog _logger = LogManager.GetCommonLogger();

        #endregion

        #region Properties

        public static PlatformServiceConfiguration Instance
        {
            get
            {
                lock (_lockToken)
                {
                    if(_instance == null)
                        _instance = new PlatformServiceConfiguration();
                }

                return _instance;
            }
        }

        public string ApiUrl { get;private set; }

        public string ApiVersion { get; private set; }

        public string RegistrationPathUri { get; private set; }

        public IEnumerable<String> ApiDocumentAssemblies { get; private set; }

        public int ClinicId { get; private set; }

        public Uri ApiUriWithVersion
        {
            get { return new Uri(ApiUrl).At(ApiVersion); }
        }


        #endregion

        #region Construction

        private PlatformServiceConfiguration()
        {
            try
            {
                BindConfigValues();
            }
            catch (Exception ex)
            {
                _logger.Error("PlatformService Configuration Exception", ex);
            }
        }

        #endregion

        #region Methods

        private void BindConfigValues()
        {
            ApiUrl = ConfigurationManager.AppSettings["apiUri"];
            ApiVersion = ConfigurationManager.AppSettings["apiVersion"];
            RegistrationPathUri = ConfigurationManager.AppSettings["registrationPathUri"];
            ApiDocumentAssemblies = ConfigurationManager.AppSettings["apiDocumentAssemblies"].Split(',');
            ClinicId = int.Parse(ConfigurationManager.AppSettings["clinicId"]);
        }

        #endregion

    }
}
