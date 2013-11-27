using System.Collections.Generic;

namespace SportsWebPt.Platform.Web.Core
{
    public class PotentialInjury : Injury
    {
        #region Properties

        public IEnumerable<PotentialSymptom> givenSymptoms { get; set; }

        public double likelyHood { get; set; }

        #endregion
    }
}
