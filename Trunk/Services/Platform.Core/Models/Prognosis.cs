using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class Prognosis
    {
        #region Properties

        public int Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public PrognosisCategory Category { get; set; }

        #endregion

        #region Navigation Properties

        public ICollection<Case> Cases { get; set; } 

        #endregion
    }
}
