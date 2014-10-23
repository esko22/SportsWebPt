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

                var user = _userManagementService.GetUser(principle.FindFirst("sub").Value);


                //if user does not have a claim of the key in the service db create new user
                //then I would add a claim of the services db id for user
                //set flag of user to accountLinked

                //in situation to validate
                //after successful validation on web side
                //I get user for that registration row, I can check if account is linked
                //if account is not linked, I add claim for service db id for user and set flag

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
