using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class Filter
    {
        #region Properties

        public int Id { get; set; }

        public String FilterCategory { get; set; }

        public FilterType FilterType { get; set; }

        #endregion

        #region Navigation Properties

        public ICollection<Sign> Signs { get; set; }

        public ICollection<Cause> Causes { get; set; } 

        #endregion
    }
}
