using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "SymptomaticRegion", Namespace = "http://SportsWebPt.Platform")]
    public class SymptomaticRegionDto : SkeletonAreaDto
    {
        #region Properties

        [DataMember]
        public SymptomaticBodyPartDto[] BodyParts { get; set; }

        #endregion
    }
}
