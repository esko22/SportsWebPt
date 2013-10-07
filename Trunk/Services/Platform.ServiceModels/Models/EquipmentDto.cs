using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class EquipmentDto
    {
        #region Properties

        public int Id { get; set; }

        public String CommonName { get; set; }

        public String TechnicalName { get; set; }

        public String PriceRange { get; set; }

        public String ProductInformation { get; set; }

        public String Category { get; set; }

        #endregion
    }
}
