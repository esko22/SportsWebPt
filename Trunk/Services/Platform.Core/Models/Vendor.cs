using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class Vendor
    {
        #region Properties

        public Int64 Id { get; set; }

        public String Name { get; set; }

        public String Url { get; set; }

        #endregion

        #region Navigation Properties
        
        public ICollection<Equipment> Equipment { get; set; }

        #endregion
    }
}
