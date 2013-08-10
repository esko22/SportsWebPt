using System;

namespace SportsWebPt.Platform.Web.Core
{
    public class InjurySymptom : Symptom
    {
        #region Properties

        public BodyPartMatrixItem bodyPartMatrixItem { get; set; }

        public int thresholdValue { get; set; }

        #endregion
    }
}
