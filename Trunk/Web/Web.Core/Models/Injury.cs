using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Web.Core
{

    public class BriefInjury
    {
        #region Properties

        public int id { get; set; }

        public String commonName { get; set; }

        public String medicalName { get; set; }

        public String openingStatement { get; set; }

        public IEnumerable<Sign> signs { get; set; }

        public IEnumerable<BodyRegion> bodyRegions { get; set; }

        #endregion
        
    }

    public class Injury : BriefInjury
    {
        #region Properties

        public String description { get; set; }

        public String recovery { get; set; }

        public String animationTag { get; set; }

        public IEnumerable<Plan> plans { get; set; }

        public IEnumerable<Cause> causes { get; set; }

        public IEnumerable<InjurySymptom> injurySymptoms { get; set; }

        public IEnumerable<InjuryPrognosis> injuryPrognoses { get; set; }

        public IEnumerable<Treatment> treatments { get; set; }

        public IEnumerable<Cause> lifestyleCauses { get; set; }

        public IEnumerable<Cause> physiologicalCauses { get; set; }

        public String tags { get; set; }

        public String pageName { get; set; }

        //TODO: hack.. these should be rolled up under another object..
        public InjuryPrognosis bestCase { get; set; }

        public InjuryPrognosis worstCase { get; set; }

        public InjuryPrognosis delayedRecovery { get; set; }

        #endregion

    }
}
