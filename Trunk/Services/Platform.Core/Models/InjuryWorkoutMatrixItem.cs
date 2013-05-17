using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class InjuryWorkoutMatrixItem
    {
        #region Propeties

        public int Id { get; set; }

        public int InjuryId { get; set; }

        public int WorkoutId { get; set; }

        public virtual Injury Injury { get; set; }

        public virtual Workout Workout { get; set; }

        #endregion
    }
}
