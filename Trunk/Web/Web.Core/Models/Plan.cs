using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Web.Core
{

    public class BriefPlan
    {
        #region Properties

        public int id { get; set; }

        public IEnumerable<BodyRegion> bodyRegions { get; set; }

        public String[] categories { get; set; }

        public String routineName { get; set; }

        public String description { get; set; }

        public String structuresInvolved { get; set; }

        #endregion
    }

    public class Plan : BriefPlan
    {
        #region Properties

        public IEnumerable<PlanExercise> exercises { get; set; }

        public String instructions { get; set; }

        public int duration { get; set; }

        public String tags { get; set; }

        public String pageName { get; set; }

        public String animationTag { get; set; }

        #endregion
    }
}
