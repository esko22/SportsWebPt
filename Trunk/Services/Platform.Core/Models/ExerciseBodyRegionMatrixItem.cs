using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class ExerciseBodyRegionMatrixItem
    {
        #region Properties

        public int Id { get; set; }

        public int ExerciseId { get; set; }

        public int BodyRegionId { get; set; }

        public virtual Exercise Exercise { get; set; }

        public virtual BodyRegion BodyRegion { get; set; }

        #endregion
    }
}
