using System;
using System.Collections.Generic;
using System.Web.Http;
using SportsWebPt.Common.Utilities;
using SportsWebPt.Platform.Web.Core;
using SportsWebPt.Platform.Web.Services;

namespace SportsWebPt.Platform.Web.Application
{
    public class ExamineController : ApiController
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

        [HttpGet]
        [Route("data/examine/symptomaticregions")]
        public IEnumerable<SymptomaticRegion> GetSymptomaticRegions()
        {
            return _examineService.GetSymptomaticRegions();
        }

        [HttpGet]
        [Route("data/examine/potentialsymptoms/{bodyPartMatrixId}")]
        public IEnumerable<PotentialSymptom> GetPotentialSymptoms(string bodyPartMatrixId)
        {
            var matrixId = 0;
            if (!String.IsNullOrEmpty(bodyPartMatrixId))
                int.TryParse(bodyPartMatrixId, out matrixId);

            return _examineService.GetPotentialSymptoms(matrixId);
        }

        [HttpGet]
        [Route("data/examine/diagnosisreport/{diffDiagId}")]
        public DiagnosisReport GetDiagnosisReport(string diffDiagId)
        {
            var differentialDiagnosisId = 0;
            if (!String.IsNullOrEmpty(diffDiagId))
                int.TryParse(diffDiagId, out differentialDiagnosisId);

            return _examineService.GetDiagnosisReport(differentialDiagnosisId);
        }

        [HttpPost]
        [Route("data/examine/diffdiag")]
        public int SubmitDifferentialDiagnosis(DifferentialDiagnosis details)
        {
            if (User.Identity.IsAuthenticated && String.IsNullOrEmpty(details.submittedBy))
                details.submittedBy = User.GetServiceAccount();

            if (User.Identity.IsAuthenticated && String.IsNullOrEmpty(details.submittedFor))
                details.submittedFor = User.GetServiceAccount();


            return _examineService.SubmitDifferentialDiagnosis(details);
        }
    }
}
