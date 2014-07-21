using System;

namespace SportsWebPt.Platform.ServiceModels
{

    public class BriefPlanDto
    {
        #region Properties

        public int Id { get; set; }

        public String[] Categories { get; set; }

        public String RoutineName { get; set; }

        public String Description { get; set; }

        public String StructuresInvolved { get; set; }

        public BodyRegionDto[] BodyRegions { get; set; }

        public String PageName { get; set; }

        public Boolean Visible { get; set; }

        public String Tags { get; set; }

        #endregion
    }


    public class PlanDto : BriefPlanDto
    {
        #region Properties
        
        public PlanExerciseDto[] Exercises { get; set; }

        public String Instructions { get; set; }

        public int Duration { get; set; }

        public String AnimationTag { get; set; }

        #endregion
    }
}
