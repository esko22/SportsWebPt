using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "BodyPart", Namespace = "http://SportsWebPt.Platform")]
    public class BodyRegionDto
    {
        #region Properties

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public String RegionCategory { get; set; }

        #endregion
    }
}
