using System;

namespace SportsWebPt.Platform.Web.Core
{
    public class Sign
    {
        #region Propeties

        public int id { get; set; }

        public String description { get; set; }

        public String category { get; set; }

        public SignFilter filter { get; set; }

        public int filterId { get; set; }

        #endregion
    }
}
