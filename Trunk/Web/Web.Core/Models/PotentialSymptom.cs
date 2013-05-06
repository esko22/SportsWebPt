using System;

namespace SportsWebPt.Platform.Web.Core
{
    public class PotentialSymptom : Symptom
    {
        #region Properties

        public int symptomMatrixItemId { get; set; }

        public String givenResponse { get; set; }

        #endregion
    }
}
