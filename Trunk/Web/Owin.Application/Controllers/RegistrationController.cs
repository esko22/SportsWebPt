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

        [HttpGet]
        [Authorize]
        [Route("data/registration/patient")]
        public ClinicPatient ValidatePatientRegistration(String emailAddress, String pin)
        {
            Check.Argument.IsNotNullOrEmpty(emailAddress, "Email Address");
            Check.Argument.IsNotNullOrEmpty(pin, "Pin");

            return _userManagementService.ValidatePatientRegistration(emailAddress, pin, User.GetSubjectId());
        }

        [HttpGet]
        [Route("data/registration/therapist")]
        public ClinicTherapist ValidateTherapistRegistration(String emailAddress, String pin)
        {
            Check.Argument.IsNotNullOrEmpty(emailAddress, "Email Address");
            Check.Argument.IsNotNullOrEmpty(pin, "Pin");

            return _userManagementService.ValidateTherapistRegistration(emailAddress, pin);
        }

        

        #endregion
    }
}