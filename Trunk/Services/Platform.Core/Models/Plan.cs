using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class Plan
    {
        #region Properties

        public int Id { get; set; }

        public String RoutineName { get; set; }

        public String Description { get; set; }

        public String StructuresInvolved { get; set; }

        public String Instructions { get; set; }

        public int Duration { get; set; }

        public String PageName { get; set; }

        public String Tags { get; set; }

        public ICollection<PlanCategoryMatrixItem> PlanCategoryMatrixItems { get; set; } 

        public ICollection<InjuryPlanMatrixItem> InjuryPlanMatrixItems { get; set; }

        public ICollection<PlanExerciseMatrixItem> PlanExerciseMatrixItems { get; set; }

        public ICollection<PlanBodyRegionMatrixItem> PlanBodyRegionMatrixItems { get; set; }

        public ICollection<User> Users { get; set; } 

        #endregion
    }
}
