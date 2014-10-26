using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using AutoMapper;
using SportsWebPt.Common.Utilities;
using System.Web.Http;

using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application
{
    public class UserController : ApiController
    {
        #region Fields

        private readonly IUserManagementService _userManagementService;
        private readonly IClinicService _clinicService;

        #endregion

        #region Construction

        public UserController(IUserManagementService userManagementService, IClinicService clinicService)
        {
            Check.Argument.IsNotNull(userManagementService, "User Management Service");
            Check.Argument.IsNotNull(userManagementService, "User Management Service");
            _userManagementService = userManagementService;
            _clinicService = clinicService;
        }

        #endregion


        [HttpGet]
        [Route("data/users/current")]
        public User GetUser()
        {

            if (User.Identity.IsAuthenticated)
            {
                var principle = (ClaimsPrincipal)User;
                var subjectId = principle.FindFirst("sub") == null ? String.Empty : principle.FindFirst("sub").Value;
                var user = Mapper.Map<User>(_userManagementService.GetServiceUser(subjectId));

                if (user == null)
                {
                    user = new User()
                    {
                        emailAddress = principle.FindFirst("email") == null ? String.Empty : principle.FindFirst("email").Value,
                        isAdmin = true,
                        isClinicManager = true,
                        isTherapist = true
                    };
                }
                else
                {
                    user.emailAddress = principle.FindFirst("email") == null ? String.Empty : principle.FindFirst("email").Value;
                    user.isAdmin = true;
                    user.isTherapist = true;
                    user.isClinicManager = true;
                }


                return user;
                //if user does not have a claim of the key in the service db create new user
                //then I would add a claim of the services db id for user
                //set flag of user to accountLinked

                //in situation to validate
                //after successful validation on web side
                //I get user for that registration row, I can check if account is linked
                //if account is not linked, I add claim for service db id for user and set flag

            }

            return null;
        }

        [HttpPost]
        [Authorize]
        [Route("data/users/validate")]
        public Boolean UserExistsByEmail([FromBody]String emailAddress)
        {
            return _userManagementService.ValidateUserByEmail(emailAddress);
        }

        [HttpPost]
        [Route("data/users/")]
        public int CreateUser(User user)
        {
            return _userManagementService.AddUser(user);
        }

        [HttpPut]
        [Route("data/users/current")]
        public int UpdateUser(User user)
        {
            return _userManagementService.AddUser(user);
        }

        [HttpPost]
        [Route("data/users/current/favorites")]
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

        [HttpGet]
        [Authorize]
        [Route("data/users/current/managedclinics")]
        public IEnumerable<Clinic> GetManagedClinics()
        {
            return _clinicService.GetManagedClinics((User).GetSubjectId());
        }
    }

    public static class ClaimsPrincipalExtensions
    {
        //TODO: move this out to a more common location for visibility
        public static String GetSubjectId(this IPrincipal principal)
        {
            var user = (ClaimsPrincipal) principal;
            return user.FindFirst("sub") == null ? String.Empty : user.FindFirst("sub").Value;
        }
    }

}
