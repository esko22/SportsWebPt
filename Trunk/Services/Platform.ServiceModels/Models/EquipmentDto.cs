using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "Equipment", Namespace = "http://SportsWebPt.Platform")]
    public class EquipmentDto
    {
        #region Properties

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public String CommonName { get; set; }

        [DataMember]
        public String TechnicalName { get; set; }

        [DataMember]
        public String PriceRange { get; set; }

        [DataMember]
        public String ProductLink1 { get; set; }

        [DataMember]
        public String ProductLink2 { get; set; }

        [DataMember]
        public String ProductLink3 { get; set; }

        [DataMember]
        public String ProductLinkName1 { get; set; }

        [DataMember]
        public String ProductLinkName2 { get; set; }

        [DataMember]
        public String ProductLinkName3 { get; set; }

        [DataMember]
        public String Category { get; set; }


        #endregion
    }
}
