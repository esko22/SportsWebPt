using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "PotentialSymptom", Namespace = "http://SportsWebPt.Platform")]
    public class PotentialSymptomDto : SymptomDto
    {
        #region Properties

        [DataMember]
        public int symptomMatrixItemId { get; set; }

        [DataMember]
        public int givenResponse { get; set; }

        #endregion
    }
}
