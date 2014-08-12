using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Web.Core
{
    public class User
    {
        #region Properties
        
        public String emailAddress { get; set; }

        public String firstName { get; set; }

        public String lastName { get; set; }

        public String phone { get; set; }

        public String hash { get; set; }

        public String skypeHandle { get; set; }

        public String userName { get; set; }

        public int id { get; set; }

        public String providerId { get; set; }

        public String provider { get; set; }

        public String locale { get; set; }

        public String gender { get; set; }

        public Boolean isAdmin { get; set; }

        public Boolean isTherapist { get; set; }

        public Boolean isClinicManager { get; set; }

        public IEnumerable<Favorite> videos { get; set; }

        public IEnumerable<Favorite> plans { get; set; }

        public IEnumerable<Favorite> injuries { get; set; }
        
        public IEnumerable<Favorite> exercises { get; set; } 

        #endregion

    }

    public class Therapist : User
    {
        #region Properties

        public IEnumerable<Clinic> clinics { get; set; }

        public IEnumerable<TherapistSharedPlan> sharedPlans { get; set; }

        public IEnumerable<TherapistSharedExercise> sharedExercises { get; set; } 

        #endregion
    }

    public class TherapistSharedPlan
    {
        #region Prorperties

        public int id { get; set; }

        public int clinicId { get; set; }

        public int planId { get; set; }

        public String clinicName { get; set; }

        public String planName { get; set; }

        public Boolean isActive { get; set; }

        #endregion
    }

    public class TherapistSharedExercise
    {
        #region Prorperties

        public int id { get; set; }

        public int clinicId { get; set; }

        public int exerciseId { get; set; }

        public String clinicName { get; set; }

        public String exerciseName { get; set; }

        public Boolean isActive { get; set; }

        #endregion
    }


}
