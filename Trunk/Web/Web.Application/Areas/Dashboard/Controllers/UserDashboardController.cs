using System.Web.Mvc;

using AttributeRouting;
using AttributeRouting.Web.Mvc;

namespace SportsWebPt.Platform.Web.Application
{
    [Authorize]
    [RouteArea("Dashboard")]
    public class UserDashboardController : BaseController
    {
        [GET("Dashboard", IsAbsoluteUrl = true)]
        public ActionResult Index()
        {
            return View(CreateViewModel<UserDashboardViewModel>());
        }
    }
}
