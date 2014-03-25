using System.Web.Mvc;

using AttributeRouting;
using AttributeRouting.Web.Mvc;

namespace SportsWebPt.Platform.Web.Application
{
    [RouteArea]
    public class IndexController : BaseController
    {

        [GET("",IsAbsoluteUrl = true)]
        public ActionResult Index()
        {
            return View(CreateViewModel<IndexViewModel>());
        }

    }
}
