using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "PotentialInjurySymptom", Namespace = "http://SportsWebPt.Platform")]
    public class PotentialInjurySymptomDto : PotentialSymptomDto
    {
        #region Properties

        [DataMember]
        public int InjurySymptomMatrixId { get; set; }

        [DataMember]
        public int ThresholdValue { get; set; }

        [DataMember]
        public String SkeletonArea { get; set; }

        [DataMember]
        public int bodyPaartMatrixItemId { get; set; }

        #endregion

    }
}
