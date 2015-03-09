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

        public String ClinicPatientIdentifier { get; set; }

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

        public String ClinicTherapistIdentifier { get; set; }

        public Boolean UserConfirmed { get; set; }

        public DateTime AddedOn { get; set; }

        public ClinicDto Clinic { get; set; }

        public TherapistDto Therapist { get; set; }

        #endregion
    }

    public class ClinicPlanDto
    {
        #region Prorperties

        public int Id { get; set; }

        public int ClinicId { get; set; }

        public int PlanId { get; set; }

        public String ClinicName { get; set; }

        public String PlanName { get; set; }

        public Boolean IsActive { get; set; }

        #endregion
    }

    public class ClinicExerciseDto
    {
        #region Prorperties

        public int Id { get; set; }

        public int ClinicId { get; set; }

        public int ExerciseId { get; set; }

        public String ClinicName { get; set; }

        public String ExerciseName { get; set; }

        public Boolean IsActive { get; set; }

        #endregion
    }
}
