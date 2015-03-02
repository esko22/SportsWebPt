using System;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

using SportsWebPt.Common.Logging;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application
{
    public class RegistrationController : ApiController
    {

        #region Fields

        private IUserManagementService _userManagementService;
        private ILog _logger = LogManager.GetCommonLogger();

        #endregion

        #region Construction

        public RegistrationController(IUserManagementService userManagementService)
        {
            Check.Argument.IsNotNull(userManagementService, "UserManagementService");
            _userManagementService = userManagementService;
        }

        #endregion

        #region Methods

        [HttpPost]
        [Authorize]
        [Route("data/registration/patient")]
        public ClinicPatient ValidatePatientRegistration([FromBody] RegistrationDetails registrationDetails)
        {
            Check.Argument.IsNotNullOrEmpty(registrationDetails.emailAddress, "Email Address");
            Check.Argument.IsNotNullOrEmpty(registrationDetails.pin, "Pin");

            //assuming this has been created already... should be but is not in principal yet, only in db
            var serviceAccount = User.GetServiceAccount();
            if (String.IsNullOrEmpty(serviceAccount))
                serviceAccount = _userManagementService.GetUser(User.GetSubjectId()).id;

            _logger.Info(String.Format("Validation Request For {0} - {1}", registrationDetails.emailAddress, serviceAccount));

            var clinicPatient = _userManagementService.ValidatePatientRegistration(registrationDetails.emailAddress,
                registrationDetails.pin, serviceAccount);

            if(clinicPatient == null)
                _logger.Info("Failed Registration Attempt");

            return clinicPatient ?? new ClinicPatient();
        }

        [HttpPost]
        [Authorize]
        [Route("data/registration/therapist")]
        public ClinicTherapist ValidateTherapistRegistration([FromBody] RegistrationDetails registrationDetails)
        {
            Check.Argument.IsNotNullOrEmpty(registrationDetails.emailAddress, "Email Address");
            Check.Argument.IsNotNullOrEmpty(registrationDetails.pin, "Pin");

            //assuming this has been created already... should be but is not in principal yet, only in db
            var serviceAccount = User.GetServiceAccount();
            if (String.IsNullOrEmpty(serviceAccount))
                serviceAccount = _userManagementService.GetUser(User.GetSubjectId()).id;

            if(!((ClaimsPrincipal)User).HasClaim("role","therapist"))
                UserManagementService.UserAccountServiceFactory().AddClaim(new Guid(User.GetSubjectId()), "role", "therapist");

            var clinicTherapist = _userManagementService.ValidateTherapistRegistration(registrationDetails.emailAddress,
                registrationDetails.pin, serviceAccount);

            Request.GetOwinContext().Authentication.SignOut(new[] { "Bearer"});

            return clinicTherapist ?? new ClinicTherapist();
        }

        

        #endregion
    }

    public class RegistrationDetails
    {
        public String emailAddress { get; set; }

        public String pin { get; set; }
    }
}