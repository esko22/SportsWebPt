using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class SkeletonArea
    {
        #region Properties

        public int Id { get; set; }

        public Orientation Orientation { get; set; }

        public Side Side { get; set; }

        public BodyRegion Region { get; set; }

        public String DisplayName { get; set; }

        public String CssClassName { get; set; }

        public virtual ICollection<BodyPartMatrixItem> BodyPartMatrix { get; set; }

        #endregion
    }
}
