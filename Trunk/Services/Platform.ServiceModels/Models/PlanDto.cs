using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class PlanDto
    {
        #region Properties

        public int Id { get; set; }
        
        public PlanExerciseDto[] Exercises { get; set; }

        public String[] Categories { get; set; }

        public String RoutineName { get; set; }

        public String Description { get; set; }

        public String StructuresInvolved { get; set; }

        public String Instructions { get; set; }

        public int Duration { get; set; }

        public String PageName { get; set; }

        public String Tags { get; set; }

        public BodyRegionDto[] BodyRegions { get; set; }

        public String AnimationTag { get; set; }

        #endregion
    }
}
