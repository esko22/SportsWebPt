using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class Workout
    {
        #region Properties

        public int Id { get; set; }

        public WorkoutCategory Category { get; set; }

        public String RoutineName { get; set; }

        public String Description { get; set; }

        public String MusclesInvolved { get; set; }

        public int Duration { get; set; }

        public ICollection<InjuryWorkoutMatrixItem> InjuryWorkoutMatrixItems { get; set; }

        public ICollection<WorkoutExceriseMatrixItem> WorkoutExerciseMatrixItems { get; set; }

        #endregion
    }
}
