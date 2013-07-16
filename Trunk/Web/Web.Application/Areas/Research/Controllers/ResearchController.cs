using System;
using System.Web.Mvc;

using AttributeRouting;
using AttributeRouting.Web.Mvc;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Application;
using SportsWebPt.Platform.Web.Services;


namespace SportsWebPt.Platform.Web.Research
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

        [GET("Research/workouts/{Id}", IsAbsoluteUrl = true)]
        public ActionResult GetWorkout(string Id)
        {
            var workoutId = 0;
            if (!String.IsNullOrEmpty(Id))
                int.TryParse(Id, out workoutId);

            var workout = _researchService.GetWorkout(workoutId);

            return Json(workout, JsonRequestBehavior.AllowGet);
        }


        [GET("Research", IsAbsoluteUrl = true)]
        public ActionResult Index()
        {
            var viewModel = CreateViewModel<ResearchViewModel>();

            return View(viewModel);
        }

        [GET("Research/bodyparts", IsAbsoluteUrl = true)]
        public ActionResult GetBodyParts()
        {
            var areaComponents = _researchService.GetBodyParts();

            return Json(areaComponents, JsonRequestBehavior.AllowGet);
        }

        [GET("Research/bodyregions", IsAbsoluteUrl = true)]
        public ActionResult GetBodyRegions()
        {
            var areaComponents = _researchService.GetBodyRegions();

            return Json(areaComponents, JsonRequestBehavior.AllowGet);
        }

        [GET("Research/symptoms", IsAbsoluteUrl = true)]
        public ActionResult GetSymptoms()
        {
            throw new NotImplementedException();
        }

        [GET("Research/exercises", IsAbsoluteUrl = true)]
        public ActionResult GetExercises()
        {
            var exercises = _researchService.GetExercises();

            return Json(exercises, JsonRequestBehavior.AllowGet);
        }


        #endregion

    }
}
