using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "BodyPartMatrixItem", Namespace = "http://SportsWebPt.Platform")]
    public class BodyPartMatrixItemDto
    {
        #region Properties

        [DataMember]
        public Int32 Id { get; set; }

        [DataMember]
        public BodyPartDto BodyPart { get; set; }

        [DataMember]
        public SkeletonAreaDto SkeletonArea { get; set; } 

        #endregion
    }
}
