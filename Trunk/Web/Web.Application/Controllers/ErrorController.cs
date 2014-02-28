using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;

using SportsWebPt.Common.Logging;

namespace SportsWebPt.Platform.Web.Application
{
    public class ErrorController : BaseController
    {
        #region Fields

        private ILog _logger = LogManager.GetCommonLogger();

        #endregion

        [GET("Error/NotFound", IsAbsoluteUrl = true)]
        public ActionResult NotFound()
        {
            _logger.Info("Page Not Found Returned");
            return View("ErrorNotFound");
        }

        [GET("Error", IsAbsoluteUrl = true)]
        public ActionResult Index()
        {
            _logger.Error("500 Unhandled Returned", new ApplicationException("Unknow Server Error"));
            Response.StatusCode = 200;  
            return View("Unhandled");
        }
    }
}
