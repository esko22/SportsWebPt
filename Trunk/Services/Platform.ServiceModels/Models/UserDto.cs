using System;

namespace SportsWebPt.Platform.ServiceModels
{
    public class UserDto
    {
        #region Properties

        public int Id { get; set; }

        public String EmailAddress { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String UserName { get; set; }

        public String ExternalAccountId { get; set; }

        public String Phone { get; set; }

        public String SkypeHandle { get; set; }

        public String Locale { get; set; }

        public String Gender { get; set; }

        public String Provider { get; set; }

        public String ProviderId { get; set; }

        public Boolean IsAdmin { get; set; }

        public Boolean IsTherapist { get; set; }

        public Boolean IsClinicManager { get; set; }

        public FavoriteDto[] VideoFavorites { get; set; }

        public FavoriteDto[] PlanFavorites { get; set; }

        public FavoriteDto[] InjuryFavorites { get; set; }

        public FavoriteDto[] ExerciseFavorites { get; set; }

        public Boolean AccountLinked { get; set; }

        #endregion
    }

    public class TherapistDto : UserDto
    {
        #region Properties

        public ClinicDto[] Clinics { get; set; }

        #endregion
    }

    public class TherapistSharedPlanDto
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

    public class TherapistSharedExerciseDto
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
