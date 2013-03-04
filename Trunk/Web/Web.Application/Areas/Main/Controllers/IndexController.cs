using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;

namespace SportsWebPt.Platform.Web.Application
{
    [RouteArea("Main")]
    public class IndexController : Controller
    {
        [GET("",IsAbsoluteUrl = true)]
        public ActionResult Index()
        {
            return View(new IndexViewModel());
        }

    }
}
