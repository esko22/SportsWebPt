using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "AreaComponent", Namespace = "http://SportsWebPt.Platform")]
    public class AreaComponentDto
    {
        #region Properties

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public String commonName { get; set; }

        [DataMember]
        public String scientificName { get; set; }

        #endregion
    }
}
