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
            viewModel.SkeletonAreas = _examineService.GetSkeletonAreas();

            return View(viewModel);
        }

        [GET("Examine/areas/{id}/components", IsAbsoluteUrl = true)]
        public ActionResult GetAreaComponents(int id)
        {
            var areaComponents = _examineService.GetAreaComponents(id);

            return Json(areaComponents, JsonRequestBehavior.AllowGet);
        }
    }
}
