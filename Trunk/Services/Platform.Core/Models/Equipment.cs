using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class Equipment
    {
        #region Properties

        public int Id { get; set; }

        public String CommonName { get; set; }

        public String TechnicalName { get; set; }

        public String PriceRange { get; set; }

        public String RecommendedVendor { get; set; }

        public ICollection<ExerciseEquipmentMatrixItem> ExerciseEquipmentMatrixItems { get; set; }  

        #endregion
    }
}
