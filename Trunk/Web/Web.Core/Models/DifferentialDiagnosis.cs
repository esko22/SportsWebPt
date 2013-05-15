using System;

namespace SportsWebPt.Platform.Web.Core
{
    public class DifferentialDiagnosis
    {
        public PotentialSymptom[] symptomDetails { get; set; }

        public DateTime submittedOn { get; set; }

        public DateTime reviewedOn { get; set; }

        public int submittedBy { get; set; }

        public int submittedFor { get; set; }

    }
}
