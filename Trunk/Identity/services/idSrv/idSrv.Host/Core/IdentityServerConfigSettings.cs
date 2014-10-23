using System;
using System.Configuration;

namespace SportsWebPt.Identity.Services.Core
{
    public class IdentityServerConfigSettings
    {
        #region Fields

        private static IdentityServerConfigSettings _instance;
        private static readonly Object _lockToken = new object();

        #endregion

        #region Properties

        public static IdentityServerConfigSettings Instance
        {
            get
            {
                lock (_lockToken)
                {
                    if(_instance == null)
                        _instance = new IdentityServerConfigSettings();
                }

                return _instance;
            }
        }

        public string FacebookClientId { get;private set; }

        public string FacebookClientSecret { get; private set; }

        public string GoogleClientId { get; private set; }

        public string GoogleClientSecret { get; private set; }

        public string PersistanceConnection { get; private set; }

        public string ConfigConnection { get; private set; }

        public string PublicHostName { get; private set; }


        #endregion

        #region Construction

        private IdentityServerConfigSettings()
        {
            try
            {
                BindConfigValues();
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region Methods

        private void BindConfigValues()
        {
            FacebookClientId = ConfigurationManager.AppSettings["facebookClientId"];
            FacebookClientSecret = ConfigurationManager.AppSettings["facebookClientSecret"];
            GoogleClientId = ConfigurationManager.AppSettings["googleClientId"];
            GoogleClientSecret = ConfigurationManager.AppSettings["googleClientSecret"];
            PersistanceConnection = ConfigurationManager.AppSettings["persistanceConnection"];
            ConfigConnection = ConfigurationManager.AppSettings["configConnection"];
            PublicHostName = ConfigurationManager.AppSettings["publicHostName"];
        }

        #endregion
    }
}