using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "InjurySymptom", Namespace = "http://SportsWebPt.Platform")]
    public class InjurySymptomDto : SymptomDto
    {
        #region Properties

        [DataMember]
        public int ThresholdValue { get; set; }

        [DataMember]
        public SymptomDto Symptom { get; set; }

        [DataMember]
        public BodyPartMatrixItemDto BodyPartMatrixItem { get; set; } 

        #endregion

    }
}
