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

        private IClinicService _clinicService;
        private IUserManagementService _userManagementService;

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
        public IEnumerable<User> GetClinicPatients(int clinicId)
        {
            var clinicPatients = _clinicService.GetClinicPatients(clinicId);
            var distinctExternalAccounts = clinicPatients.Select(s => s.externalAccountId).Distinct();

            foreach (var identityUser in _userManagementService.GetUsersByExternalAccountId(distinctExternalAccounts))
            {
                var clinicPatient =
                    clinicPatients.SingleOrDefault(s => s.externalAccountId.Equals(identityUser.ServiceAccount));
                if (clinicPatient != null)
                    clinicPatient.emailAddress = identityUser.Email;
            }

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
        public IEnumerable<Therapist> GetClinicTherapists(int clinicId)
        {
            return _clinicService.GetClinicTherapists(clinicId);
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