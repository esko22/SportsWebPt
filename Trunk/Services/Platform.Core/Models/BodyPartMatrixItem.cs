using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class BodyPartMatrixItem
    {
        #region Properties

        public int Id { get; set; }

        public int SkeletonAreaId { get; set; }

        public int BodyPartId { get; set; }

        public virtual SkeletonArea SkeletonArea { get; set; }

        public virtual BodyPart BodyPart { get; set; }

        public ICollection<SymptomMatrixItem> SymptomMatrixItems { get; set; }

        public Boolean IsSecondary { get; set; }

        public Boolean Decom { get; set; }

        #endregion
    }
}
