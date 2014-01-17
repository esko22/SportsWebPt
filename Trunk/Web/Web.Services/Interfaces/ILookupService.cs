using System;
using System.Collections.Generic;

using SportsWebPt.Platform.Web.Core;

namespace SportsWebPt.Platform.Web.Services
{
    public interface ILookupService
    {
        #region Methods

        IEnumerable<Filter> GetSignFilters();

        IEnumerable<Filter> GetCauseFilters();

        #endregion
    }
}
