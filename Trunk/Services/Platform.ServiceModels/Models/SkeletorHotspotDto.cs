using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "SkeletorHotspot", Namespace = "http://SportsWebPt.Platform")]
    public class SkeletorHotspotDto
    {
        #region Properties

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public String region { get; set; }

        [DataMember]
        public String orientation { get; set; }

        [DataMember]
        public String side { get; set; }

        #endregion
    }
}
