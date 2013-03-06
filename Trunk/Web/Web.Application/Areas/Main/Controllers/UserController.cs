using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AttributeRouting;
using AttributeRouting.Web.Mvc;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application
{
    [RouteArea]
    public class UserController : Controller
    {
        #region Fields

        private IUserManagementService _userManagementService;

        #endregion

        #region Construction

        public UserController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        #endregion


        [GET("User/{id}")]
        public ActionResult GetUser(int id)
        {
            var user = _userManagementService.GetUser(id);
            return Json(user, JsonRequestBehavior.AllowGet);
        }

    }
}
