using System;

namespace SportsWebPt.Platform.Web.Core
{
    public class BodyPart
    {
        #region Properties

        public int id { get; set; }

        public String commonName { get; set; }

        public String scientificName { get; set; }

        public String description { get; set; }

        public SkeletonArea[] primaryAreas { get; set; }

        public SkeletonArea[] secondaryAreas { get; set; }

        #endregion
    }
}
