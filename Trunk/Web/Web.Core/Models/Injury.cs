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

        public String prognosis { get; set; }

        public String recovery { get; set; }

        public String animationTag { get; set; }

        public IEnumerable<Plan> plans { get; set; }

        public IEnumerable<Cause> causes { get; set; }

        public IEnumerable<Sign> signs { get; set; }

        public IEnumerable<BodyRegion> bodyRegions { get; set; }

        public IEnumerable<InjurySymptom> injurySymptoms { get; set; }

        public String tags { get; set; }

        public String pageName { get; set; }

        #endregion

    }
}
