using System;

namespace SportsWebPt.Platform.Web.Core
{
    public class DifferentialDiagnosis
    {
        public PotentialSymptom[] symptomDetails { get; set; }

        public DateTime submittedOn { get; set; }

        public DateTime reviewedOn { get; set; }

        public String submittedBy { get; set; }

        public String submittedFor { get; set; }

        public Int64 sessionId { get; set; }

    }
}
