using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Web.Core
{

    public class BriefPlan
    {
        #region Properties

        public int id { get; set; }

        public IEnumerable<BodyRegion> bodyRegions { get; set; }

        public IEnumerable<ClinicPlan> sharedClinics { get; set; } 

        public String[] categories { get; set; }

        public String routineName { get; set; }

        public String description { get; set; }

        public String structuresInvolved { get; set; }

        public String pageName { get; set; }

        public bool visible { get; set; }

        public Boolean requestorIsOwner { get; set; }

        #region Grid Helpers

        public String formattedBodyRegions { get; set; }

        public String formattedCategories { get; set; }
        
        #endregion

        #endregion
    }

    public class Plan : BriefPlan
    {
        #region Properties

        public IEnumerable<PlanExercise> exercises { get; set; }

        public String instructions { get; set; }

        public int duration { get; set; }

        public String tags { get; set; }

        public String animationTag { get; set; }

        #endregion
    }
}
