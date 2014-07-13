using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class Therapist
    {

        #region Properties

        public int Id { get; set; }

        public String Credentials { get; set; }

        public String Licenses { get; set; }

        #endregion


        #region Navigation Props

        public virtual User User { get; set; }

        public ICollection<ClinicTherapistMatrixItem> ClinicTherapistMatrixItems { get; set; }

        public ICollection<TherapistPlanMatrixItem> TherapistPlanMatrixItems { get; set; }

        public ICollection<TherapistExerciseMatrixItem> TherapistExerciseMatrixItems { get; set; }
        
        #endregion

    }

    public class TherapistPlanMatrixItem
    {
        #region Properties

        public int Id { get; set; }

        public int TherapistId { get; set; }

        public int PlanId { get; set; }

        public Boolean IsActive { get; set; }

        #endregion

        #region Naviagtion Properties

        public virtual Plan Plan { get; set; }

        public virtual Therapist Therapist { get; set; }

        #endregion
    }

    public class TherapistExerciseMatrixItem
    {
        #region Properties

        public int Id { get; set; }

        public int TherapistId { get; set; }

        public int ExerciseId { get; set; }

        public Boolean IsActive { get; set; }

        #endregion

        #region Naviagtion Properties

        public virtual Exercise Exercise { get; set; }

        public virtual Therapist Therapist { get; set; }

        #endregion
    }
}
