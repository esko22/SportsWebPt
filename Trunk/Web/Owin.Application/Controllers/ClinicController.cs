using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;

using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application.Controllers
{
    public class ClinicController : ApiController
    {
        #region Fields

        private readonly IClinicService _clinicService;
        private readonly IUserManagementService _userManagementService;

        #endregion

        #region Construction

        public ClinicController(IClinicService clinicService, IUserManagementService userManagementService)
        {
            Check.Argument.IsNotNull(clinicService, "Clinic Service");
            Check.Argument.IsNotNull(userManagementService, "User Management Service");
            _clinicService = clinicService;
            _userManagementService = userManagementService;
        }

        #endregion

        #region Methods

        [HttpGet]
        [Route("data/clinics/{clinicId}")]
        public Clinic GetClinic(int clinicId)
        {
            return _clinicService.GetClinic(clinicId);
        }


        [HttpGet]
        [Route("data/clinics/{clinicId}/patients")]
        public IEnumerable<ClinicPatient> GetClinicPatients(int clinicId)
        {
            var clinicPatients = _clinicService.GetClinicPatients(clinicId);
            _userManagementService.SetUserDetailsByExternalAccounts(clinicPatients.Select(s => s.user));

            return clinicPatients;
        }

        [HttpPost]
        [Route("data/clinics/{clinicId}/patients")]
        public ClinicPatient AddClinicPatient(int clinicId, User user)
        {
            return _clinicService.AddPatientToClinic(clinicId,user);
        }

        [HttpGet]
        [Route("data/clinics/{clinicId}/therapists")]
        public IEnumerable<ClinicTherapist> GetClinicTherapists(int clinicId)
        {
            var clinicTherapists = _clinicService.GetClinicTherapists(clinicId);
            _userManagementService.SetUserDetailsByExternalAccounts(clinicTherapists.Select(s => s.therapist));

            return clinicTherapists;
        }

        [HttpPost]
        [Route("data/clinics/{clinicId}/therapists")]
        public ClinicTherapist AddClinicTherapist(int clinicId, User therapist)
        {
            return _clinicService.AddTherapistToClinic(clinicId,therapist);
        }


        #endregion
    }
}