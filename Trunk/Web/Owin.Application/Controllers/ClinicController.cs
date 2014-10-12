using System;
using System.Collections.Generic;
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

        #endregion

        #region Construction

        public ClinicController(IClinicService clinicService)
        {
            Check.Argument.IsNotNull(clinicService, "Clinic Service");
            _clinicService = clinicService;
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
        [Route("data/clinics/managers/{clinicManagerId}")]
        public IEnumerable<Clinic> GetManagedClinics(int clinicManagerId)
        {
            return _clinicService.GetManagedClinics(clinicManagerId);
        }

        [HttpGet]
        [Route("data/clinics/{clinicId}/patients")]
        public IEnumerable<User> GetClinicPatients(int clinicId)
        {
            return _clinicService.GetClinicPatients(clinicId);
        }

        [HttpPost]
        [Route("data/clinics/{clinicId}/patients")]
        public User AddClinicPatient(int clinicId, User user)
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
        public User AddClinicTherapist(int clinicId, User therapist)
        {
            return _clinicService.AddTherapistToClinic(clinicId,therapist);
        }


        #endregion
    }
}