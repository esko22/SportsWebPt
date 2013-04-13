using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class SkeletonHotspot
    {
        #region Properties

        public int Id { get; set; }

        public Orientation Orientation { get; set; }

        public Side Side { get; set; }

        public BodyRegion Region { get; set; }

        #endregion
    }
}
