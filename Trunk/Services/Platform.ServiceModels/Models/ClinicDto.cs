using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class ClinicDto
    {
        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public string DisplayImage { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }

        #endregion
    }

    public class ClinicPatientDto
    {
        #region Properties

        public int Id { get; set; }

        public Boolean UserConfirmed { get; set; }

        public ClinicDto Clinic { get; set; }

        public UserDto User { get; set; }

        public DateTime AddedOn { get; set; }

        #endregion
    }

    public class ClinicTherapistDto
    {
        #region Properties

        public int Id { get; set; }

        public Boolean UserConfirmed { get; set; }

        public DateTime AddedOn { get; set; }

        public ClinicDto Clinic { get; set; }

        public TherapistDto Therapist { get; set; }

        #endregion
    }
}
