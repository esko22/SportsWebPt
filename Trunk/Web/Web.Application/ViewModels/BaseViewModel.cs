using System;
using System.Collections.Generic;

using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Application
{
    public abstract class BaseViewModel
    {
        #region Construction

        protected BaseViewModel()
        {
            AreaBundles = new List<String>();
        }

        #endregion

        #region Properties

        public List<String> AreaBundles { get; private set; }

        public User User { get; set; }

        public String GoogleAnalyticsKey { get; set; }

        #endregion
    }
}