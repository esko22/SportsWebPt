using System.Collections.Generic;

namespace SportsWebPt.Platform.Web.Core
{
    public class SymptomaticRegion : SkeletonArea
    {
        #region Properties

        public IEnumerable<SymptomaticComponent> components { get; set; } 

        #endregion
    }
}
