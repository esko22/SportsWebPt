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

        public String ProductLink1 { get; set; }

        public String ProductLink2 { get; set; }

        public String ProductLink3 { get; set; }

        public String ProductLinkName1 { get; set; }

        public String ProductLinkName2 { get; set; }

        public String ProductLinkName3 { get; set; }

        public String Category { get; set; }

        #endregion
    }
}
