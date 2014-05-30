using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using AttributeRouting;
using AttributeRouting.Web.Mvc;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

using YelpSharp;

namespace SportsWebPt.Platform.Web.Application
{
    [RouteArea("Research")]
    public class ResearchController : BaseController
    {

        #region Fields

        private readonly IResearchService _researchService; 

        #endregion

        #region Construction

        public ResearchController(IResearchService researchService)
        {
            Check.Argument.IsNotNull(researchService, "Examine Service");
            _researchService = researchService;
        }

        #endregion

        #region Methods

        [GET("data/research/injury/detail/{pageName}", IsAbsoluteUrl = true)]
        public ActionResult InjuryDetail(String pageName)
        {
            Check.Argument.IsNotNullOrEmpty(pageName, "pageName");

            var injury = _researchService.GetInjuryByPageName(pageName);

            return Json(injury, JsonRequestBehavior.AllowGet);
        }

        [GET("data/research/plan/detail/{pageName}", IsAbsoluteUrl = true)]
        public ActionResult PlanDetail(String pageName)
        {
            Check.Argument.IsNotNullOrEmpty(pageName, "pageName");

            var plan = _researchService.GetPlanByPageName(pageName);

            return Json(plan, JsonRequestBehavior.AllowGet);
        }

        [GET("data/research/exercise/detail/{pageName}", IsAbsoluteUrl = true)]
        public ActionResult ExerciseDetail(String pageName)
        {
            Check.Argument.IsNotNullOrEmpty(pageName, "pageName");

            var exercise = _researchService.GetExerciseByPageName(pageName);

            return Json(exercise, JsonRequestBehavior.AllowGet);
        }

        [GET("data/research/bodyparts", IsAbsoluteUrl = true)]
        public ActionResult GetBodyParts()
        {
            var areaComponents = _researchService.GetBodyParts();

            return Json(areaComponents, JsonRequestBehavior.AllowGet);
        }

        [GET("data/research/bodyregions", IsAbsoluteUrl = true)]
        public ActionResult GetBodyRegions()
        {
            var areaComponents = _researchService.GetBodyRegions();

            return Json(areaComponents, JsonRequestBehavior.AllowGet);
        }

        [GET("data/research/signs", IsAbsoluteUrl = true)]
        public ActionResult GetSigns()
        {
            var signs = _researchService.GetSigns();

            return Json(signs, JsonRequestBehavior.AllowGet);
        }

        [GET("data/research/symptoms", IsAbsoluteUrl = true)]
        public ActionResult GetSymptoms()
        {
            throw new NotImplementedException();
        }

        [GET("data/research/exercises", IsAbsoluteUrl = true)]
        public ActionResult GetExercises()
        {
            var exercises = _researchService.GetExercises();

            return Json(exercises, JsonRequestBehavior.AllowGet);
        }

        [GET("data/research/plans", IsAbsoluteUrl = true)]
        public ActionResult GetPlans()
        {
            var plans = _researchService.GetPlans();

            return Json(plans, JsonRequestBehavior.AllowGet);
        }

        [GET("data/research/injuries", IsAbsoluteUrl = true)]
        public ActionResult GetInjuries()
        {
            var injuries = _researchService.GetInjuries();

            return Json(injuries, JsonRequestBehavior.AllowGet);
        }

        [GET("data/exercises/brief", IsAbsoluteUrl = true)]
        public ActionResult GetBriefExercises()
        {
            var exercises = _researchService.GetBriefExercises();

            return Json(exercises, JsonRequestBehavior.AllowGet);
        }

        [GET("data/plans/brief", IsAbsoluteUrl = true)]
        public ActionResult GetBriefPlans()
        {
            var plans = _researchService.GetBriefPlans();

            return Json(plans, JsonRequestBehavior.AllowGet);
        }

        [GET("data/injuries/brief", IsAbsoluteUrl = true)]
        public ActionResult GetBriefInjuries()
        {
            var injuries = _researchService.GetBriefInjuries();

            return Json(injuries, JsonRequestBehavior.AllowGet);
        }

        [GET("data/research/locate/{zipcode}", IsAbsoluteUrl = true)]
        public ActionResult GetTherapyByLocation(String zipcode)
        {
            var yelpProxy = new Yelp(WebPlatformConfigSettings.Instance.YelpOptions);
            var results = yelpProxy.Search(WebPlatformConfigSettings.Instance.YelpSearchTerm, zipcode).Result;

            return Json(results.businesses,JsonRequestBehavior.AllowGet);
        }

        [GET("data/research/equipment", IsAbsoluteUrl = true)]
        public ActionResult GetEquipment()
        {
            var equipment = _researchService.GetEquipment();

            return Json(equipment, JsonRequestBehavior.AllowGet);
        }

        [GET("data/research/equipment/brief", IsAbsoluteUrl = true)]
        public ActionResult GetBriefEquipment()
        {
            var equipment = _researchService.GetBriefEquipment();

            return Json(equipment, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}
