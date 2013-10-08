using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class InjurySymptomMatrixItem
    {
        #region Properties

        public int Id { get; set; }

        public int InjuryId { get; set; }

        public int SymptomMatrixItemId { get; set; }

        public String ComparisonValue { get; set; }

        public virtual Injury Injury { get; set; }

        public virtual SymptomMatrixItem SymptomMatrixItem { get; set; }

        #endregion
    }
}
