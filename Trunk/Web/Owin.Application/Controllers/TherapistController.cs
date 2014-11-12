using System;
using System.Collections.Generic;
using System.Web.Http;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application.Controllers
{
    public class TherapistController : ApiController
    {
        #region Fields

        private ITherapistService _therapistService;

        #endregion

        #region Construction

        public TherapistController(ITherapistService therapistService)
        {
            Check.Argument.IsNotNull(therapistService, "Therapist Service");
            _therapistService = therapistService;
        }

        #endregion

        [HttpGet]
        [Route("data/therapists/{id}/exercises")]
        public IEnumerable<GridExercise> GetExercises(String id)
        {
            return _therapistService.GetExercises(id);
        }

        [HttpGet]
        [Route("data/therapists/{id}/plans")]
        public IEnumerable<GridPlan> GetPlans(String id)
        {
            return _therapistService.GetPlans(id);
        }

        [HttpGet]
        [Route("data/therapists/{id}")]
        public Therapist GetTherapistDetail(String id)
        {
            return _therapistService.GetTherapistDetail(id);
        }

        [HttpGet]
        [Route("data/therapists/{id}/sharedplans")]
        public IEnumerable<TherapistSharedPlan> GetTherapistSharedPlans(int id, int? planId)
        {
            return _therapistService.GetTherapistSharedPlans(id, planId);
        }

        [HttpPut]
        [Route("data/therapists/{id}/sharedplans")]
        public Boolean UpdateTherapistSharedPlans(int id, TherapistSharedPlan[] sharedPlans)
        {
            return _therapistService.UpdateTherapistSharedPlans(id, sharedPlans);
        }

        [HttpPut]
        [Route("data/therapists/{id}/sharedexercises")]
        public Boolean UpdateTherapistSharedExercises(int id, TherapistSharedExercise[] sharedExercises)
        {
            return _therapistService.UpdateTherapistSharedExercises(id, sharedExercises);
        }


        [HttpGet]
        [Route("data/therapists/{id}/sharedexercises")]
        public IEnumerable<TherapistSharedExercise> GetTherapistSharedExercises(int id, int? exerciseId)
        {
            return _therapistService.GetTherapistSharedExercises(id, exerciseId);
        }

        [HttpGet]
        [Route("data/therapists/{id}/episodes")]
        public IEnumerable<Episode> GetTherapistEpisodes(String id, String state)
        {
            return _therapistService.GetEpisodes(id, state);
        }

    }
}