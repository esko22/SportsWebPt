﻿using System.Web;
using System.Web.Mvc;

namespace SportsWebPt.Platform.Web.Application
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AjaxHandleErrorAttribute { View = "Unhandled" });
        }
    }
}