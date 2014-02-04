using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class InjuryPrognosisMatrixItem
    {
        #region Propeties

        public int Id { get; set; }

        public int InjuryId { get; set; }

        public int PrognosisId { get; set; }

        public String Duration { get; set; }

        public virtual Injury Injury { get; set; }

        public virtual Prognosis Prognosis { get; set; }

        #endregion

    }
}
