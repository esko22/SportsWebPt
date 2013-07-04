using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AttributeRouting;
using AttributeRouting.Web.Mvc;
using SportsWebPt.Platform.Web.Application;

namespace SportsWebPt.Platform.Web.Admin
{
    [RouteArea("Admin")]
    public class IndexController : BaseController
    {
        
        [GET("Admin", IsAbsoluteUrl = true)]
        public ActionResult Index()
        {
            var viewModel = CreateViewModel<AdminIndexViewModel>();

            return View(viewModel);
        }

    }
}
