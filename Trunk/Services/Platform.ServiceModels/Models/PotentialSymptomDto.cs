using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class PotentialSymptomDto : SymptomDto
    {
        #region Properties

        public int SymptomId { get; set; }

        public string GivenResponse { get; set; }

        public string BodyPart { get; set; }

        #endregion
    }
}
