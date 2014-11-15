using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class BriefEpisodeDto
    {
        #region Properties

        public int Id { get; set; }

        public EpisodeStateDto State { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int PrognosisId { get; set; }

        public int ClinicPatientId { get; set; }

        public String PatientId { get; set; }

        public String TherapistId { get; set; }

        public int ClinicId { get; set; }

        #endregion

    }


    public class EpisodeDto : BriefEpisodeDto
    {
        #region Properties

        public PrognosisDto Prognosis { get; set; }

        public String ClinicPatientIdentifier { get; set; }

        public String PatientEmail { get; set; }

        public String TherapistEmail { get; set; }

        public ClinicDto Clinic { get; set; }

        #endregion
    }

    public enum EpisodeStateDto
    {
        Pending = 1,
        Active = 2,
        InActive = 3,
        ReferredOn = 4,
        Recovered = 5
    }
}
