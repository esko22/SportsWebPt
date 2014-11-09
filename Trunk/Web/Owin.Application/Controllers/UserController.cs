using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using AutoMapper;
using Microsoft.Owin;
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
        [Authorize]
        [Route("data/users/current")]
        public User GetUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var principle = (ClaimsPrincipal)User;
                var serviceAccount = principle.GetServiceAccount();
                var user = new User();

                if (String.IsNullOrEmpty(serviceAccount))
                    serviceAccount = _userManagementService.CreateServiceAccount(User.GetSubjectId());

                user.emailAddress = principle.FindFirst("email") == null ? String.Empty : principle.FindFirst("email").Value;
                user.isAdmin = principle.IsInRole("admin");
                user.isTherapist = principle.IsInRole("therapist");
                user.isClinicManager = principle.IsInRole("manager");
                user.serviceAccount = serviceAccount;

                return user;
            }

            return new User();
        }

        [HttpPost]
        [Authorize]
        [Route("data/users/validate")]
        public Boolean UserExistsByEmail([FromBody]String emailAddress)
        {
            return _userManagementService.ValidateUserByEmail(emailAddress);
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
            return _clinicService.GetManagedClinics((User).GetServiceAccount());
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

        public static String GetServiceAccount(this IPrincipal principal)
        {
            var user = (ClaimsPrincipal)principal;
            return user.FindFirst("service_account") == null ? String.Empty : user.FindFirst("service_account").Value;
        }
    }

}
