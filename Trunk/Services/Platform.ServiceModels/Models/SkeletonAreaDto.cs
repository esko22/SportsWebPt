using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class SkeletonAreaDto
    {
        #region Properties

        public int Id { get; set; }

        public String Region { get; set; }

        public String Orientation { get; set; }

        public String Side { get; set; }

        public String DisplayName { get; set; }

        public String CssClassName { get; set; }

        #endregion
    }
}
