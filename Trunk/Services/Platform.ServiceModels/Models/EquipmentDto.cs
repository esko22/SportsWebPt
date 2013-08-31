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
        public String RecommendedVendor { get; set; }

        [DataMember]
        public String Category { get; set; }


        #endregion
    }
}
