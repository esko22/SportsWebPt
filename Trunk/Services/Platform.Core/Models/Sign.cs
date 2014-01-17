using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class Sign
    {
        #region Propeties

        public int Id { get; set; }

        public String Description { get; set; }

        public SignCategory Category { get; set; }

        #endregion

        #region Foriegn Keys

        public int FilterId { get; set; }
        
        #endregion

        #region Navigation Properties

        public virtual Filter Filter { get; set; }

        #endregion
    }
}
