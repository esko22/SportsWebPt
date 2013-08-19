using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "BodyPart", Namespace = "http://SportsWebPt.Platform")]
    public class BodyPartDto
    {
        #region Properties

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public String CommonName { get; set; }

        [DataMember]
        public String ScientificName { get; set; }

        [DataMember]
        public SkeletonAreaDto[] PrimaryAreas { get; set; }

        [DataMember]
        public SkeletonAreaDto[] SecondaryAreas { get; set; }

        #endregion
    }
}
