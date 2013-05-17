using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class WorkoutExceriseMatrixItem
    {
        #region Properties

        public int Id { get; set; }

        public int WorkoutId { get; set; }

        public int ExerciseId { get; set; }

        public virtual Workout Workout { get; set; }

        public virtual Exercise Exercise { get; set; }

        #endregion

    }
}
