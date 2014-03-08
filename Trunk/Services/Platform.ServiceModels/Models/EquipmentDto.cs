using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class EquipmentDto : BriefEquipmentDto
    {
        #region Properties
 
        public String TechnicalName { get; set; }

        public String PriceRange { get; set; }

        public String ProductInformation { get; set; }

        public String Category { get; set; }

        #endregion
    }

    public class BriefEquipmentDto
    {
        #region Properties
        
        public int Id { get; set; }

        public String CommonName { get; set; }

        #endregion       
    }
}
