using System;
using System.Collections.Generic;
using System.Security.Claims;
using SportsWebPt.Common.Utilities;
using System.Web.Http;

using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application
{
    public class UserController : ApiController
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


        [HttpGet]
        [Route("data/users/{id}")]
        public User GetUser(String id)
        {
            var principle = User as ClaimsPrincipal;

            if (principle != null)
            {
                var email = principle.FindFirst("email") == null ? "Unknown" : principle.FindFirst("email").Value;

                return new User()
                {
                    emailAddress = email,
                    isAdmin = true,
                    isClinicManager = true,
                    isTherapist = true
                };
            }

            return null;

            //return _userManagementService.GetUser(id);
        }

        [HttpPost]
        [Route("data/users/")]
        public int CreateUser(User user)
        {
            return _userManagementService.AddUser(user);
        }

        [HttpPut]
        [Route("data/users/{id}")]
        public int UpdateUser(User user)
        {
            return _userManagementService.AddUser(user);
        }

        [HttpPost]
        [Route("data/users/favorites")]
        public Favorite AddFavorites(Favorite favorite)
        {
            if (User.Identity.IsAuthenticated)
                _userManagementService.AddFavorite(favorite, Convert.ToInt32(User.Identity.Name));

            return favorite;
        }

        [HttpGet]
        [Route("data/patients/{id}/episodes")]
        public IEnumerable<Episode> GetPatientEpisodes(int id, String state)
        {
            return _userManagementService.GetEpisodes(id, state);
        }



    }
}
