using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Web.Core
{
    public class DiagnosisReport : DifferentialDiagnosis
    {
        #region Properties

        public IEnumerable<Injury> PotentialInjuries { get; set; } 

        #endregion
    }
}
