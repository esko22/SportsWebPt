using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    public class SymptomaticBodyPartDto : BodyPartDto
    {
        #region Properties

        public int BodyPartMatrixId { get; set; }

        public Boolean IsSecondary { get; set; }

        public PotentialSymptomDto[] PotentialSymptoms { get; set; }

        #endregion
    }
}
