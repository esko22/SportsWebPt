using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class SkeletonHotspot
    {
        #region Properties

        public int Id { get; set; }

        public OrientationType Orientation { get; set; }

        public SideType Side { get; set; }

        public RegionType Region { get; set; }

        #endregion
    }
}
