using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using http = System.Web.Http;

using AttributeRouting;
using AttributeRouting.Web.Mvc;
using SportsWebPt.Platform.Web.Core;
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


        [GET("Users/{id}")]
        public ActionResult GetUser(int id)
        {
            var user = _userManagementService.GetUser(id);
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        [POST("Users/")]
        public ActionResult CreateUser(User user)
        {
            var response = _userManagementService.AddUser(user);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [PUT("Users/{id}")]
        public ActionResult UpdateUser(User user)
        {
            var response = _userManagementService.AddUser(user);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //[DELETE("Users/{id}")]
        //public ActionResult DeleteUser(int id)
        //{
        //    var response = _userManagementService.AddUser(user);
        //    return Json(response, JsonRequestBehavior.AllowGet);
        //}

    }
}
