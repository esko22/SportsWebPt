using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class ExerciseVideoMatrixItem
    {
        #region Properties

        public int Id { get; set; }

        public int ExcerciseId { get; set; }

        public int VideoId { get; set; }

        public virtual Exercise Exercise { get; set; }

        public virtual Video Video { get; set; }

        #endregion
    }
}
