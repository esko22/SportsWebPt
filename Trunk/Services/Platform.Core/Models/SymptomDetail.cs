using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class SymptomDetail
    {
        #region Properties

        public int SymptomMatrixItemId { get; set; }

        public String GivenResponse { get; set; }

        public int DifferentialDiagnosisId { get; set; }

        public virtual SymptomMatrixItem SymptomMatrixItem { get; set; }

        public virtual DifferentialDiagnosis DifferentialDiagnosis { get; set; }

        #endregion
    }
}
