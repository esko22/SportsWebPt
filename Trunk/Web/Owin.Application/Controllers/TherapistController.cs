using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application.Controllers
{
    [Authorize]
    public class TherapistController : ApiController
    {
        #region Fields

        private readonly ITherapistService _therapistService;
        private readonly IUserManagementService _userManagementService;

        #endregion

        #region Construction

        public TherapistController(ITherapistService therapistService, IUserManagementService userManagementService)
        {
            Check.Argument.IsNotNull(therapistService, "Therapist Service");
            Check.Argument.IsNotNull(userManagementService, "User Management Service");
            _therapistService = therapistService;
            _userManagementService = userManagementService;
        }

        #endregion

        [HttpGet]
        [Route("data/therapists/exercises")]
        public IEnumerable<BriefExercise> GetExercises()
        {
            return _therapistService.GetExercises(User.GetServiceAccount());
        }

        [HttpGet]
        [Route("data/therapists/plans")]
        public IEnumerable<BriefPlan> GetPlans()
        {
            return _therapistService.GetPlans(User.GetServiceAccount());
        }

        [HttpGet]
        [Route("data/therapists")]
        public Therapist GetTherapistDetail()
        {
            return _therapistService.GetTherapistDetail(User.GetServiceAccount());
        }

        [HttpPut]
        [Route("data/therapists/sharedplans")]
        public Boolean UpdateTherapistSharedPlans(ClinicPlan[] sharedPlans)
        {
            return _therapistService.UpdateTherapistSharedPlans(User.GetServiceAccount(), sharedPlans);
        }

        [HttpPut]
        [Route("data/therapists/sharedexercises")]
        public Boolean UpdateTherapistSharedExercises(ClinicExercise[] sharedExercises)
        {
            return _therapistService.UpdateTherapistSharedExercises(User.GetServiceAccount(), sharedExercises);
        }

        [HttpGet]
        [Route("data/therapists/cases")]
        public IEnumerable<Case> GetTherapistCases(String state)
        {
            var cases = _therapistService.GetCases(User.GetServiceAccount(), state);

            foreach (var user in _userManagementService.GetUserDetailsByExternalAccounts(cases.Select(s => s.patientId).Distinct()))
            {
                cases.Where(p => p.patientId == user.serviceAccount).ForEach(f =>
                {
                    f.patientEmail = user.emailAddress;
                    f.patientName = user.fullName;
                });
            }

            return cases;
        }

    }
}