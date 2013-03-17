using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;

namespace SportsWebPt.Platform.Web.Application.Areas.Dashboard.Controllers
{
    [RouteArea("Dashboard")]
    [Authorize]
    public class UserDashboardController : Controller
    {
        [GET("Dashboard", IsAbsoluteUrl = true)]
        public ActionResult Index()
        {
            return View(new UserDashboardViewModel());
        }
    }
}
