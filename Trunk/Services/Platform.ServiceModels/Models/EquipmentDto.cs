using System;
using System.Runtime.Serialization;

namespace SportsWebPt.Platform.ServiceModels
{
    [DataContract(Name = "Equipment", Namespace = "http://SportsWebPt.Platform")]
    public class EquipmentDto
    {
        #region Properties

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public String commonName { get; set; }

        [DataMember]
        public String technicalName { get; set; }

        [DataMember]
        public String priceRange { get; set; }

        [DataMember]
        public String recommendedVendor { get; set; }

        #endregion
    }
}
