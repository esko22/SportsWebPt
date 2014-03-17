using System;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

using AttributeRouting;
using AttributeRouting.Web.Mvc;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Core;
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

        [GET("Examine/symptomaticregions", IsAbsoluteUrl = true)]
        public ActionResult GetSymptomaticRegions()
        {
            var symptomaticRegions = _examineService.GetSymptomaticRegions();

            return Json(symptomaticRegions, JsonRequestBehavior.AllowGet);
        }

        [GET("Examine/potentialsymptoms/{bodyPartMatrixId}", IsAbsoluteUrl = true)]
        public ActionResult GetPotentialSymptoms(string bodyPartMatrixId)
        {
            var matrixId = 0;
            if (!String.IsNullOrEmpty(bodyPartMatrixId))
                int.TryParse(bodyPartMatrixId, out matrixId);

            var potentialSymptoms = _examineService.GetPotentialSymptoms(matrixId);

            return Json(potentialSymptoms, JsonRequestBehavior.AllowGet);
        }

        [GET("Examine/diagnosisreport/{diffDiagId}", IsAbsoluteUrl = true)]
        public ActionResult GetDiagnosisReport(string diffDiagId)
        {
            var differentialDiagnosisId = 0;
            if (!String.IsNullOrEmpty(diffDiagId))
                int.TryParse(diffDiagId, out differentialDiagnosisId);

            var diagnosisReport = _examineService.GetDiagnosisReport(differentialDiagnosisId);

            return Json(diagnosisReport, JsonRequestBehavior.AllowGet);
        }

        [POST("examine/diffdiag", IsAbsoluteUrl = true)]
        public ActionResult SubmitDifferentialDiagnosis(DifferentialDiagnosis details)
        {
            var diffDiagId = _examineService.SubmitDifferentialDiagnosis(details);
            return Json(diffDiagId, JsonRequestBehavior.DenyGet);
        }
    }
}
