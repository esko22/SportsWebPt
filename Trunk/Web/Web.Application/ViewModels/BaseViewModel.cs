using System;
using System.Collections.Generic;

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

        #endregion
    }
}