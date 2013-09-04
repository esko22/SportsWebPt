using System;

namespace SportsWebPt.Platform.Web.Core
{
    public class BodyPartMatrixItem
    {
        #region Properties

        public int id { get; set; }

        public BodyPart bodyPart { get; set; }

        public SkeletonArea skeletonArea { get; set; }

        public String displayName { get; set; }

        public String cssClassName { get; set; }

        #endregion
    }
}
