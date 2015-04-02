using System;
using System.Collections.Generic;
using System.Linq;
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
                {
                    serviceAccount = _userManagementService.CreateServiceAccount(User.GetSubjectId());
                    ((ClaimsIdentity)principle.Identity).AddClaim(new Claim("service_account", serviceAccount));
                }
                else
                {
                    var serviceUser = _userManagementService.GetServiceUser(serviceAccount);
                    user.plans = serviceUser.plans;
                    user.videos = serviceUser.exercises;
                    user.injuries = serviceUser.injuries;
                    user.exercises = serviceUser.exercises;
                }

                user.emailAddress = principle.FindFirst("email") == null ? String.Empty : principle.FindFirst("email").Value;
                //TODO: having issues mapping roles... First time auth after signin, Principal has Bearer and JWT identities that seem to use
                //the role InboundClaimTypeMap correctly, when I do a refresh on the page, principal only has bearer identity
                //and even though the roles are in the identity, they do not map because they are missing the xml namespace
                user.isAdmin = principle.HasClaim("role", "admin");
                user.isTherapist = principle.HasClaim("role","therapist");
                user.isClinicManager = principle.HasClaim("role", "manager");
                user.id = serviceAccount;

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
        [Authorize]
        [Route("data/users/favorites")]
        public Favorite AddFavorites(Favorite favorite)
        {
            _userManagementService.AddFavorite(favorite, User.GetServiceAccount());
            return favorite;
        }

        [HttpGet]
        [Authorize]
        [Route("data/patients/cases")]
        public IEnumerable<Case> GetPatientCases( String state)
        {
            var cases = _userManagementService.GetCases(User.GetServiceAccount(), state);

            foreach (var user in _userManagementService.GetUserDetailsByExternalAccounts(cases.Select(s => s.therapistId).Distinct()))
            {
                cases.Where(p => p.patientId == User.GetServiceAccount()).ForEach(f =>
                {
                    f.therapistEmail = user.emailAddress;
                    f.therapistName = user.fullName;
                });
            }

            return cases;
        }

        [HttpGet]
        [Authorize]
        [Route("data/patients/snapshot")]
        public PatientSnapshot GetPatientSnapshot()
        {
            return _userManagementService.GetPatientSnapshot(User.GetServiceAccount());
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

            var serviceAccount = user.FindFirst("service_account") == null
                ? String.Empty
                : user.FindFirst("service_account").Value;

            if (String.IsNullOrEmpty(serviceAccount))
            {
                //TODO: this is when initial login happens and service account has not been set in the claims for the principal... should 
                //be able to take this out once I fix auto login with update on IDSvr 
                var userManagementService = new UserManagementService(WebPlatformConfigSettings.Instance.ServiceStackClientSettings);
                serviceAccount = userManagementService.GetUser(user.GetSubjectId()).id;
            }

            return serviceAccount;
        }
    }

}
