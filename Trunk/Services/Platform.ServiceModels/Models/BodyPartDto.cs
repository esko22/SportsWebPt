using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "BodyPart", Namespace = "http://SportsWebPt.Platform")]
    public class BodyPartDto
    {
        #region Properties

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public String commonName { get; set; }

        [DataMember]
        public String scientificName { get; set; }

        [DataMember]
        public SkeletonAreaDto[] SkeletonAreas { get; set; }


        #endregion
    }
}
