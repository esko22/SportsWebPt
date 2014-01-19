using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class InjuryDto
    {
        #region Properties

        public int Id { get; set; }

        public String CommonName { get; set; }

        public String MedicalName { get; set; }

        public String OpeningStatement { get; set; }

        public String Description { get; set; }

        public String Prognosis { get; set; }

        public String Recovery { get; set; }

        public PlanDto[] Plans { get; set; }

        public SignDto[] Signs { get; set; }

        public CauseDto[] Causes { get; set; }

        public BodyRegionDto[] BodyRegions { get; set; }

        public String PageName { get; set; }

        public String Tags { get; set; }

        public String AnimationTag { get; set; }

        public InjurySymptomDto[] InjurySymptoms { get; set; }

        #endregion
    }
}
