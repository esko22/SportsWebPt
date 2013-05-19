using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Web.Core
{
    public class Injury
    {
        #region Properties

        public int id { get; set; }

        public String commonName { get; set; }

        public String medicalName { get; set; }

        public String openingStatement { get; set; }

        public String description { get; set; }

        public IEnumerable<Workout> workouts { get; set; } 

        #endregion

    }
}
