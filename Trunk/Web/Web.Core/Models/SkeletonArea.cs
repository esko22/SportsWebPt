using System;

namespace SportsWebPt.Platform.Web.Core
{
    public class SkeletonArea
    {
        #region Properties

        public int id { get; set; }

        public String region { get; set; }

        public String orientation { get; set; }

        public String side { get; set; }

        public String name
        {
            get { return String.Format("{0}-{1}-{2}", orientation, region, side).ToLower(); }
        }

        #endregion
    }
}
