using System;

namespace SportsWebPt.Platform.Core.Models
{
    public class PlanExerciseMatrixItem
    {
        #region Properties

        public int Id { get; set; }

        public int PlanId { get; set; }

        public int ExerciseId { get; set; }

        public int Sets { get; set; }

        public int Repititions { get; set; }

        public int PerWeek { get; set; }

        public int PerDay { get; set; } 

        public virtual Plan Plan { get; set; }

        public virtual Exercise Exercise { get; set; }

        #endregion

    }
}
