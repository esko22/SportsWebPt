using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;
using SportsWebPt.Platform.Web.Admin;

namespace SportsWebPt.Platform.Web.Application.Areas.Main.Controllers
{
    public class ErrorController : BaseController
    {
        [GET("Error/NotFound", IsAbsoluteUrl = true)]
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;  //you may want to set this to 200
            return View("_errorNotFound");
        }

        [GET("Error", IsAbsoluteUrl = true)]
        public ActionResult Index()
        {
            Response.StatusCode = 404;  //you may want to set this to 200
            return View("_errorNotFound");
        }
    }
}
