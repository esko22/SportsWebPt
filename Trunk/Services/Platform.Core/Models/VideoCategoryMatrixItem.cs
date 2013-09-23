using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class VideoCategoryMatrixItem
    {
        #region Properties

        public int VideoId { get; set; }

        public virtual FunctionCategory Category { get; set; }

        #endregion

        #region NavigationProperty

        public virtual Video Video { get; set; }

        #endregion

    }
}
