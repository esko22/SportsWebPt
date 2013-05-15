using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "Injury", Namespace = "http://SportsWebPt.Platform")]
    public class InjuryDto
    {
        #region Properties

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public String commonName { get; set; }

        [DataMember]
        public String medicalName { get; set; }

        [DataMember]
        public String openingStatement { get; set; }

        [DataMember]
        public String description { get; set; }

        #endregion
    }
}
