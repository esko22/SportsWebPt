using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class PotentialSymptomDto : SymptomDto
    {
        #region Properties

        public int SymptomMatrixItemId { get; set; }

        public string GivenResponse { get; set; }

        public string BodyPart { get; set; }

        #endregion
    }
}
