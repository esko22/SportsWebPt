using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class SignFilter
    {
        #region Properties

        public int Id { get; set; }

        public String FilterCategory { get; set; }

        #endregion

        #region Navigation Properties

        public ICollection<Sign> Signs { get; set; } 

        #endregion
    }
}
