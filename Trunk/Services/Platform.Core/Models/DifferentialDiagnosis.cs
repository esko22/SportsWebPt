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

        public Guid SubmittedBy { get; set; }

        public Guid SubmittedFor { get; set; }

        public ICollection<SymptomDetail> SymptomDetails { get; set; } 

        #endregion
    }
}
