using System;

namespace SportsWebPt.Platform.Web.Core
{
    public class InjurySymptom : Symptom
    {
        #region Properties

        public string comparisonValue { get; set; }

        public int symptomId { get; set; }

        public int bodyPartMatrixItemId { get; set; }

        public String bodyPartMatrixItemName { get; set; }

        #endregion
    }
}
