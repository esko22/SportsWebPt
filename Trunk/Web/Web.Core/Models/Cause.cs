using System;

namespace SportsWebPt.Platform.Web.Core
{
    public class Cause
    {
        #region Propeties

        public int id { get; set; }

        public String description { get; set; }

        public String category { get; set; }

        public Filter filter { get; set; }

        public int filterId { get; set; }

        #endregion
    }
}
