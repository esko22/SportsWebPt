using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Web.Core
{
    public class Workout
    {
        #region Properties

        public int id { get; set; }

        public IEnumerable<Exercise> exercises { get; set; }

        public String category { get; set; }

        public String routineName { get; set; }

        public String description { get; set; }

        public String musclesInvolved { get; set; }

        public int duration { get; set; }

        #endregion
    }
}
