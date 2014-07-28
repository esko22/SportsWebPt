using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting;
using AttributeRouting.Web.Mvc;
using SportsWebPt.Common.Utilities;
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
    }
}