using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SportsWebPt.Common.Utilities;
using http = System.Web.Http;

using AttributeRouting;
using AttributeRouting.Web.Mvc;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application
{
    [RouteArea]
    public class UserController : BaseController
    {
        #region Fields

        private IUserManagementService _userManagementService;

        #endregion

        #region Construction

        public UserController(IUserManagementService userManagementService)
        {
            Check.Argument.IsNotNull(userManagementService, "User Management Service");
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
            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [PUT("Users/{id}")]
        public ActionResult UpdateUser(User user)
        {
            var response = _userManagementService.AddUser(user);
            return Json(response, JsonRequestBehavior.DenyGet);
        }

        //[DELETE("Users/{id}")]
        //public ActionResult DeleteUser(int id)
        //{
        //    var response = _userManagementService.AddUser(user);
        //    return Json(response, JsonRequestBehavior.AllowGet);
        //}

    }
}
