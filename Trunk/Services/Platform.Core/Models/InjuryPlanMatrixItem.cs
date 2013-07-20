using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class InjuryPlanMatrixItem
    {
        #region Propeties

        public int Id { get; set; }

        public int InjuryId { get; set; }

        public int PlanId { get; set; }

        public virtual Injury Injury { get; set; }

        public virtual Plan Plan { get; set; }

        #endregion
    }
}
