using System;

namespace SportsWebPt.Platform.Web.Core
{
    public class DifferentialDiagnosisSubmission
    {
        public PotentialSymptom[] symptomDetails { get; set; }

        public int submittedFor { get; set; }
    }
}
