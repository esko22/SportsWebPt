using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class Cause
    {
        #region Propeties

        public int Id { get; set; }

        public String Description { get; set; }

        public CauseCategory Category { get; set; }

        #endregion

        #region Foriegn Keys

        public int FilterId { get; set; }

        #endregion

        #region Navigation Properties

        public virtual Filter Filter { get; set; }

        #endregion
    }
}
