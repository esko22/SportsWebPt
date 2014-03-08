using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class BriefInjuryDto
    {
        #region Properties

        public int Id { get; set; }

        public String CommonName { get; set; }

        public String MedicalName { get; set; }

        public String OpeningStatement { get; set; }

        public SignDto[] Signs { get; set; }

        public BodyRegionDto[] BodyRegions { get; set; }

        #endregion
    }

    public class InjuryDto : BriefInjuryDto
    {
        #region Properties

        public String Description { get; set; }

        public String Recovery { get; set; }

        public PlanDto[] Plans { get; set; }

        public SignDto[] Signs { get; set; }

        public CauseDto[] Causes { get; set; }

        public TreatmentDto[] Treatments { get; set; }

        public String PageName { get; set; }

        public String Tags { get; set; }

        public String AnimationTag { get; set; }

        public InjurySymptomDto[] InjurySymptoms { get; set; }

        public InjuryPrognosisDto[] InjuryPrognoses { get; set; }

        #endregion
    }
}
