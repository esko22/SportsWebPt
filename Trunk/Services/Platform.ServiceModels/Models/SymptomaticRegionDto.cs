using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "SymptomaticRegion", Namespace = "http://SportsWebPt.Platform")]
    public class SymptomaticRegionDto
    {
        #region Properties

        [DataMember]
        public SkeletonAreaDto SkeletonArea { get; set; }

        [DataMember]
        public ComponentSymptomDto[] ComponentSymptoms { get; set; }

        #endregion
    }
}
