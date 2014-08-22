using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application.Controllers
{
    [RouteArea]
    public class TherapistController : BaseController
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

        [GET("data/therapists/{id}/exercises", IsAbsoluteUrl = true)]
        public ActionResult GetExercises(String id)
        {
            var exercises = _therapistService.GetExercises(id);

            return Json(exercises, JsonRequestBehavior.AllowGet);
        }

        [GET("data/therapists/{id}/plans", IsAbsoluteUrl = true)]
        public ActionResult GetPlans(String id)
        {
            var plans = _therapistService.GetPlans(id);

            return Json(plans, JsonRequestBehavior.AllowGet);
        }

        [GET("data/therapists/{id}", IsAbsoluteUrl = true)]
        public ActionResult GetTherapistDetail(int id)
        {
            return Json(_therapistService.GetTherapistDetail(id), JsonRequestBehavior.AllowGet);
        }

        [GET("data/therapists/{id}/sharedplans", IsAbsoluteUrl = true)]
        public ActionResult GetTherapistSharedPlans(int id, int? planId)
        {
            return Json(_therapistService.GetTherapistSharedPlans(id, planId), JsonRequestBehavior.AllowGet);
        }

        [PUT("data/therapists/{id}/sharedplans", IsAbsoluteUrl = true)]
        public ActionResult UpdateTherapistSharedPlans(int id, TherapistSharedPlan[] sharedPlans)
        {
            return Json(_therapistService.UpdateTherapistSharedPlans(id, sharedPlans), JsonRequestBehavior.DenyGet);
        }

        [PUT("data/therapists/{id}/sharedexercises", IsAbsoluteUrl = true)]
        public ActionResult UpdateTherapistSharedExercises(int id, TherapistSharedExercise[] sharedExercises)
        {
            return Json(_therapistService.UpdateTherapistSharedExercises(id, sharedExercises), JsonRequestBehavior.DenyGet);
        }


        [GET("data/therapists/{id}/sharedexercises", IsAbsoluteUrl = true)]
        public ActionResult GetTherapistSharedExercises(int id, int? exerciseId)
        {
            return Json(_therapistService.GetTherapistSharedExercises(id, exerciseId), JsonRequestBehavior.AllowGet);
        }

        [GET("data/therapists/{id}/episodes", IsAbsoluteUrl = true)]
        public ActionResult GetTherapistEpisodes(int id, String state)
        {
            return Json(_therapistService.GetEpisodes(id, state), JsonRequestBehavior.AllowGet);
        }

    }
}