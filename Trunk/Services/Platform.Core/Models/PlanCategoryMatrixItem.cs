using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class PlanCategoryMatrixItem
    {
        #region Properties

        public int PlanId { get; set; }

        public virtual FunctionCategory Category { get; set; }

        #endregion

        #region NavigationProperty

        public virtual Plan Plan { get; set; }

        #endregion
    }
}
