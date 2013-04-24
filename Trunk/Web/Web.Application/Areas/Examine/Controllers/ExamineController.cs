using System;
using System.Web.Mvc;

using AttributeRouting;
using AttributeRouting.Web.Mvc;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application
{
    [RouteArea("Examine")]
    public class ExamineController : BaseController
    {

        #region Fields

        private IExamineService _examineService;

        #endregion

        #region Construction

        public ExamineController(IExamineService examineService)
        {
            Check.Argument.IsNotNull(examineService, "Examine Service");
            _examineService = examineService;
        }

        #endregion

        [GET("Examine", IsAbsoluteUrl = true)]
        public ActionResult Index()
        {
            var viewModel = CreateViewModel<ExamineViewModel>();
            viewModel.SymptomaticRegions = _examineService.GetSymptomaticRegions();

            return View(viewModel);
        }

        [GET("Examine/components", IsAbsoluteUrl = true)]
        public ActionResult GetAreaComponents(String areaId)
        {
            var skeletonAreaId = 0;
            if (!String.IsNullOrEmpty(areaId))
                int.TryParse(areaId, out skeletonAreaId);

            var areaComponents = _examineService.GetAreaComponents(skeletonAreaId);

            return Json(areaComponents, JsonRequestBehavior.AllowGet);
        }

        [GET("Examine/symptomaticregions", IsAbsoluteUrl = true)]
        public ActionResult GetSymptomaticRegions()
        {
            var symptomaticRegions = _examineService.GetSymptomaticRegions();

            return Json(symptomaticRegions, JsonRequestBehavior.AllowGet);
        }

        [GET("Examine/symptoms", IsAbsoluteUrl = true)]
        public ActionResult GetSymptoms()
        {
            throw new NotImplementedException();
        }
    }
}
