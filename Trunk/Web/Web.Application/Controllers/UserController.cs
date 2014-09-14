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


        [GET("data/users/{id}")]
        public ActionResult GetUser(String id)
        {
            var user = _userManagementService.GetUser(id);
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        [POST("data/users/")]
        public ActionResult CreateUser(User user)
        {
            var response = _userManagementService.AddUser(user);
            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [PUT("data/users/{id}")]
        public ActionResult UpdateUser(User user)
        {
            var response = _userManagementService.AddUser(user);
            return Json(response, JsonRequestBehavior.DenyGet);
        }

        [POST("data/users/favorites")]
        public ActionResult AddFavorites(Favorite favorite)
        {
            if (User.Identity.IsAuthenticated)
                _userManagementService.AddFavorite(favorite, Convert.ToInt32(User.Identity.Name));

            return Json(favorite, JsonRequestBehavior.DenyGet);
        }

        [GET("data/patients/{id}/episodes", IsAbsoluteUrl = true)]
        public ActionResult GetPatientEpisodes(int id, String state)
        {
            return Json(_userManagementService.GetEpisodes(id, state), JsonRequestBehavior.AllowGet);
        }

        //[DELETE("Users/{id}")]
        //public ActionResult DeleteUser(int id)
        //{
        //    var response = _userManagementService.AddUser(user);
        //    return Json(response, JsonRequestBehavior.AllowGet);
        //}

    }
}
