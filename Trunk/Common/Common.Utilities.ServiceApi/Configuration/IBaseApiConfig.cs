using System;
using System.Collections.Generic;

namespace SportsWebPt.Common.Utilities.ServiceApi
{
    public interface IBaseApiConfig
    {
        String ApiUrl { get; }

        Uri ApiUriWithVersion { get; }

        String ApiVersion { get; }


    }
}
