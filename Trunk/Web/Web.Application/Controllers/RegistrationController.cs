using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application
{
    [RouteArea]
    public class RegistrationController : BaseController
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

        [GET("/data/registration/patient")]
        public ActionResult ValidatePatientRegistration(String emailAddress, String pin)
        {
            Check.Argument.IsNotNullOrEmpty(emailAddress, "Email Address");
            Check.Argument.IsNotNullOrEmpty(pin, "Pin");

            return Json(_userManagementService.ValidatePatientRegistration(emailAddress, pin),
                JsonRequestBehavior.AllowGet);
        }

        [GET("/data/registration/therapist")]
        public ActionResult ValidateTherapistRegistration(String emailAddress, String pin)
        {
            Check.Argument.IsNotNullOrEmpty(emailAddress, "Email Address");
            Check.Argument.IsNotNullOrEmpty(pin, "Pin");

            return Json(_userManagementService.ValidateTherapistRegistration(emailAddress, pin),
                JsonRequestBehavior.AllowGet);
        }

        

        #endregion
    }
}