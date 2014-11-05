using System;
using System.Web.Http;

using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application
{
    public class RegistrationController : ApiController
    {

        #region Fields

        private IUserManagementService _userManagementService;

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
                serviceAccount = _userManagementService.GetUser(User.GetSubjectId()).hash;
 
            var clinicPatient = _userManagementService.ValidatePatientRegistration(registrationDetails.emailAddress,
                registrationDetails.pin, serviceAccount);

            return clinicPatient;
        }

        [HttpPost]
        [Route("data/registration/therapist")]
        public ClinicTherapist ValidateTherapistRegistration([FromBody] RegistrationDetails registrationDetails)
        {
            Check.Argument.IsNotNullOrEmpty(registrationDetails.emailAddress, "Email Address");
            Check.Argument.IsNotNullOrEmpty(registrationDetails.pin, "Pin");

            //assuming this has been created already... should be but is not in principal yet, only in db
            var serviceAccount = User.GetServiceAccount();
            if (String.IsNullOrEmpty(serviceAccount))
                serviceAccount = _userManagementService.GetUser(User.GetSubjectId()).hash;

            var clinicTherapist = _userManagementService.ValidateTherapistRegistration(registrationDetails.emailAddress,
                registrationDetails.pin, serviceAccount);

            return clinicTherapist;
        }

        

        #endregion
    }

    public class RegistrationDetails
    {
        public String emailAddress { get; set; }

        public String pin { get; set; }
    }
}