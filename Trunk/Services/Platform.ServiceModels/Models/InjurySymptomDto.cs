using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class InjurySymptomDto : SymptomDto
    {
        #region Properties

        public String ComparisonValue { get; set; }

        public Boolean IsRedFlag { get; set; }

        public Int32 BodyPartMatrixItemId { get; set; }

        public String BodyPartMatrixItemName { get; set; }

        public Int32 SymptomId { get; set; }

        #endregion

    }
}
