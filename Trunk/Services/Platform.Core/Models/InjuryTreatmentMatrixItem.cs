using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class InjuryTreatmentMatrixItem
    {
        #region Propeties

        public int Id { get; set; }

        public int InjuryId { get; set; }

        public int TreatmentId { get; set; }

        public virtual Injury Injury { get; set; }

        public virtual Treatment Treatment { get; set; }

        #endregion
    }
}
