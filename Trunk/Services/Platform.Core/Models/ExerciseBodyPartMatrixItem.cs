using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class ExerciseBodyPartMatrixItem
    {
        #region Properties

        public int Id { get; set; }

        public int ExerciseId { get; set; }

        public int BodyPartId { get; set; }

        public virtual Exercise Exercise { get; set; }

        public virtual BodyPart BodyPart { get; set; }

        #endregion
    }
}
