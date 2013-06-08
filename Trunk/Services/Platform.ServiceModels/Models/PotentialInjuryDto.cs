using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "PotentialInjury", Namespace = "http://SportsWebPt.Platform")]
    public class PotentialInjuryDto : InjuryDto
    {
        #region Properties

        [DataMember]
        public PotentialSymptomDto[] GivenSymptoms { get; set; }

        #endregion
    }
}
