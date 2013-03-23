using System.Web.Mvc;

using AttributeRouting;
using AttributeRouting.Web.Mvc;

namespace SportsWebPt.Platform.Web.Application
{
    [RouteArea("Examine")]
    public class SelfExamController : BaseController
    {

        [GET("Examine", IsAbsoluteUrl = true)]
        public ActionResult Index()
        {
            return View(new SelfExamViewModel());
        }

    }
}
