using System.Collections.Generic;

namespace SportsWebPt.Platform.Web.Core
{
    public class SymptomaticRegion : SkeletonArea
    {
        #region Properties

        public IEnumerable<SymptomaticBodyPart> bodyParts { get; set; } 

        #endregion
    }
}
