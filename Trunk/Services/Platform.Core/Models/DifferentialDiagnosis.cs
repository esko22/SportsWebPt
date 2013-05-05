using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class DifferentialDiagnosis
    {
        #region Properties

        public int Id { get; set; }

        public DateTime SumbittedOn { get; set; }

        public DateTime ReviewedOn { get; set; }

        public int SubmittedBy { get; set; }

        public int SubmittedFor { get; set; }

        public ICollection<SymptomResponse> SymptomDiagnosisResults { get; set; } 

        #endregion
    }
}
