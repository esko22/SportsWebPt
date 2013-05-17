using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class InjuryCauseMatrixItem
    {
        #region Propeties

        public int Id { get; set; }

        public int InjuryId { get; set; }

        public int CauseId { get; set; }

        public virtual Injury Injury { get; set; }

        public virtual Cause Cause { get; set; }

        #endregion
    }
}
