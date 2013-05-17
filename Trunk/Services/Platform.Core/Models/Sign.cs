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
    }
}
