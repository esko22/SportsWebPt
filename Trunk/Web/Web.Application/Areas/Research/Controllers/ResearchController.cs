using System;
using System.Linq;
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

        [GET("Research/plans/{Id}", IsAbsoluteUrl = true)]
        public ActionResult GetPlan(string Id)
        {
            var planId = 0;
            if (!String.IsNullOrEmpty(Id))
                int.TryParse(Id, out planId);

            var plan = _researchService.GetPlan(planId);

            return Json(plan, JsonRequestBehavior.AllowGet);
        }


        [GET("Research", IsAbsoluteUrl = true)]
        public ActionResult Index()
        {
            var viewModel = CreateViewModel<ResearchViewModel>();

            return View(viewModel);
        }

        [GET("Research/exercise/{pageName}", IsAbsoluteUrl = true)]
        public ActionResult Exercise(String pageName)
        {
            Check.Argument.IsNotNullOrEmpty(pageName, "pageName");

            var viewModel = CreateViewModel<ResearchExerciseViewModel>();
            var exercise = _researchService.GetExerciseByPageName(pageName);

            //TODO: throw page not found
            Check.Argument.IsNotNull(exercise, "Exercise");

            viewModel.Tags = exercise.tags;
            viewModel.Title = exercise.name;
            viewModel.Exercise = exercise;

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
