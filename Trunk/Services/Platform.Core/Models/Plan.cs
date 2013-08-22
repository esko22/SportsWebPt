using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class Plan
    {
        #region Properties

        public int Id { get; set; }

        public FunctionCategory Category { get; set; }

        public String RoutineName { get; set; }

        public String Description { get; set; }

        public String MusclesInvolved { get; set; }

        public int Duration { get; set; }

        public String PageName { get; set; }

        public String Tags { get; set; }

        public ICollection<InjuryPlanMatrixItem> InjuryPlanMatrixItems { get; set; }

        public ICollection<PlanExerciseMatrixItem> PlanExerciseMatrixItems { get; set; }

        public ICollection<PlanBodyRegionMatrixItem> PlanBodyRegionMatrixItems { get; set; } 

        #endregion
    }
}
