using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    public class DifferentialDiagnosisDto
    {
        #region Properties

        public int Id { get; set; }

        public DateTime SubmittedOn { get; set; }

        public DateTime ReviewedOn { get; set; }

        public String SubmittedBy { get; set; }

        public String SubmittedFor { get; set; }

        public PotentialSymptomDto[] SymptomDetails { get; set; }

        #endregion

    }
}
