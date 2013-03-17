using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;
using SportsWebPt.Platform.Web.Application;

namespace SportsWebPt.Platform.Web.Examine
{
    [RouteArea("Examine")]
    public class SelfExamController : Controller
    {

        [GET("Examine", IsAbsoluteUrl = true)]
        public ActionResult Index()
        {
            return View(new SelfExamViewModel());
        }

    }
}
