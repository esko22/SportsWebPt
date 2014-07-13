using System;
using System.Collections.Generic;

namespace SportsWebPt.Platform.Core.Models
{
    public class Clinic
    {
        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public string DisplayImage { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }

        #endregion

        #region ForeignKeys
		 
	    #endregion

        #region Naviagation Properties

        public ICollection<Location> Locations { get; set; } 
        public ICollection<ClinicTherapistMatrixItem> ClinicTherapistMatrixItems { get; set; }
        public ICollection<ClinicAdminMatrixItem> ClinicAdminMatrixItems { get; set; }
        public ICollection<ClinicPatientMatrixItem> ClinicPatientMatrixItems { get; set; }
        public ICollection<ClinicPlanMatrixItem> ClinicPlanMatrixItems { get; set; }
        public ICollection<ClinicExerciseMatrixItem> ClinicExerciseMatrixItems { get; set; }
		 
	    #endregion
    }

    public class ClinicPlanMatrixItem
    {
        #region Properties

        public int Id { get; set; }

        public int ClinicId { get; set; }

        public int PlanId { get; set; }

        public Boolean IsPublic { get; set; }
		 
    	#endregion

        #region Naviagtion Properties

        public virtual Plan Plan { get; set; }

        public virtual Clinic Clinic { get; set; }

    	#endregion
    }

    public class ClinicExerciseMatrixItem
    {
        #region Properties

        public int Id { get; set; }

        public int ClinicId { get; set; }

        public int ExerciseId { get; set; }

        public Boolean IsPublic { get; set; }
		 
    	#endregion

        #region Naviagtion Properties

        public virtual Exercise Exercise { get; set; }

        public virtual Clinic Clinic { get; set; }

    	#endregion
    }

    public class ClinicInjuryMatrixItem
    {
        #region Properties

        public int Id { get; set; }

        public int ClinicId { get; set; }

        public int InjuryId { get; set; }

        public Boolean IsPublic { get; set; }

        #endregion

        #region Naviagtion Properties

        public virtual Injury Injury { get; set; }

        public virtual Clinic Clinic { get; set; }

        #endregion
    }

    public class ClinicAdmin
    {
        #region Properties

        public int Id { get; set; }

        public String EmergencyContact { get; set; }

        #endregion

        #region Navigation Properties

        public ICollection<ClinicAdminMatrixItem> ClinicAdminMatrixItems { get; set; }

        public virtual User User { get; set; }

        #endregion
    }

    public class Location
    {
        #region Properties

        public Int64 Id { get; set; }

        public String Address { get; set; }

        public String City { get; set; }

        public String State { get; set; }

        public int Zipcode { get; set; }

        #endregion

        #region Foreign Keys

        public int ClinicId { get; set; }

        #endregion
    }
}
