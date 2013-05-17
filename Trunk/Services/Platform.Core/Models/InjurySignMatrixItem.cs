using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class InjurySignMatrixItem
    {
        #region Propeties

        public int Id { get; set; }

        public int InjuryId { get; set; }

        public int SignId { get; set; }

        public virtual Injury Injury { get; set; }

        public virtual Sign Sign { get; set; }

        #endregion
    }
}
