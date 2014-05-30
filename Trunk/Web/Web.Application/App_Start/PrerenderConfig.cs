using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Prerender_asp_mvc;

namespace SportsWebPt.Platform.Web.Application
{
    public static class PrerenderConfig
    {
        private static bool _isStarting;

        public static void PreStart()
        {
            if (!_isStarting)
            {
                _isStarting = true;

                DynamicModuleUtility.RegisterModule(typeof(PrerenderModule));
            }
        }
    }
}