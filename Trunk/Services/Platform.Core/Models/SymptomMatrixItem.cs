using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class SymptomMatrixItem
    {
        #region Properties

        public int Id { get; set; }

        public int SymptomId { get; set; }

        public int BodyPartMatrixItemId { get; set; }

        public virtual Symptom Symptom { get; set; }

        public virtual BodyPartMatrixItem BodyPartMatrixItem { get; set; }

        public Boolean Decom { get; set; }

        #endregion
    }
}
