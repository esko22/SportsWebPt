using System;
using System.Collections.Generic;
using System.Web.Http;

using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application.Controllers
{
    [Authorize]
    public class CaseController : ApiController
    {

        #region Fields

        private readonly ICaseService _caseService;
        private readonly IUserManagementService _userManagementService;
        #endregion

        #region Construction

        public CaseController(ICaseService caseService, IUserManagementService userManagementService)
        {
            Check.Argument.IsNotNull(caseService, "Case Service");
            Check.Argument.IsNotNull(userManagementService, "User Management Service");

            _caseService = caseService;
            _userManagementService = userManagementService;
        }

        #endregion

        #region Methods
        
        [HttpGet]
        [Route("data/cases/{caseId}")]
        public Case GetCase(Int64 caseId)
        {
            var caseInstance = _caseService.GetCase(caseId);
            var user = _userManagementService.GetUserByServiceAccountId(caseInstance.patientId);
            var therapist = _userManagementService.GetUserByServiceAccountId(caseInstance.therapistId);
            if(user != null)
              caseInstance.patientEmail = user.Email;
            if (therapist != null)
                caseInstance.therapistEmail = therapist.Email;

            return caseInstance;
        }

        [HttpGet]
        [Route("data/cases/{caseId}/sessions")]
        public IEnumerable<Session> GetCaseSessions(Int64 caseId)
        {
            return _caseService.GetCaseSessions(caseId);
        }

        [HttpPost]
        [Route("data/cases")]
        public Case AddCase(Case caseInstance)
        {
            caseInstance.therapistId = User.GetServiceAccount();
            var response = _caseService.AddCase(caseInstance);
            caseInstance.id = response;

            return caseInstance;
        }

        #endregion


    }
}