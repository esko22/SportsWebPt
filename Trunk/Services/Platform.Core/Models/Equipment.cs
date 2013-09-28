using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class Equipment
    {
        #region Properties

        public int Id { get; set; }

        public String CommonName { get; set; }

        public String PriceRange { get; set; }

        public String ProductLink1 { get; set; }

        public String ProductLink2 { get; set; }

        public String ProductLink3 { get; set; }

        public FunctionCategory Category { get; set; }

        #endregion

        #region Navigation Properties

        public ICollection<ExerciseEquipmentMatrixItem> ExerciseEquipmentMatrixItems { get; set; }

        public ICollection<Vendor> Vendors { get; set; } 

        #endregion
    }
}
