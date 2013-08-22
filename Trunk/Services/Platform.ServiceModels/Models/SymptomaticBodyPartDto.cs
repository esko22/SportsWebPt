using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "SymptomaticBodyPart", Namespace = "http://SportsWebPt.Platform")]
    public class SymptomaticBodyPartDto : BodyPartDto
    {
        #region Properties

        [DataMember]
        public int BodyPartMatrixId { get; set; }

        [DataMember]
        public Boolean IsSecondary { get; set; }


        [DataMember]
        public PotentialSymptomDto[] PotentialSymptoms { get; set; }


        #endregion
    }
}
