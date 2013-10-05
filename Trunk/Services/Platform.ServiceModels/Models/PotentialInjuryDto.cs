using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class PotentialInjuryDto : InjuryDto
    {
        #region Properties

        public PotentialSymptomDto[] GivenSymptoms { get; set; }

        #endregion
    }
}
