using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "SymptomaticBodyPart", Namespace = "http://SportsWebPt.Platform")]
    public class SymptomaticBodyPartDto : BodyPartDto
    {
        #region Properties

        [DataMember]
        public int bodyPartMatrixId { get; set; }

        #endregion
    }
}
