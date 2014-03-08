using System;

namespace SportsWebPt.Platform.Web.Core
{
    public class Equipment : BriefEquipment
    {
        #region Properties

        public int id { get; set; }

        public String commonName { get; set; }

        public String technicalName { get; set; }

        public String priceRange { get; set; }

        public String productInformation { get; set; }

        public string category { get; set; }

        #endregion
    }

    public class BriefEquipment
    {
        #region Properties

        public int id { get; set; }

        public String commonName { get; set; }

        #endregion
        
    }
}
